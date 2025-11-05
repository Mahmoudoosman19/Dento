using Common.Domain.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResponseModel
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var validationTasks = _validators
                .Select(validator => validator.ValidateAsync(request, cancellationToken))
                .ToList();

            await Task.WhenAll(validationTasks);

            var errors = validationTasks
                .SelectMany(validationTask => validationTask.Result.Errors)
                .Where(validationFailure => validationFailure != null)
                .Select(failure => $"{failure.PropertyName} {failure.ErrorMessage}")
                .Distinct()
                .ToArray();

            if (errors.Any())
                return CreateValidationResult<TResponse>(errors);

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(string[] errorMessages)
            where TResult : ResponseModel
        {
            if (typeof(TResult).IsAssignableFrom(typeof(ResponseModel)))
                return (ValidationResult.WithErrors(errorMessages) as TResult)!;

            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult<TResult>.WithErrors))!
                .Invoke(null, [null, errorMessages])!;

            return (TResult)validationResult;
        }

        private void LogValidationErrors(TRequest request, string[] propertyNames, string[] errorMessages)
        {
            _logger.LogWarning("Validation failed for request {RequestType}. Validation errors: {Errors}",
                            typeof(TRequest).Name,
                            FormatValidationErrors(propertyNames, errorMessages));
        }

        private static string FormatValidationErrors(string[] propertyNames, string[] errorMessages)
        {
            var formattedErrors = propertyNames
                .Zip(errorMessages, (property, error) => $"{property}: {error}")
                .ToList();

            return string.Join("; ", formattedErrors);
        }
    }

}



namespace Common.Domain.Shared;

/// <summary>
/// Represents a validation result with errors.
/// </summary>
public sealed class ValidationResult : ResponseModel, IValidationResult
{
    private ValidationResult(string[] errors)
        : base(false, errors.Length > 0 ? errors[0] : "Validation failed")
    {
        ErrorMessages = errors;
    }

    /// <summary>
    /// Gets the errors associated with the validation result
    /// </summary>
    public string[] ErrorMessages { get; }

    /// <summary>
    /// Creates a validation result with the specified errors.
    /// </summary>
    public static ValidationResult WithErrors(string[] errors) => new ValidationResult(errors);
}

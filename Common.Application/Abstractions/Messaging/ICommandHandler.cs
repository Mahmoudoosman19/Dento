using Common.Domain.Shared;
using MediatR;

namespace Common.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ResponseModel>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, ResponseModel<TResponse>>
    where TCommand : ICommand<TResponse>
{
}

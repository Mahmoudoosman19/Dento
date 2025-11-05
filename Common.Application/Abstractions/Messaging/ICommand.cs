using Common.Domain.Shared;
using MediatR;

namespace Common.Application.Abstractions.Messaging;

public interface ICommand : IRequest<ResponseModel>
{
}

public interface ICommand<TResponse> : IRequest<ResponseModel<TResponse>>
{
}

using Common.Domain.Shared;
using MediatR;

namespace Common.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<ResponseModel<TResponse>>
{
}
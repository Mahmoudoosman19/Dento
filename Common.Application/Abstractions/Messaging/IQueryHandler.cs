using Common.Domain.Shared;
using MediatR;

namespace Common.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, ResponseModel>
    where TQuery : IQuery
{
}
public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, ResponseModel<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
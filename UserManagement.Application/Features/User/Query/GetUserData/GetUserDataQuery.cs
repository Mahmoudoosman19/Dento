using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Queries.GetUserData
{
    public class GetUserDataQuery : IQuery<GetUserDataQueryResponse>
    {
        public Guid Id { get; init; }
    }
}

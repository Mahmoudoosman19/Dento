using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Queries.GetUsersData
{
    public class GetUsersDataQuery : IQuery<List< GetUsersDataQueryResponse>>
    {
        public List<Guid> Ids { get; init; }
    }
   
}

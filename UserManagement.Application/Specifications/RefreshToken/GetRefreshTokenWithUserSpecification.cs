
using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.RefreshToken
{
    public class GetRefreshTokenWithUserSpecification : Specification<Domain.Entities.RefreshToken>
    {
        public GetRefreshTokenWithUserSpecification(string token)
        {
            AddCriteria(x => x.Token == token);

            AddInclude($"{nameof(Domain.Entities.RefreshToken.User)}");
        }
    }
}

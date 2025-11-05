using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.RefreshToken
{
    public class GetRefreshTokenByUserIdSpecification : Specification<Domain.Entities.RefreshToken>
    {
        public GetRefreshTokenByUserIdSpecification(Guid userId)
        {
            AddCriteria(x => x.UserId == userId);
        }
    }
}

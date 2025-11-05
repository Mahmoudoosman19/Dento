using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.User;

public class GetUserByEmailWithOtpSpecification : Specification<Domain.Entities.User>
{
    public GetUserByEmailWithOtpSpecification(string email)
    {
        AddCriteria(x => x.Email == email);
        AddInclude(nameof(Domain.Entities.User.Otp));
    }
}
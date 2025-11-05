using Common.Domain.Specification;

namespace UserManagement.Application.Specifications.OTP
{
    internal class GetOTPByCodeSpecification : Specification<Domain.Entities.OTP>
    {
        public GetOTPByCodeSpecification(string code)
        {
            AddCriteria(x => x.Code == code);
        }
    }
}

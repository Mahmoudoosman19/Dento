using Common.Domain.Specification;
using UserManagement.Application.Features.Auth.Commands.Register;

namespace UserManagement.Application.Specifications.Designer
{
    public class GetDesignerByEmailAndPhoneNumberSpecification : Specification<Domain.Entities.Designer>
    {
        public GetDesignerByEmailAndPhoneNumberSpecification(RegisterCommand registerDto)
        {
            AddInclude(nameof(Domain.Entities.User));
            AddCriteria(x => x.User.NormalizedEmail == registerDto.Designer!.Email || x.User.PhoneNumber == registerDto.Designer!.PhoneNumber);
        }
    }
}

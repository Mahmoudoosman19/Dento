using Common.Domain.Specification;
using UserManagement.Application.Features.Auth.Commands.ChangePhoneNumber;
using UserManagement.Application.Features.Auth.Commands.ChangeUserEmail;
using UserManagement.Application.Features.User.Queries.GetUsersData;

namespace UserManagement.Application.Specifications.User
{
    public class GetUsersDataSpecifications : Specification<Domain.Entities.User>
    {
        public GetUsersDataSpecifications(GetUsersDataQuery query)
        {
            AddCriteria(x => query.Ids.Contains(x.Id));
            AddInclude($"{nameof(Domain.Entities.User.Role)}");
        }
        public GetUsersDataSpecifications(ChangeUserEmailCommand command)
        {
            AddCriteria(x => x.Email == command.Email);
        }
        public GetUsersDataSpecifications(ChangePhoneNumberCommand command)
        {
            AddCriteria(x => x.PhoneNumber == command.PhoneNumber);
        }
    }
}

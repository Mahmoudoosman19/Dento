using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Auth.Commands.ChangePhoneNumber
{
    public class ChangePhoneNumberCommand:ICommand
    {
        public string PhoneNumber { get; set; }
    }
}

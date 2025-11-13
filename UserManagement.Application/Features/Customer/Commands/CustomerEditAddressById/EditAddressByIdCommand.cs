using Common.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Features.Customer.Commands.CustomerEditAddressById
{
    public class EditAddressByIdCommand : ICommand
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; }
        [Required(ErrorMessage = "الرقم مطلوب")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = " المدنيه مطلوبه")]
        public string City { get; set; }
        public string? Floor { get; set; }
        [Required(ErrorMessage = " العنوان مطلوب")]
        public string AddressName { get; set; }
    }
}

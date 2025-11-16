using Common.Application.Abstractions.Messaging;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLoginExtraDetails;

public class ExternalLoginExtraDetailsCommand : ICommand<LoginCommandResponse>
{
    public UserGender Gender { get; set; }
    
    private string _userName = null!;
    public string UserName
    {
        get => _userName;
        init => _userName = value?.Replace(" ", "");

    }
    public DateTime BirthDate { get;  set; }
    public string PhoneNumber { get; set; } = null!;
}
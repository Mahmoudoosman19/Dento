using Common.Application.Abstractions.Messaging;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;

namespace UserManagement.Application.Features.Auth.Commands.Register
{
    public sealed class RegisterCommand : ICommand
    {
        public AdminRegisterDto? Admin { get; init; }
        public DesignerRegisterDto? Designer { get; init; }
        public CustomerRegisterDto? Customer { get; init; }
        public SupervisorRegisterDto? Supervisor { get; init; }
        public DesignerRegisterType Type { get; init; }

        public RegisterCommand(SupervisorRegisterDto supervisor ,DesignerRegisterType type)
        {
            this.Supervisor = supervisor;   
            this.Type = type; }  
        public RegisterCommand(DesignerRegisterDto designer , DesignerRegisterType type)
        {
            this.Designer = designer;
            this.Type = type;
        }
        public RegisterCommand(CustomerRegisterDto customer, DesignerRegisterType type)
        {
            this.Customer = customer;
            this.Type = type;
        }
        public RegisterCommand()
        {
            
        }
    }

}

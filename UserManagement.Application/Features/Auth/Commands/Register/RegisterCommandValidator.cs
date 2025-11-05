using Common.Domain.Repositories;
using FluentValidation;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.Validators;
using UserManagement.Application.Identity;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(CustomUserManager userManager)
        {
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage(Messages.IncorrectData);

            When(x => x.Type == DesignerRegisterType.Admin, () =>
            {
                RuleFor(x => x.Admin)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new AdminRegisterDtoValidator(userManager)!);
            });

            When(x => x.Type == DesignerRegisterType.Designer, () =>
            {
                RuleFor(x => x.Designer)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new DesignerRegisterDtoValidator(userManager)!);
            });

            When(x => x.Type == DesignerRegisterType.Customer, () =>
            {
                RuleFor(x => x.Customer)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new CustomerRegisterDtoValidator(userManager)!);
            });

            When(x => x.Type == DesignerRegisterType.Supervisor, () =>
            {
                RuleFor(x => x.Supervisor)
                .NotNull().WithMessage(Messages.EmptyField)
                .SetValidator(new SupervisorRegisterDtoValidator(userManager)!);
            });
        }
    }
}

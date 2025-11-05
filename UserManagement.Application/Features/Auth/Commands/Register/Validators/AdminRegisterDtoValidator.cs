using FluentValidation;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;
using UserManagement.Application.Identity;

namespace UserManagement.Application.Features.Auth.Commands.Register.Validators
{
    internal class AdminRegisterDtoValidator : AbstractValidator<AdminRegisterDto>
    {
        public AdminRegisterDtoValidator(CustomUserManager userManager)
        {
            Include(new BaseRegisterDtoValidator(userManager));
        }
    }
}

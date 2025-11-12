namespace UserManagement.Application.Features.Auth.Commands.Register.Abstract
{
    internal interface IRegisterFactory
    {
        BaseRegister Register(RegisterType type);
    }
}
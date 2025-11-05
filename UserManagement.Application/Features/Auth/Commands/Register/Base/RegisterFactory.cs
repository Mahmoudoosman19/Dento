using UserManagement.Application.Features.Auth.Commands.Register.Abstract;

namespace UserManagement.Application.Features.Auth.Commands.Register.Base
{
    internal class RegisterFactory : IRegisterFactory
    {
        private readonly IEnumerable<BaseRegister> _baseRegisters;

        public RegisterFactory(IEnumerable<BaseRegister> baseRegisters)
        {
            _baseRegisters = baseRegisters;
        }
        public BaseRegister Register(DesignerRegisterType type)
        {
            var register = _baseRegisters.FirstOrDefault(r => r.Type == type);

            return register!;
        }
    }
}

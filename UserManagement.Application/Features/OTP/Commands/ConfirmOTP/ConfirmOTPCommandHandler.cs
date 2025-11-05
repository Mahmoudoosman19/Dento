using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP
{
    internal class ConfirmOTPCommandHandler : ICommandHandler<ConfirmOTPCommand>
    {
        private readonly IConfirmOTPFactory _confirmOTPFactory;

        public ConfirmOTPCommandHandler(IConfirmOTPFactory confirmOTPFactory)
        {
            _confirmOTPFactory = confirmOTPFactory;
        }
        public async Task<ResponseModel> Handle(ConfirmOTPCommand request, CancellationToken cancellationToken)
        {
            var confirmOTP = _confirmOTPFactory.ConfirmOTP(request.Type);

            return await confirmOTP.ConfirmOTP(request);
        }
    }
}

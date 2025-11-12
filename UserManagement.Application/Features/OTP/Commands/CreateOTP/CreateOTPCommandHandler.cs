using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using Microsoft.Extensions.Options;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Options;

namespace UserManagement.Application.Features.OTP.Commands.CreateOTP
{
    internal class CreateOTPCommandHandler : ICommandHandler<CreateOTPCommand>
    {
        private readonly OTPOptions _otpOptions;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateOTPCommandHandler(IOptions<OTPOptions> otpOptions, IGenericRepository<Domain.Entities.User> userRepo,
                                       IUnitOfWork unitOfWork,
                                       IEmailService emailService)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _otpOptions = otpOptions.Value;
        }
        public async Task<ResponseModel> Handle(CreateOTPCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Repository<Domain.Entities.User>().GetEntityWithSpec(new GetUserByEmailWithOtpSpecification(request.Email));
            var oldOtp = user!.Otp;

            var expirationTime = DateTime.UtcNow.AddMinutes(_otpOptions.ExpirationTimeInMinutes);

            
            var otp = new Domain.Entities.OTP
                (request.Type,
                 _otpOptions.CodeLength,
                expirationTime,
                request.Purpose);


            if (oldOtp is not null)
                _unitOfWork.Repository<Domain.Entities.OTP>().Delete(oldOtp);

            user!.SetOtp(otp);


            await _unitOfWork.CompleteAsync(cancellationToken);

            // Send Email
            //_emailService.SendEmail(user.Email!,
            //                        "OTP for Reset Password",
            //                        $"OTP is {otp.Code}");

            return ResponseModel.Success(new CreateOtpResponse() { OTP = otp, UserId = user.Id, Email = user.Email! });
            // return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}

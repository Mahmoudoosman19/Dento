namespace UserManagement.Application.Features.OTP.Commands.CreateOTP
{
    public class CreateOtpResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Domain.Entities.OTP OTP { get; set; }
    }

}

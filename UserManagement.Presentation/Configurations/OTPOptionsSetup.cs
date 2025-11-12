using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserManagement.Domain.Options;

namespace UserManagement.API.OptionsSetup
{
    public class OTPOptionsSetup : IConfigureOptions<OTPOptions>
    {
        private const string SectionName = "OTP";
        private readonly IConfiguration _configuration;

        public OTPOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(OTPOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}

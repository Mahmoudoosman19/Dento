using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Options;

namespace UserManagement.Presentation.OptionsSetup
{
    public class SmtpOptionsSetup: IConfigureOptions<SmtpOptions>
    {
        private readonly IConfiguration _configuration;
        private const string SectionName = "Smtp";
        public SmtpOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SmtpOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);

        }
    }
}

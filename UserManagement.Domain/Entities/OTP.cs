using Common.Domain.Primitives;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class OTP : Entity<Guid>, IAuditableEntity
    {
        public string Code { get; private set; } = null!;
        public OTPType Type { get; private set; }
        public DateTime ExpireOn { get; private set; }
        public bool IsUsed { get; private set; } = false;
        public string? Purpose { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public OTP() { }

        public OTP(OTPType type, int length, DateTime expireOn, string? purpose = null)
        {
            GenerateCode(length);
            Type = type;
            ExpireOn = expireOn;
            Purpose = purpose;
        }
        public OTP(string code, OTPType type, DateTime expireOn, string? purpose = null)
        {
            Code = code;
            Type = type;
            ExpireOn = expireOn;
            Purpose = purpose;
            CreatedOnUtc = DateTime.UtcNow;
            IsUsed = false;
        }

        public void GenerateCode(int length)
        {
            var random = new Random();
            const string chars = "0123456789";
            Code = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void SetType(OTPType type)
        {
            Type = type;
        }

        public void SetExpireOn(DateTime expireOn)
        {
            if (expireOn <= DateTime.UtcNow)
                throw new ArgumentException("ExpireOn must be a future date", nameof(expireOn));
            ExpireOn = expireOn;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        public void SetPurpose(string? purpose)
        {
            Purpose = purpose;
        }
    }
}

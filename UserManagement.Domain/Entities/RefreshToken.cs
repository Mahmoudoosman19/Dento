using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Domain.Entites;

namespace UserManagement.Domain.Entities
{
    public class RefreshToken : ValueObject, IAuditableEntity
    {
        public string Token { get; private set; } = null!;
        public DateTime ExpiresOn { get; private set; }
        public bool IsExpired => DateTime.Now >= ExpiresOn;
        public DateTime? RevokedOn { get; private set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public RefreshToken(string token, DateTime expiresOn, Guid userId)
        {
            Token = token;
            ExpiresOn = expiresOn;
            UserId = userId;
        }

        public void Revoke()
        {
            if (IsExpired)
            {
                throw new InvalidOperationException("Cannot revoke an expired token.");
            }

            RevokedOn = DateTime.Now;
        }

        public void ExtendExpiration(DateTime newExpiryDate)
        {
            if (newExpiryDate <= DateTime.Now)
            {
                throw new ArgumentException("New expiry date must be in the future.");
            }

            ExpiresOn = newExpiryDate;
        }
    }
}

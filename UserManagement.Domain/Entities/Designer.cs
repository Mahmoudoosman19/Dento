using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Domain.Entites;

namespace UserManagement.Domain.Entities
{
    public class Designer : Entity<Guid>, IAuditableEntity
    {
        public string BaseAvatarId { get; private set; }
        public string? CustomizedAvatarId { get; private set; }
        public Guid UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }


        public void SetBaseAvatarId(string baseAvatarId)
        {
            BaseAvatarId = baseAvatarId;
        }
        public void SetCustomizedAvatarId(string customizedAvatarId)
        {
            CustomizedAvatarId = customizedAvatarId;
        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}

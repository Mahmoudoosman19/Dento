using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Domain.Entites;

namespace UserManagement.Domain.Entities
{
    public class Designer : Entity<Guid>, IAuditableEntity
    {
        public Guid UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        
      
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

    }
}

using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Domain.Entites;

namespace UserManagement.Domain.Entities
{
    public class Notifications : Entity<Guid>, IAuditableEntity
    {
        public string Title { get; set; }     
        public string Content { get; set; }   
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public bool IsSeen { get; set; } = false;   
        public DateTime CreatedOnUtc { get; set ; }
        public DateTime? ModifiedOnUtc { get ; set ; }
    }
}

using Common.Domain.Primitives;

namespace UserManagement.Domain.Entities
{
    public class MetaData : Entity<Guid> , IAuditableEntity
    {
        public decimal ApplicationRate { get; set; }
        public DateTime CreatedOnUtc { get; set ; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}

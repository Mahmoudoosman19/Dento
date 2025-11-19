using Common.Domain.Primitives;

namespace UserManagement.Domain.Entities
{
    public class Permission : Entity<long>, IAuditableEntity
    {
        public string Name { get; private set; } = null!;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public Permission(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("English name cannot be null or empty", nameof(name));
            }

            Name = name;
        }
    }
}

using Common.Domain.Primitives;

namespace UserManagement.Domain.Entities
{
    public class Permission : Entity<long>, IAuditableEntity
    {
        public string NameAr { get; private set; } = null!;
        public string NameEn { get; private set; } = null!;
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public Permission(string nameAr, string nameEn)
        {
            SetName(nameAr, nameEn);
        }

        public void SetName(string nameAr, string nameEn)
        {
            if (string.IsNullOrWhiteSpace(nameAr))
            {
                throw new ArgumentException("Arabic name cannot be null or empty", nameof(nameAr));
            }

            if (string.IsNullOrWhiteSpace(nameEn))
            {
                throw new ArgumentException("English name cannot be null or empty", nameof(nameEn));
            }

            NameAr = nameAr;
            NameEn = nameEn;
        }
    }
}

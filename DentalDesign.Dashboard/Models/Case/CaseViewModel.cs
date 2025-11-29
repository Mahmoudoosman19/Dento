using Case.Domain.Enum;

namespace DentalDesign.Dashboard.Models.Case
{
    public class CaseViewModel
    {
        public Guid Id { get; set; }

        public string CaseName { get; set; }

        //public Guid CustomerId { get; set; }

        public Guid? DesignerId { get; set; }

        public string DesignerName { get; set; } 

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
        public long StatusId { get; set; }
        public string? StatusName
        {
            get
            {
                return Enum.GetName(typeof(CaseStatusEnum), StatusId);
            }
        }

        public DateTime DueDate { get; set; }

        public string? Description { get; set; }

        public long CaseType { get; set; }  
        public string? CaseTypeName
        {
            get
            {
                return Enum.GetName(typeof(CaseTypeEnum), CaseType);
            }
        }

        public DateTime AssignedAt { get; set; }
    }

}

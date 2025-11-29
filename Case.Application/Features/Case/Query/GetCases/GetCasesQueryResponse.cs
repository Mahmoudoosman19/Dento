using Case.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Application.Features.Case.Query.GetCases
{
    public class GetCasesQueryResponse
    {
        public Guid Id { get; set; }
        public string CaseName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? DesignertId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public long StatusId { get; set; }
        public DateTime DueDate { get; set; }
        public string? Description { get; set; }
        public long CaseType { get; set; }
        public DateTime AssignedAt { get; set; }

    }
}

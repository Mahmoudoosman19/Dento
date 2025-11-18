using Case.Domain.Enum;
using Common.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain.Entities
{
    public class Case : AggregateRoot<Guid>, IAuditableEntity
    {
        public string CaseName { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? DesignertId { get; private set; }
        public DateTime CreatedOnUtc { get; set; } = DateTime.Now;
        public DateTime? ModifiedOnUtc { get; set; }
        public long StatusId { get; private set; }
        public DateTime DueDate { get;  private set; }
        public string? Description {  get; private set; }
        public CaseTypeEnum CaseType { get; private set; }

        public void SetCustomerId(Guid customerId)
        {
            CustomerId = customerId;
        }
        public void SetCaseName(string caseName)
        {
            CaseName = caseName;
        }
        public void SetDesignertId(Guid designertId)
        {
            DesignertId = designertId;
        }
        public void SetStatus(CaseStatusEnum status)
        {
            StatusId = (long)status;    
        }
        public void SetDescription (string description)
        {
            Description= description;
        }
        public void SetCaseType(CaseTypeEnum caseType)
        {
            CaseType = caseType;
        }
        public void SetDueDate(DateTime dueDate)
        {
            DueDate = dueDate;
        }
    }
}

using Common.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Enums;

namespace Task.Domain.Entities
{
    public class CaseTask : AggregateRoot<Guid>, IAuditableEntity
    {
        public Guid CaseId { get; private set; }          
        public Guid DesignerId { get; private set; }     

        public DateTime? StartedAt { get; private set; }   
        public DateTime? EndedAt { get; private set; }   

        public TaskStatusEnum Status { get; private set; } = TaskStatusEnum.Pending;

        public string? Notes { get; private set; }    

        public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOnUtc { get; set; }

        private CaseTask() { } 

        public CaseTask(Guid caseId, Guid designerId)
        {
            CaseId = caseId;
            DesignerId = designerId;
            Status = TaskStatusEnum.Pending;
        }


        public void StartTask()
        {
            if (Status != TaskStatusEnum.Pending)
                throw new InvalidOperationException("Task cannot be started. It is already in progress or completed.");

            Status = TaskStatusEnum.InProgress;
            StartedAt = DateTime.UtcNow;
        }

        public void CompleteTask()
        {
            if (Status != TaskStatusEnum.InProgress)
                throw new InvalidOperationException("Only tasks InProgress can be completed.");

            Status = TaskStatusEnum.Completed;
            EndedAt = DateTime.UtcNow;
        }

        public void AddNotes(string notes)
        {
            Notes = notes;
            ModifiedOnUtc = DateTime.UtcNow;
        }

    }
}

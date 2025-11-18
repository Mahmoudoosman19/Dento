using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain.Enum
{
    public enum CaseStatusEnum
    {
        New = 1,
        Assigned,
        Active,
        Submitted,
        Reviewed,
        Approved,
        Canceled,
        Closed,
        Reopened
        
    }
}

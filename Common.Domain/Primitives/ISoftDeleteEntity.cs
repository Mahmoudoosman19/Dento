using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Primitives
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; }
        public DateTime? DeletedAt { get; }
        public DateTime? RestoredAt { get; }
    }
}

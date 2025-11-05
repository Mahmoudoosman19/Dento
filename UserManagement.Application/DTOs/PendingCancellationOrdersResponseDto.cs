using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.SharedDTOs
{
    public class PendingCancellationOrdersResponseDto
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }


        public DateTime DateOfRequest { get; set; }

        public string Status { get; set; }
        public string ReturnedReason { get; set; }
    }
}

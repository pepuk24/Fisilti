using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Completed,
        Failed,
        Refunded
    }
}

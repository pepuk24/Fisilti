using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SubscriptionDTO
    {
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public SubscriptionType Type { get; set; } // 2
    }
}

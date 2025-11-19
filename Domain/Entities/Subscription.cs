using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public int AppUserId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public SubscriptionType Type { get; set; } // 2

        public AppUser AppUser { get; set; }

    }
}

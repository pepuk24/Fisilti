using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Purchase : BaseEntity
    {
        
        public int AppUserId { get; set; }
        public int PromptId { get; set; }
        public int PaymentId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        //Navigation Property: İlişki kurulurken yol gösterici property, yani hangi tabloyla ilişki kurulacağını belirtir.
        public AppUser AppUser { get; set; }
        public Prompt Prompt { get; set; }
        public Payment Payment { get; set; }

    }
}

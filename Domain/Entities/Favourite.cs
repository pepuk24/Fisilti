using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Favourite : BaseEntity
    {
        public int AppUserId { get; set; }
        public int PromptId { get; set; } // 7

        public Prompt Prompt { get; set; }
        public AppUser AppUser { get; set; }

    }
}

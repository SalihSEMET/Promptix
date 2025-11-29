using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public int AppUserId { get; set; }
        public int PromptId { get; set; }


        // Navigation Properties
        public AppUser AppUser { get; set; }
        public Prompt Prompt { get; set; }
    }
}

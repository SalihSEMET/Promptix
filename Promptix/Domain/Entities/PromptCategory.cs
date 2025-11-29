using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PromptCategory : BaseEntity
    {
        public int PromptId { get; set; }
        public int CategoryId { get; set; }


        // Navigation Properties
        public Prompt Prompt { get; set; }
        public Category Category { get; set; }


    }
}

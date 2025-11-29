using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }


        // Navigation Properties
        public ICollection<PromptCategory> PromptCategories { get; set; }
    }
}

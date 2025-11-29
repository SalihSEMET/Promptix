using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Prompt : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; } // Asıl Prompt İçeriği
        public decimal Price { get; set; }


        // Navigation Properties
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<PromptCategory> PromptCategories { get; set; }


    }
}

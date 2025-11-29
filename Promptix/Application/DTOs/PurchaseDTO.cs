using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int PromptId { get; set; }
        public int PaymentId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}

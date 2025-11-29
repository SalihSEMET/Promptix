using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PromptCategoryDTO
    {
        public int Id { get; set; }
        public int PromptId { get; set; }
        public int CategoryId { get; set; }
    }
}

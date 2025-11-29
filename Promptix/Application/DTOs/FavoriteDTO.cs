using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FavoriteDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int PromptId { get; set; }
    }
}

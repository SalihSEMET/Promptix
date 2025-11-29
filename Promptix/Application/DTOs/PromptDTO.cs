using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    //DTO (Data Transfer Object): Veritabanındaki bütün verileri çekip kullanıcıya göstermek istemeyebiliriz, veya bazen bir tabloyu başka bir tabloyla birleştirip iki tablodan da sadece göstermek istediğimiz verileri göstermek isteyebiliriz, bu tarz durumlarda Filtrelenmiş temizlenmiş class lara ihtiyaç duyarız işte bunlara DTO deriz.
    public class PromptDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}

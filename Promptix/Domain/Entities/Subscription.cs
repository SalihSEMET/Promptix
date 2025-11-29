using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public int AppUserId { get; set; }
        public SubscriptionType Type { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }




        // Navigation Properties(Navigasyon Propertyleri): Tablolar arası ilişki kurulurken hangi tablo ile hangi tablo arasında bir ilişki var bunu belirtmemizi ve tablolar arası ilişkileri kurmamızı sağlayan property türüdür.

        // Navigation Properties
        public AppUser AppUser { get; set; }



    }
}

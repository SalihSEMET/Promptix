using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //Entitylerin temeli, yani bütün entitylerin kalıtım aldığı en temel class. İçerisindeki özellikler ise bütün entitylerde bulunması gereken ve kalıtım yoluya aktarılacak olan özellikler.
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }


        // Core + Entity            = Domain
        // DataAccess               = Infrastructure
        // Business Logic           = Application
        // API + UI(User Interface) = Presentation(Sunum)


        //VERİTABANINDAKİ TABLOLARIMIZ
        //-----------------------------------------------------------------------------------

        // Users          : Kullanıcı bilgileri, roller, abonelik durumu
        // Prompts        : Prompt içeriği, kategori, açıklama, fiyat
        // Categories     : Prompt kategorileri(“Tasarım”, “Yazılım”, “Blog İçeriği” vb.)
        // Purchases      : Kullanıcı hangi promptu satın almış, tarih ve ödeme bilgisi
        // Subscriptions  : Kullanıcının abonelik tipi, başlangıç ve bitiş tarihi
        // Payments Ödeme : Ödeme kayıtları(mock ya da gerçek gateway entegrasyonu)
        // Favorites      : Kullanıcının favoriye eklediği promptlar
        // AuditLogs      : Loglama, işlem geçmişi
    }
}

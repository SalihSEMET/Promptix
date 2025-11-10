namespace Domain.Entities;

public class Purchase : BaseEntity
{
    public int      AppUserId { get; set; }
    public int      PromptId { get; set; }
    public int      PaymentId { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
    //Navigation Properties
    public AppUser AppUser { get; set; }
    public Prompt Prompt { get; set; }
    public Payment Payment { get; set; }
}
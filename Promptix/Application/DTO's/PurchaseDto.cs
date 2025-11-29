namespace Application.DTO_s;

public class PurchaseDto
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int PromptId { get; set; }
    public int PaymentId { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
}
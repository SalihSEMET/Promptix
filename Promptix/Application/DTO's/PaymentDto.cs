using Domain.Enums;

namespace Application.DTO_s;

public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionId { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
}
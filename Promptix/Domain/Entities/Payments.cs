using Domain.Enums;

namespace Domain.Entities;

public class Payments : BaseEntity
{
    public decimal       Amount { get; set; }
    public string        TransactionId { get; set; } = "";
    public DateTime      PaymentDate { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
}
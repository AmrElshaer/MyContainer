using MyContainer.Enums;

namespace MyContainer.Data;

public class Transaction
{
    public long Id { get; set; }
    public int? ContainerId { get; set; }
    public Container? Container { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }

}
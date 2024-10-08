namespace MyContainer.Data;

public class Transaction
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal DebitAmount { get; set; }

}
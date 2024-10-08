namespace MyContainer.Data;

public class User
{
    public User()
    {
        Transactions = new List<Transaction>();
    }
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
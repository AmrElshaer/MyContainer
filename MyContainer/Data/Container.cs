namespace MyContainer.Data;

public class Container
{
    public int Id { get; set; }
    public string ContainerType { get; set; } = default!;
    public decimal ContainerPrice { get; set; }
    public string Number { get; set; } = default!;
    public decimal LoadingPrice { get; set; }
    public DateTime LoadingTime { get; set; }=DateTime.Now;
    public int NumberOfWoodenBoards { get; set; }
    public decimal PriceOfWoodenBoard { get; set; }
    public int NumberOfBoxes { get; set; }
    public decimal BoxPrice { get; set; }
    public int NumberOfStretch { get; set; }
    public decimal  PriceOfStretch{ get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Remaining { get; set; }
    public decimal Arrive { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
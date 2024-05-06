namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public string Type { get; set; }
    public Customer Customer { get; set; }
    public List<Transaction> Transactions { get; set; }
}

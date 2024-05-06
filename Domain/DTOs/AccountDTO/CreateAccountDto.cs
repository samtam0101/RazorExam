namespace Domain.DTOs.AccountDTO;

public class CreateAccountDto
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public string Type { get; set; }
}

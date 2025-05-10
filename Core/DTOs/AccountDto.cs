namespace Core.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = null!;
}
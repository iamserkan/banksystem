namespace Core.DTOs;

public class CreateTransactionDto
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!; // Deposit, Withdraw, Transfer
    public string? Description { get; set; }
    public int? TargetAccountId { get; set; } // Transfer için
}
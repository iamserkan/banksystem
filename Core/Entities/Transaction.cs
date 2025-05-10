namespace Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public string Type { get; set; } = null!; // Deposit, Withdraw, Transfer
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
}
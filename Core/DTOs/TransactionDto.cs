namespace Core.DTOs;

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
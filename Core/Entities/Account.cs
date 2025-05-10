using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Account
{
    public int Id { get; set; }
    
    [Required]
    public string AccountNumber { get; set; } = null!;
    
    public decimal Balance { get; set; }
    public string Currency { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;


    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
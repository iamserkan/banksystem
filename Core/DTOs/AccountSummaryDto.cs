namespace Core.DTOs;

public class AccountSummaryDto
{
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public decimal CurrentBalance { get; set; }
}
namespace Core.DTOs;

public class TransactionFilterDto
{
    public int PageNumber { get; set; } = 1;  
    public int PageSize { get; set; } = 5; 
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
}
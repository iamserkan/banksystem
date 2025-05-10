using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly BankDbContext _context;

    public TransactionService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto> DepositAsync(CreateTransactionDto dto, int userId)
    {
        var account = await _context.Accounts.FindAsync(dto.AccountId);
        if (account == null) throw new Exception("Account not found");
        if (account.UserId != userId) throw new Exception("Bu hesaba işlem yapamazsınız.");

        account.Balance += dto.Amount;

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            Type = "Deposit",
            Description = dto.Description
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return MapToDto(transaction);
    }

    public async Task<TransactionDto> WithdrawAsync(CreateTransactionDto dto, int userId)
    {
        var account = await _context.Accounts.FindAsync(dto.AccountId);
        if (account == null) throw new Exception("Account not found");
        if (account.UserId != userId) throw new Exception("Bu hesaba işlem yapamazsınız.");
        if (account.Balance < dto.Amount) throw new Exception("Insufficient balance");

        account.Balance -= dto.Amount;

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            Type = "Withdraw",
            Description = dto.Description
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return MapToDto(transaction);
    }

    public async Task<TransactionDto> TransferAsync(CreateTransactionDto dto, int userId)
    {
        if (!dto.TargetAccountId.HasValue) throw new ArgumentNullException("Target account required");

        var sender = await _context.Accounts.FindAsync(dto.AccountId);
        var receiver = await _context.Accounts.FindAsync(dto.TargetAccountId.Value);

        if (sender == null || receiver == null) throw new Exception("Account not found");
        if (sender.UserId != userId) throw new Exception("Bu hesaba işlem yapamazsınız.");
        if (sender.Currency != receiver.Currency) throw new Exception("Para birimleri uyuşmuyor.");
        if (sender.Balance < dto.Amount) throw new Exception("Insufficient balance");

        sender.Balance -= dto.Amount;
        receiver.Balance += dto.Amount;

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            Type = "Transfer",
            Description = $"To: {receiver.AccountNumber}"
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return MapToDto(transaction);
    }

    public async Task<List<TransactionDto>> GetAccountTransactionsAsync(int accountId, int userId, TransactionFilterDto? filter)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null || account.UserId != userId)
            throw new Exception("Bu hesaba işlem geçmişi görüntüleyemezsiniz.");

        var query = _context.Transactions
            .Where(t => t.AccountId == accountId)
            .AsQueryable();

        if (filter?.StartDate != null)
            query = query.Where(t => t.Date >= filter.StartDate);
        if (filter?.EndDate != null)
            query = query.Where(t => t.Date <= filter.EndDate);
        if (filter?.MinAmount != null)
            query = query.Where(t => t.Amount >= filter.MinAmount);
        if (filter?.MaxAmount != null)
            query = query.Where(t => t.Amount <= filter.MaxAmount);
        if (filter != null)
        {
            query = query
                .OrderByDescending(t => t.Date)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }

        return await query
            .OrderByDescending(t => t.Date)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                Description = t.Description,
                Date = t.Date
            }).ToListAsync();
    }
    public async Task<AccountSummaryDto> GetAccountSummaryAsync(int accountId, int userId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null || account.UserId != userId)
            throw new Exception("Bu hesaba erişemezsiniz.");

        // Veriyi belleğe al
        var transactions = await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .ToListAsync();

        var deposits = transactions
            .Where(t => t.Type == "Deposit")
            .Sum(t => t.Amount);

        var withdrawals = transactions
            .Where(t => t.Type == "Withdraw")
            .Sum(t => t.Amount);

        return new AccountSummaryDto
        {
            TotalDeposits = deposits,
            TotalWithdrawals = withdrawals,
            CurrentBalance = account.Balance
        };
    }



    private static TransactionDto MapToDto(Transaction t)
    {
        return new TransactionDto
        {
            Id = t.Id,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description,
            Date = t.Date
        };
    }
}

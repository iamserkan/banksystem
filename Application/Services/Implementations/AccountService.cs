using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly BankDbContext _context;

    public AccountService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<List<AccountDto>> GetUserAccountsAsync(int userId)
    {
        return await _context.Accounts
            .Where(a => a.UserId == userId)
            .Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
                Currency = a.Currency
            })
            .ToListAsync();
    }

    public async Task<AccountDto> CreateAccountAsync(int userId, CreateAccountDto dto)
    {
        var account = new Account
        {
            AccountNumber = "TR" + Guid.NewGuid().ToString("N")[..10].ToUpper(),
            Balance = 0,
            Currency = dto.Currency,
            UserId = userId
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Currency = account.Currency
        };
    }

    public async Task<decimal> GetBalanceAsync(int accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null) throw new Exception("Account not found");

        return account.Balance;
    }
}
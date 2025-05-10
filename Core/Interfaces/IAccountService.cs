using Core.DTOs;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<List<AccountDto>> GetUserAccountsAsync(int userId);
    Task<AccountDto> CreateAccountAsync(int userId, CreateAccountDto dto);
    Task<decimal> GetBalanceAsync(int accountId);
}
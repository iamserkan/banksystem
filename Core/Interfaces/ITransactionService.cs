using Core.DTOs;

namespace Core.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> DepositAsync(CreateTransactionDto dto, int userId);
    Task<TransactionDto> WithdrawAsync(CreateTransactionDto dto, int userId);
    Task<TransactionDto> TransferAsync(CreateTransactionDto dto, int userId);
    Task<List<TransactionDto>> GetAccountTransactionsAsync(int accountId, int userId, TransactionFilterDto? filter);
    Task<AccountSummaryDto> GetAccountSummaryAsync(int accountId, int userId);

}
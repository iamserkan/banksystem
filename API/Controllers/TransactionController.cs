using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] CreateTransactionDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.DepositAsync(dto, userId);
        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] CreateTransactionDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.WithdrawAsync(dto, userId);
        return Ok(result);
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] CreateTransactionDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.TransferAsync(dto, userId);
        return Ok(result);
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetTransactions(int accountId, [FromQuery] TransactionFilterDto filter)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.GetAccountTransactionsAsync(accountId, userId, filter);
        return Ok(result);
    }


    [HttpPost("{accountId}/filter")]
    public async Task<IActionResult> FilteredTransactions(int accountId, [FromBody] TransactionFilterDto filter)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.GetAccountTransactionsAsync(accountId, userId, filter);
        return Ok(result);
    }
    [HttpGet("{accountId}/summary")]
    public async Task<IActionResult> GetAccountSummary(int accountId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.GetAccountSummaryAsync(accountId, userId);
        return Ok(result);
    }


}
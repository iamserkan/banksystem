using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence;

namespace API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly BankDbContext _context;

    public AdminController(BankDbContext context)
    {
        _context = context;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users
            .Select(u => new { u.Id, u.FullName, u.Email, u.Role, u.CreatedAt })
            .ToList();

        return Ok(users);
    }

    [HttpGet("accounts")]
    public IActionResult GetAccounts()
    {
        var accounts = _context.Accounts
            .Select(a => new { a.Id, a.AccountNumber, a.Balance, a.UserId })
            .ToList();

        return Ok(accounts);
    }

    [HttpGet("transactions")]
    public IActionResult GetTransactions()
    {
        var txs = _context.Transactions
            .Select(t => new { t.Id, t.AccountId, t.Amount, t.Type, t.Description, t.Date })
            .ToList();

        return Ok(txs);
    }
}
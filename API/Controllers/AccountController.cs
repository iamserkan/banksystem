using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly BankDbContext _context;
    private readonly IAccountService _accountService;

    public AccountController(BankDbContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetMyAccounts()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var accounts = await _context.Accounts
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return Ok(accounts);
    }

    //  Admine özel tüm hesapları listeler
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await _context.Accounts
            .Include(a => a.User)
            .ToListAsync();

        return Ok(accounts);
    }

    // Hesap oluştur 
    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _accountService.CreateAccountAsync(userId, dto);
        return Ok(result);
    }
}
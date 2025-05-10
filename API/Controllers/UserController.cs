using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Sadece Admin erişebilir
public class UsersController : ControllerBase
{
    private readonly BankDbContext _context;

    public UsersController(BankDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("{id}/accounts")]
    public async Task<IActionResult> GetUserAccounts(int id)
    {
        var user = await _context.Users
            .Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return NotFound();
        return Ok(user.Accounts);
    }
}
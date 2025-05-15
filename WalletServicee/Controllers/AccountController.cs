using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletServicee.Database;

namespace WalletServicee.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public AccountController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _context.Users.ToListAsync());
    
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

}
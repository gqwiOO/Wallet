using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletServicee.Database;
using WalletServicee.Models.Users;
using WalletServicee.Services;

namespace WalletServicee.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private IUserService _userService;

    public UserController(AppDbContext context, IUserService userService)
    {
        _userService = userService;
        _context = context;
    }
    
    [HttpGet("accounts")]
    public async Task<IActionResult> Get() => Ok(await _context.Users.ToListAsync());
    
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _context.Users.Add(user);
        Wallet wallet = new Wallet()
        {
            Currency = "USDT",
            Balance = 0,
        };
        wallet.UserId = user.Id;
        wallet.User = user;
        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public User GetUserById([FromRoute] int id)
    {
        return _userService.GetUserById(id);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}
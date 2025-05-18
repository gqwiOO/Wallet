using System.Security.Claims;
using WalletServicee.Database;
using WalletServicee.Models.Users;

namespace WalletServicee.Services;

public class UserService : IUserService
{
    private AppDbContext _context;
    private IHttpContextAccessor _httpContextAccessor;

    public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
    
    public User GetCurrentUser()
    {
        int userIdClaim = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        User? currentUser = _context.Users.Find(userIdClaim);
        return currentUser;
    }

    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(x => x.Username == username);
    }

    public User CreateUser(User user)
    {
        return user;
    }
}
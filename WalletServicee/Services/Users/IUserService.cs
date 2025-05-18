using Microsoft.EntityFrameworkCore;
using WalletServicee.Models.Users;

namespace WalletServicee.Services;

public interface IUserService
{
    User GetCurrentUser();
    User GetUserById(int id);
    User GetUserByUsername(string username);
    
    User CreateUser(User user);
}
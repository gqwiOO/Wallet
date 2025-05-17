using Microsoft.EntityFrameworkCore;
using WalletServicee.Models.Users;

namespace WalletServicee.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Wallet> Wallets { get; set; }
}
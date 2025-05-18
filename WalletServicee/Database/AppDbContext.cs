using Microsoft.EntityFrameworkCore;
using WalletServicee.Controllers.Operations;
using WalletServicee.Models.Users;

namespace WalletServicee.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<OperationData> OperationsHistory { get; set; }
}
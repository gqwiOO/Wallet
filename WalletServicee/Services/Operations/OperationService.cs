using WalletServicee.Database;
using WalletServicee.Services;

namespace WalletServicee.Controllers.Operations;

public class OperationService : IOperationService
{
    private AppDbContext _context;
    private IUserService _userService;

    public OperationService(AppDbContext context, IUserService userService)
    {
        _userService = userService;
        _context = context;
    }
    public IEnumerable<OperationData> GetAllUserOperations(int userId)
    {
        return _context.OperationsHistory.Where(item => item.UserId == userId);
    }

    public IEnumerable<OperationData> GetAllCurrentUserOperations()
    {
        return _context.OperationsHistory.Where(item => item.UserId == _userService.GetCurrentUser().Id);
    }

    public void AddOperation(OperationData operation)
    {
        _context.OperationsHistory.Add(operation);
        _context.SaveChanges();
    }
}
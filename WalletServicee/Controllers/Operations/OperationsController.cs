using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletServicee.Database;
using WalletServicee.Services.Wallets;

namespace WalletServicee.Controllers.Operations;

[Authorize]
[ApiController]
[Route("[controller]")]
public class OperationsController: ControllerBase
{
    private AppDbContext _dbContext;
    private IWalletService _walletService;
    private IOperationService _operationService;

    public OperationsController(AppDbContext dbContext, IWalletService walletService, IOperationService operationService)
    {
        _operationService = operationService;
        _walletService = walletService;
        _dbContext = dbContext;
    }
    
    
    [HttpGet("userOperations/{userId}")]
    public IActionResult GetAllUserOperations([FromRoute]int userId)
    {
        IEnumerable<OperationData> allUserOperations = _operationService.GetAllUserOperations(userId);
        return Ok(allUserOperations);
    }
}
namespace WalletServicee.Controllers.Operations;

public interface IOperationService
{
    IEnumerable<OperationData> GetAllUserOperations(int userId);
    IEnumerable<OperationData> GetAllCurrentUserOperations();
    
    void AddOperation(OperationData operation);
}
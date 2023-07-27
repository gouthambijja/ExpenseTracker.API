using ExpenseTracker.BLL.RequestModels;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Contracts
{
    public interface ITransactionService
    {
        Task<(BLTransaction? transaction, string ErrorMsg)> Add(BLTransaction transaction);
        Task<(BLTransaction? transaction, string ErrorMsg)> Update(BLTransaction transaction);
        Task<(BLTransaction? transaction, string ErrorMsg)> Restore(BLTransaction transaction);
        Task<(BLTransaction? transaction, string ErrorMsg)> Delete(Guid transactionId);
        Task<(BLTransaction? transaction, string ErrorMsg)> DeletePermanently(Guid transactionId);
        Task<(BLTransaction? transaction, string ErrorMsg)> Get(BLTransaction transaction);
        Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetAll(Guid UserId);
        Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetBinAll(Guid UserId);
        public Task<(bool isSuccess, string ErrorMsg)> AddMany(List<BLTransaction> transactions);
        public Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetFiltered(TransactionsFilterModel filters);
        public Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetFilteredByDateRange(Guid UserId, DateTime StartDate, DateTime EndDate);
        public Task<(bool isSuccess, string ErrorMsg)> DeleteMultiple(List<Guid> UserIds);
    }
}

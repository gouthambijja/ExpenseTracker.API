using ExpenseTracker.DAL.Models;

namespace ExpenseTracker.DAL.Contracts
{
    public interface ITransactionRepository
    {
        Task<(Transaction? transaction, string ErrorMsg)> Add(Transaction transaction);
        Task<(Transaction? transaction, string ErrorMsg)> Update(Transaction transaction);
        Task<(Transaction? transaction, string ErrorMsg)> Restore(Transaction transaction);
        Task<(Transaction? transaction, string ErrorMsg)> Delete(Guid transactionId);
        Task<(Transaction? transaction, string ErrorMsg)> DeletePermanently(Guid transactionId);
        Task<(Transaction? transaction, string ErrorMsg)> Get(Transaction transaction);
        Task<(List<Transaction>? transactions, string ErrorMsg)> GetAll(Guid UserId);
        Task<(List<Transaction>? transactions, string ErrorMsg)> GetBinAll(Guid UserId);
        public Task<(bool isSuccess, string ErrorMsg)> AddMany(List<Transaction> transactions);
        public Task<(List<Transaction>? transactions, string ErrorMsg)> GetFiltered(Guid UserId, string? Name, Guid? Category, string? Description, DateTime? StartDate, DateTime? EndDate);
        public Task<(List<Transaction>? transactions, string ErrorMsg)> GetFilteredByDateRange(Guid UserId, DateTime StartDate, DateTime EndDate);
        public Task<(bool isSuccess, string ErrorMsg)> DeleteMultiple(List<Guid> UserIds);
    }
}

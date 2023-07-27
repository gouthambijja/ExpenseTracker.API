
using AutoMapper;
using ExpenseTracker.BLL.RequestModels;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Contracts;
using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        Mapper mapper = AutoMappers.InitializeAutoMapper();
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<(BLTransaction? transaction, string ErrorMsg)> Add(BLTransaction transaction)
        {
            try
            {
                var _transaction = await _transactionRepository.Add(mapper.Map<Transaction>(transaction));
                return (mapper.Map<BLTransaction>(_transaction.transaction),_transaction.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> AddMany(List<BLTransaction> transactions)
        {
            try
            {                
               return await _transactionRepository.AddMany(mapper.Map<List<Transaction>>(transactions));  
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(BLTransaction? transaction, string ErrorMsg)> Delete(Guid id)
        {
            try
            {
                var _transaction = await _transactionRepository.Delete(id);
                return (mapper.Map<BLTransaction>(_transaction.transaction), _transaction.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
            }
        }
        public async Task<(BLTransaction? transaction, string ErrorMsg)> DeletePermanently(Guid id)
        {
            try
            {
                var _transaction = await _transactionRepository.DeletePermanently(id);
                return (mapper.Map<BLTransaction>(_transaction.transaction), _transaction.ErrorMsg);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }



        public async Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetAll(Guid UserId)
        {
            try
            {
                var transactions = await _transactionRepository.GetAll(UserId);
                return (mapper.Map<List<BLTransaction>>(transactions.transactions), transactions.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
            }
        }
        public async Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetBinAll(Guid UserId)
        {
            try
            {
                var transactions = await _transactionRepository.GetBinAll(UserId);
                return (mapper.Map<List<BLTransaction>>(transactions.transactions), transactions.ErrorMsg);

            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetFiltered(TransactionsFilterModel Filters)
        {
            try
            {
                var transactions = await _transactionRepository.GetFiltered(Filters.UserId,Filters. Name, Filters.Category, Filters.Description, Filters.StartDate,Filters. EndDate);
                return (mapper.Map<List<BLTransaction>>(transactions.transactions), transactions.ErrorMsg);

            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(List<BLTransaction>? transactions, string ErrorMsg)> GetFilteredByDateRange(Guid UserId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var transactions = await _transactionRepository.GetFilteredByDateRange(UserId, StartDate, EndDate);
                return (mapper.Map<List<BLTransaction>>(transactions.transactions), transactions.ErrorMsg);

            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(BLTransaction? transaction, string ErrorMsg)> Restore(BLTransaction transaction)
        {
            try
            {
                Console.WriteLine("restore-");
                var _transaction = await _transactionRepository.Restore(mapper.Map<Transaction>(transaction));
                return (mapper.Map<BLTransaction>(_transaction.transaction), _transaction.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
            }
        }

        public async Task<(BLTransaction? transaction, string ErrorMsg)> Update(BLTransaction transaction)
        {
            try
            {
                var _transaction = await _transactionRepository.Update(mapper.Map<Transaction>(transaction));
                return (mapper.Map<BLTransaction>(_transaction.transaction),_transaction.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> DeleteMultiple(List<Guid> TransactionIds)
        {
            try
            {
                var response = await _transactionRepository.DeleteMultiple(TransactionIds);
                return (response.isSuccess, response.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (false,ex.Message);
            }
        }

        Task<(BLTransaction? transaction, string ErrorMsg)> ITransactionService.Get(BLTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}

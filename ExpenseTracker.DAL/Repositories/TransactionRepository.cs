using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseTrackerContext Context;
        public TransactionRepository(ExpenseTrackerContext DBContext)
        {
            Context = DBContext;
        }

        public async Task<(Transaction? transaction, string ErrorMsg)> Add(Transaction transaction)
        {
            try
            {
                Context.Transactions.Add(transaction);
                await Context.SaveChangesAsync();
                await Context.Entry(transaction).GetDatabaseValuesAsync();
                return (transaction, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(bool isSuccess,string ErrorMsg)> AddMany(List<Transaction> transactions)
        {
            try
            {
                await Context.Transactions.AddRangeAsync(transactions);
                await Context.SaveChangesAsync();
                return (true,"");
            }
            catch(Exception ex)
            {
                return (false,ex.Message);
            }
        }

        public async Task<(Transaction? transaction, string ErrorMsg)> Delete(Guid transactionId)
        {
            try
            {
                var _Transaction = await Context.Transactions.
                        Where(e => e.TransactionId == transactionId).FirstOrDefaultAsync();
                if (_Transaction == null) return (null, "Transaction not available,Deletion Failed.");
                _Transaction.IsActive = false;
                _Transaction.UpdatedAt = DateTime.Now;
                await Context.SaveChangesAsync();
                return (_Transaction, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(Transaction? transaction, string ErrorMsg)> DeletePermanently(Guid transactionId)
        {
            try
            {
                var _Transaction = await Context.Transactions.
                        Where(e => e.TransactionId == transactionId).FirstOrDefaultAsync();
                if (_Transaction == null) return (null, "Transaction Not Available,Deletion Failed!");
                _Transaction.IsActive = false;
                _Transaction.UpdatedAt = DateTime.Now;
                _Transaction.IsPermanentDelete = true;
                await Context.SaveChangesAsync();
                return (_Transaction, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }

        }
        public async Task<(Transaction? transaction, string ErrorMsg)> Restore(Transaction transaction)
        {
            try
            {
                var _Transaction = await Context.Transactions.
                        Where(e => e.TransactionId == transaction.TransactionId).FirstOrDefaultAsync();
                if (_Transaction == null) return (null, "Transaction not Available,Restoration Failed");
                _Transaction.IsActive = true;
                _Transaction.UpdatedAt = DateTime.Now;
                await Context.SaveChangesAsync();
                return (_Transaction, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Transaction? transaction,string ErrorMsg)> Get(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<Transaction>? transactions, string ErrorMsg)> GetAll(Guid UserId)
        {
            try
            {
                var _Transactions = await Context.Transactions
                    .Where(e => e.UserId == UserId && e.IsActive == true).OrderByDescending(e => e.UpdatedAt).ToListAsync();
                Console.WriteLine(_Transactions.Count());
                return (_Transactions, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(List<Transaction>? transactions, string ErrorMsg)> GetBinAll(Guid UserId)
        {
            try
            {
                var _Transactions = await Context.Transactions
                    .Where(e => e.UserId == UserId && e.IsActive == false && e.IsPermanentDelete == false).OrderByDescending(e => e.Date).ToListAsync();
                Console.WriteLine(_Transactions.Count());
                return (_Transactions, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(List<Transaction>? transactions, string ErrorMsg)> GetFiltered(Guid UserId, string? Name, Guid? CategoryId, string? Description, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {

                var _Transactions = await Context.Transactions
                    .Where(e => e.UserId == UserId && e.IsActive == true && ((Name != "none") ? e.Name == Name : true) && ((CategoryId != null) ? e.CategoryId == CategoryId : true) && ((Description != "none") ? e.Description == Description : true) && e.Date >= StartDate && e.Date <= EndDate).ToListAsync();
                return (_Transactions, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(List<Transaction>? transactions, string ErrorMsg)> GetFilteredByDateRange(Guid UserId, DateTime StartDate, DateTime EndDate)
        {
            Console.WriteLine(EndDate);
            try
            {
                var _Transactions = await Context.Transactions
                    .Where(e => e.UserId == UserId && e.Date >= StartDate && e.Date <= EndDate).OrderByDescending(e => e.Date).ToListAsync();
                return (_Transactions, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Transaction? transaction, string ErrorMsg)> Update(Transaction transaction)
        {
            try
            {
                var _Transaction = await Context.Transactions.
                    Where(e => e.TransactionId == transaction.TransactionId).FirstOrDefaultAsync();
                _Transaction.Name = transaction.Name;
                _Transaction.Description = transaction.Description;
                _Transaction.Amount = transaction.Amount;
                _Transaction.CategoryId = transaction.CategoryId;
                _Transaction.Date = transaction.Date;
                _Transaction.UpdatedAt = DateTime.Now;
                await Context.SaveChangesAsync();
                return (_Transaction, "");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(bool isSuccess, string ErrorMsg)> DeleteMultiple(List<Guid> TransactionIds)
        {

            try
            {
                var _Transactions = await Context.Transactions.
                        Where(e => TransactionIds.Contains(e.TransactionId) && e.IsActive == true).ToListAsync();
                if (_Transactions.Count() == 0) return (false, "No Transaction Available!");
                for (int i = 0; i < _Transactions.Count(); i++)
                {
                    _Transactions[i].IsActive = false;
                    _Transactions[i].UpdatedAt = DateTime.Now;
                }
                await Context.SaveChangesAsync();
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }

}

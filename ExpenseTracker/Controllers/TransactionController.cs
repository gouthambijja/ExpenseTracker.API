using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.BLL.RequestModels;

namespace ExpenseTracker.WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid UserId)
        {
            try
            {
                var response = await _transactionService.GetAll(UserId);
                if (response.transactions == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Bin")]
        public async Task<IActionResult> GetBin(Guid UserId)
        {
            try
            {
                var response = await _transactionService.GetBinAll(UserId);
                if (response.transactions == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddTransaction")]
        public async Task<IActionResult> Post(BLTransaction transaction)
        {
            try
            {
                var response = await _transactionService.Add(transaction);
                if (response.transaction == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddTransactions")]
        public async Task<IActionResult> Post(List<BLTransaction> transactions)
        {
            try
            {
                var response = await _transactionService.AddMany(transactions);
                if (response.isSuccess == false) return BadRequest(response.ErrorMsg);
                return Ok(response.isSuccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetFiltered")]
        public async Task<IActionResult> GetFiltered(TransactionsFilterModel? transactionsFilterModel)
        {
            try
            {
                var response = await _transactionService.GetFiltered(transactionsFilterModel);
                if (response.transactions == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetFilteredByDate")]
        public async Task<IActionResult> GetFilteredByDate(Guid UserId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var response = await _transactionService.GetFilteredByDateRange(UserId, StartDate, EndDate);
                if (response.transactions == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("EditTransaction")]
        public async Task<IActionResult> UpdateTransaction(BLTransaction transaction)
        {
            try
            {
                var response = await _transactionService.Update(transaction);
                if (response.transaction == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Restore")]
        public async Task<IActionResult> Restore(BLTransaction transaction)
        {
            try
            {
                var response = await _transactionService.Restore(transaction);
                if (response.transaction == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {
                var response = await _transactionService.Delete(id);
                if (response.transaction == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteTransactionPermanently/{id}")]
        public async Task<IActionResult> DeleteTransactionPermanently(Guid id)
        {
            try
            {
                var response = await _transactionService.DeletePermanently(id);
                if (response.transaction == null) return BadRequest(response.ErrorMsg);
                return Ok(response.transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> TransactionIds)
        {
            try
            {
                var response = await _transactionService.DeleteMultiple(TransactionIds);
                if (!response.isSuccess) return BadRequest(response.ErrorMsg);
                return Ok(response.isSuccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

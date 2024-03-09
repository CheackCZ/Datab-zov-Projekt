using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    // <summary>
    /// Controller for managing bank transfers.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Bank_TransfersController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public Bank_TransfersController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all bank transfers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Bank_Transfer>>> GetAllBank_Transfers()
        {
            var bank_Transfers = await _context.Bank_Transfers.ToListAsync();
            return Ok(bank_Transfers);
        }

        /// <summary>
        /// Retrieves a bank transfer by ID.
        /// </summary>
        /// <param name="id">The ID of the bank transfer to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank_Transfer>> GetBank_TransferByID(int id)
        {
            var bank_Transfer = await _context.Bank_Transfers.FindAsync(id);
            if (bank_Transfer == null)
                return NotFound("Bank Trannsfer Not Found!");

            return Ok(bank_Transfer);
        }

        /// <summary>
        /// Adds a new bank transfer.
        /// </summary>
        /// <param name="bank_Transfer">The bank transfer to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Bank_Transfer>>> AddBankTransfer([FromBody] Bank_Transfer bank_Transfer)
        {
            if (bank_Transfer.Variable_Symbol.Length > 10)
                return BadRequest("Variable symbol can have maximum of 10 characters!");
            if (bank_Transfer.IBAN.Length > 34)
                return BadRequest("IBAN can have maximum of 34 characters!");

            _context.Bank_Transfers.Add(bank_Transfer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Bank_Transfers.ToListAsync());
        }

        /// <summary>
        /// Updates an existing bank transfer.
        /// </summary>
        /// <param name="updatedBankTransfer">The updated bank transfer information.</param>
        [HttpPut]
        [HttpPut]
        public async Task<ActionResult<List<Bank_Transfer>>> UpdateBankTransfer(Bank_Transfer updatedBankTransfer)
        {
            var dbTransfer = await _context.Bank_Transfers.FindAsync(updatedBankTransfer.ID);
            if (dbTransfer == null)
                return NotFound("Bank Transfer Not Found!");

            dbTransfer.Variable_Symbol = updatedBankTransfer.Variable_Symbol;
            dbTransfer.IBAN = updatedBankTransfer.IBAN;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.Bank_Transfers.ToListAsync());
        }

        /// <summary>
        /// Deletes a bank transfer.
        /// </summary>
        /// <param name="id">The ID of the bank transfer to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Bank_Transfer>>> DeleteBankTransfer(int id)
        {
            var dbTransfer = await _context.Bank_Transfers.FindAsync(id);
            if (dbTransfer == null)
                return NotFound("Bank Transfer Not Found!");

            _context.Bank_Transfers.Remove(dbTransfer);
            await _context.SaveChangesAsync();

            return Ok($"Bank Transfer deleted successfully: \n ID: {dbTransfer.ID}\n IBAN: {dbTransfer.IBAN}\n Variable symbol: {dbTransfer.Variable_Symbol}");
        }
    }
}

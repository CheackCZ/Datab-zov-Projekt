using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Bank_TransfersController : ControllerBase
    {
        private readonly DataContext _context;

        public Bank_TransfersController(DataContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<List<Bank_Transfer>>> GetAllBank_Transfers()
        {
            var bank_Transfers = await _context.Bank_Transfers.ToListAsync();
            return Ok(bank_Transfers);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Bank_Transfer>> GetBank_TransferByID(int id)
        {
            var bank_Transfer = await _context.Bank_Transfers.FindAsync(id);
            if (bank_Transfer == null)
                return NotFound("Bank Trannsfer Not Found!");

            return Ok(bank_Transfer);
        }

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

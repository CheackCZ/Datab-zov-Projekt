using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Text.RegularExpressions;

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
            //ID specification condition
            if (bank_Transfer.ID == 0)
                return BadRequest("You have to specify the bank trasnfer's ID!");

            //Checking for ID usage
            var transfers = _context.Bank_Transfers.ToList();
            foreach (Bank_Transfer transfer in transfers)
            {
                if (bank_Transfer.ID == transfer.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateBankTransfer(bank_Transfer);
            if (result != null)
            {
                return result;
            }

            _context.Bank_Transfers.Add(bank_Transfer);
            await _context.SaveChangesAsync();

            return Ok($"Bank ({bank_Transfer.ID}) Transfer added successfully:\n" + bank_Transfer);
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

            var result = ValidateBankTransfer(updatedBankTransfer);
            if (result != null)
            {
                return result;
            }

            dbTransfer.Variable_Symbol = updatedBankTransfer.Variable_Symbol;
            dbTransfer.IBAN = updatedBankTransfer.IBAN;
            
            await _context.SaveChangesAsync();

            return Ok($"Bank Transfer ({dbTransfer.ID}) updated successfully: \n" + updatedBankTransfer);
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

            return Ok($"Bank ({dbTransfer.ID}) Transfer deleted successfully: \n" + dbTransfer);
        }

        /// <summary>
        /// Validates a bank transfer entity.
        /// </summary>
        /// <param name="bankTransfer">The bank transfer entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided IBAN is already in use by another bank transfer,
        /// - <see cref="BadRequestObjectResult"/> with a message if the variable symbol length exceeds 10 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the IBAN length exceeds 34 characters,
        /// - <see langword="null"/> if the bank transfer entity is valid.
        /// </returns>
        private ActionResult ValidateBankTransfer(Bank_Transfer bankTransfer)
        {
            //Checks IBAN is not duplicated, because it has to be unique.
            var existingBankTransfers = _context.Bank_Transfers.ToList();
            foreach (var existingTransfer in existingBankTransfers)
            {
                if (bankTransfer.IBAN == existingTransfer.IBAN)
                {
                    if (bankTransfer.ID != existingTransfer.ID)
                        return BadRequest("This IBAN is already in use!\n -> Use a different one.");
                }
            }

            //Variable Symbol Regex
            string varSymPattern = @"^\d{2,10}$";
            if (!Regex.IsMatch(bankTransfer.Variable_Symbol, varSymPattern))
            {
                return BadRequest("Variable symbol can have a maximum of 10 characters and minimumm of 2 and is supposed to be numeric!");
            }

            //IBAN Regex
            string ibanPattern = @"^([A-Z]{2}[0-9]{2})([A-Z0-9]{4}\s?)[A-Z0-9]{1,26}$";
            if (!Regex.IsMatch(bankTransfer.IBAN, ibanPattern))
            {
                return BadRequest("IBAN can have a maximum of 34 characters, needs to be in a specific format and is supposed to be numeric!!");
            }

            return null;
        }
    }
}

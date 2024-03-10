using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing payments.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public PaymentsController(DataContext context) { _context = context; }

        // <summary>
        /// Retrieves all payments.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

        /// <summary>
        /// Retrieves a payment by ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentByID(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound("Payment Not Found!");

            if ((payment.Credit_Card_id == null) & (payment.Bank_Transfer_id != null) || (payment.Credit_Card_id != null) & (payment.Bank_Transfer_id == null))
            {
                return Ok(payment);
            }

            if (payment.Description == null)
            {
                return Ok("Descritpion is null!" + payment);
            }
            return Ok(payment);
        }

        /// <summary>
        /// Adds a new payment.
        /// </summary>
        /// <param name="payment">The payment to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Payment>>> AddPayment([FromBody] Payment payment)
        {
            //ID specification condition
            if (payment.ID == 0)
                return BadRequest("You have to specify the payment's ID!");

            //Checking for ID usage
            var payments = _context.Payments.ToList();
            foreach (Payment pay in payments)
            {
                if (payment.ID == pay.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidatePayment(payment);
            if (result != null)
            {
                return result;
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok($"Payment ({payment.ID}) added successfully:\n" + payment);
        }

        /// <summary>
        /// Updates an existing payment.
        /// </summary>
        /// <param name="updatedPayment">The updated payment information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Payment>>> UpdatePayment(Payment updatedPayment)
        {
            var dbPayment = await _context.Payments.FindAsync(updatedPayment.ID);
            if (dbPayment == null)
                return NotFound("Payment Not Found!");

            var result = ValidatePayment(updatedPayment);
            if (result != null)
            {
                return result;
            }

            dbPayment.Bank_Transfer_id = updatedPayment.Bank_Transfer_id;
            dbPayment.Credit_Card_id = updatedPayment.Credit_Card_id;
            dbPayment.Description = updatedPayment.Description;

            await _context.SaveChangesAsync();

            return Ok($"Payment ({dbPayment.ID}) updated successfully:\n" + updatedPayment);
        }

        /// <summary>
        /// Deletes a payment.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Payment>>> DeletePayment(int id)
        {
            var dbPayment = await _context.Payments.FindAsync(id);
            if (dbPayment == null)
                return NotFound("Payment Not Found!");

            _context.Payments.Remove(dbPayment);
            await _context.SaveChangesAsync();

            return Ok($"Payment ({dbPayment.ID}) deleted successfully:\n" + dbPayment);
        }

        /// <summary>
        /// Validates a payment entity.
        /// </summary>
        /// <param name="payment">The payment entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the payment description length exceeds 50 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if both bank transfer ID and credit card ID are set or both are null,
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided bank transfer ID or credit card ID does not exist in the database,
        /// - <see langword="null"/> if the payment entity is valid.
        /// </returns>
        private ActionResult ValidatePayment(Payment payment)
        {
            //Checks for length of inputs
            if (payment.Description != null)
                if (payment.Description.Length > 50)
                {
                    return BadRequest("Payment description can have maximum length of 50 characters!");
                }

            //Checks the XOR of ARC.
            if (((payment.Bank_Transfer_id == null) && (payment.Credit_Card_id == null)) || ((payment.Bank_Transfer_id != null) && (payment.Credit_Card_id != null)))
            {
                return BadRequest("Warning: One FK has to be null, the other one has to be set!");
            }

            //Checks the FK exists
            var bankTransferIds = _context.Bank_Transfers.Select(transfer => transfer.ID).ToList();
            var creditCardIds = _context.Credit_Cards.Select(card => card.ID).ToList();
          
            if (payment.Bank_Transfer_id != null && !bankTransferIds.Contains(payment.Bank_Transfer_id.Value))
            {
                return BadRequest("This ID doesn't match any of Bank Transfers ID!");
            }
            if (payment.Credit_Card_id != null && !creditCardIds.Contains(payment.Credit_Card_id.Value))
            {
                return BadRequest("This ID doesn't match any of Credit Cards ID!");
            }

            return null;
        }
    }
}

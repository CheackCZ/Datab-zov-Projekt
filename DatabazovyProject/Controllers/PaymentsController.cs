using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Payments.ToListAsync());
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

            dbPayment.Bank_Transfer_id = updatedPayment.Bank_Transfer_id;
            dbPayment.Credit_Card_id = updatedPayment.Credit_Card_id;
            dbPayment.Description = updatedPayment.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.Payments.ToListAsync());
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

            return Ok(await _context.Payments.ToListAsync());
        }
    }
}

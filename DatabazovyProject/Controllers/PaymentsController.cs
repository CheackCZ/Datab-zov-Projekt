using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DataContext _context;

        public PaymentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }


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


        [HttpPost]
        public async Task<ActionResult<List<Payment>>> AddPayment([FromBody] Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Payments.ToListAsync());
        }

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

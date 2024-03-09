using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound("Customer Not Found!");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer updatedCustomer)
        {
            var dbCustomer = await _context.Customers.FindAsync(updatedCustomer.ID);
            if (dbCustomer == null)
                return NotFound("Customer Not Found!");

            dbCustomer.Name = updatedCustomer.Name;
            dbCustomer.LastName = updatedCustomer.LastName;
            dbCustomer.Email = updatedCustomer.Email;
            dbCustomer.Phone = updatedCustomer.Phone;
            dbCustomer.Password = updatedCustomer.Password;

            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Customer>>> DeleteCustomer(int id)
        {
            var dbCustomer = await _context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return NotFound("Customer Not Found!");

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }
    }
}

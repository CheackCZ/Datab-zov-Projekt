using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing customers.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public CustomersController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByID(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound("Customer Not Found!");

            return Ok(customer);
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="updatedCustomer">The updated customer information.</param>
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

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
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

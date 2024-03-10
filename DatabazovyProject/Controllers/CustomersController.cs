using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            //ID specification condition
            if (customer.ID == 0)
                return BadRequest("You have to specify the customer's ID!");

            //Checking for ID usage
            var customers = _context.Customers.ToList();
            foreach (Customer cust in customers)
            {
                if (cust.ID == customer.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateCustomer(customer);
            if (result != null)
            {
                return result;
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok($"Customer ({customer.ID}) added successfully:\n" + customer);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="updatedCustomer">The updated customer information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer updatedCustomer)
        {
            var result = ValidateCustomer(updatedCustomer);
            if (result != null)
            {
                return result;
            }

            var dbCustomer = await _context.Customers.FindAsync(updatedCustomer.ID);
            if (dbCustomer == null)
                return NotFound("Customer Not Found!");

            dbCustomer.Name = updatedCustomer.Name;
            dbCustomer.LastName = updatedCustomer.LastName;
            dbCustomer.Email = updatedCustomer.Email;
            dbCustomer.Phone = updatedCustomer.Phone;
            dbCustomer.Password = updatedCustomer.Password;

            await _context.SaveChangesAsync();

            return Ok($"Customer ({dbCustomer.ID}) updated successfully:\n" + updatedCustomer);
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

            return Ok($"Customer ({dbCustomer.ID}) deleted successfully:\n" + dbCustomer);
        }

        /// <summary>
        /// Validates a customer entity.
        /// </summary>
        /// <param name="customer">The customer entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided email is already in use by another customer,
        /// - <see cref="BadRequestObjectResult"/> with a message if the customer's name or last name length exceeds 20 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the customer's email length exceeds 30 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the customer's phone length exceeds 12 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the customer's password length exceeds 60 characters,
        /// - <see langword="null"/> if the customer entity is valid.
        /// </returns>
        private ActionResult ValidateCustomer(Customer customer)
        {
            var customers = _context.Customers.ToList();
            foreach (Customer cust in customers)
            {
                if (customer.Email == cust.Email)
                    if (customer.ID != cust.ID)
                        return BadRequest("This email is already in use!\n -> Use differrent Email.");
            }

            //Name / Last Name Regex
            string namePattern = @"^[a-zěščřžýáíéůA-ZĚŠČŘŽÝÁÍÉ]{1,20}$";
            if ((!Regex.IsMatch(customer.Name, namePattern)) || ((!Regex.IsMatch(customer.LastName, namePattern))))
            {
                return BadRequest("Customer's name / last name can have maximum length of 20 characters and can only contain Alphabetical characters!");
            }

            //Email Regex
            string emailPattern = @"^(?=.{1,30}$)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(customer.Email, emailPattern))
            {
                return BadRequest("Customer's email can have maximum length of 30 characters and has to be in a special format!");
            }

            //Phone Regex
            string phonePattern = @"^\d{9,12}$";
            if (!Regex.IsMatch(customer.Phone, phonePattern))
            {
                return BadRequest("Customer's phone can have maximum length of 12 characters and minimmum of 9!");
            }

            //Password Regex
            string passwordPattern = @"^.{1,60}$";
            if (!Regex.IsMatch(customer.Password, passwordPattern))
            {
                return BadRequest("Customer's password can have maximum length of 60 characters and can use any character!");
            }
            return null;
        }
    }
}

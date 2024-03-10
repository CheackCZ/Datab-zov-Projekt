using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing orders.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public OrdersController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves an order by ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByID(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound("Order Not Found!");

            return Ok(order);
        }

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="order">The order to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder([FromBody] Order order)
        {
            //ID specification condition
            if (order.ID == 0)
                return BadRequest("You have to specify the order's ID!");

            //Checking for ID usage
            var orders = _context.Orders.ToList();
            foreach (Order ord in orders)
            {
                if (order.ID == ord.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateOrder(order);
            if (result != null)
            {
                return result;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok($"Order ({order.ID}) added successfully:\n" + order);
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="updatedOrder">The updated order information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Order>>> UpdateOrder(Order updatedOrder)
        {
            var dbOrder = await _context.Orders.FindAsync(updatedOrder.ID);
            if (dbOrder == null)
                return NotFound("Order Not Found!");

            var result = ValidateOrder(updatedOrder);
            if (result != null)
            {
                return result;
            }

            dbOrder.Payment_id = updatedOrder.Payment_id;
            dbOrder.Customer_id = updatedOrder.Customer_id;
            dbOrder.Order_number = updatedOrder.Order_number;
            dbOrder.Order_price = updatedOrder.Order_price;
            dbOrder.Date = updatedOrder.Date;

            await _context.SaveChangesAsync();

            return Ok($"Order ({dbOrder.ID}) updated successfully:\n" + updatedOrder);
        }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var dbOrder = await _context.Orders.FindAsync(id);
            if (dbOrder == null)
                return NotFound("Order Not Found!");

            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();

            return Ok($"Order ({dbOrder.ID}) deleted successfully:\n" + dbOrder);
        }

        /// <summary>
        /// Validates an order entity.
        /// </summary>
        /// <param name="order">The order entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided order number is already in use by another order,
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided payment ID or customer ID does not exist in the database,
        /// - <see langword="null"/> if the order entity is valid.
        /// </returns>
        private ActionResult ValidateOrder(Order order)
        {
            //Controls PK
            var orders = _context.Orders.ToList();

            foreach (Order ord in orders)
            {
                if (ord.Order_number == order.Order_number)
                {
                    if (order.ID != ord.ID)
                        return BadRequest("This order number is already in use!\n -> Use differrent one.");
                }
            }

            //Checks the FK exists
            var paymentsIds = _context.Payments.Select(payment => payment.ID).ToList();
            var customerIds = _context.Customers.Select(customer => customer.ID).ToList();

            if (order.Payment_id != null && !paymentsIds.Contains(order.Payment_id))
            {
                return BadRequest("This ID doesn't match any of Payments ID!");
            }
            if (order.Customer_id != null && !customerIds.Contains(order.Customer_id))
            {
                return BadRequest("This ID doesn't match any of Customers ID!");
            }

            //Order Price Regex
            string pricePattern = @"^\d*\.?\d+$";
            if (!Regex.IsMatch(order.Order_number.ToString(), pricePattern))
            {
                return BadRequest("Price added is in wrong format!");
            }
            return null;
        }
    }   
}

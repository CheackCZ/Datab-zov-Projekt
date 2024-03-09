using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByID(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound("Order Not Found!");

            return Ok(order);
        }


        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Order>>> UpdateOrder(Order updatedOrder)
        {
            var dbOrder = await _context.Orders.FindAsync(updatedOrder.ID);
            if (dbOrder == null)
                return NotFound("Order Not Found!");

            dbOrder.Payment_id = updatedOrder.Payment_id;
            dbOrder.Customer_id = updatedOrder.Customer_id;
            dbOrder.Order_number = updatedOrder.Order_number;
            dbOrder.Order_price = updatedOrder.Order_price;
            dbOrder.Date = updatedOrder.Date;

            await _context.SaveChangesAsync();

            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var dbOrder = await _context.Orders.FindAsync(id);
            if (dbOrder == null)
                return NotFound("Order Not Found!");

            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();

            return Ok(await _context.Orders.ToListAsync());
        }
    }   
}

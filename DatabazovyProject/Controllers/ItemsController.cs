using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemByID(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound("Item Not Found!");

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<Item>>> AddItem([FromBody] Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Item>>> UpdateItem(Item updatedItem)
        {
            var dbItem = await _context.Items.FindAsync(updatedItem.ID);
            if (dbItem == null)
                return NotFound("Item Not Found!");

            dbItem.Template_id = updatedItem.Template_id;
            dbItem.Orders_id = updatedItem.Orders_id;
            dbItem.Quantity = updatedItem.Quantity;
            dbItem.Price_of_item = updatedItem.Price_of_item;

            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Item>>> DeleteItem(int id)
        {
            var dbItem = await _context.Items.FindAsync(id);
            if (dbItem == null)
                return NotFound("Item Not Found!");

            _context.Items.Remove(dbItem);
            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }
    }
}

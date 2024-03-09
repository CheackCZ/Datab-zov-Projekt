using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing items.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public ItemsController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        /// <summary>
        /// Retrieves an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemByID(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound("Item Not Found!");

            return Ok(item);
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Item>>> AddItem([FromBody] Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="updatedItem">The updated item information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Item>>> UpdateItem(Item updatedItem)
        {
            var dbItem = await _context.Items.FindAsync(updatedItem.ID);
            if (dbItem == null)
                return NotFound("Item Not Found!");

            dbItem.Template_id = updatedItem.Template_id;
            dbItem.Order_id = updatedItem.Order_id;
            dbItem.Quantity = updatedItem.Quantity;
            dbItem.Price_of_Item = updatedItem.Price_of_Item;

            await _context.SaveChangesAsync();

            return Ok(await _context.Items.ToListAsync());
        }

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
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

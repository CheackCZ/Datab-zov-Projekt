using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            //ID specification condition
            if (item.ID == 0)
                return BadRequest("You have to specify the item's ID!");

            //Checking for ID usage
            var items = _context.Items.ToList();
            foreach (Item it in items)
            {
                if (item.ID == it.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateItem(item);
            if (result != null)
            {
                return result;
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return Ok($"Item ({item.ID}) added successfully:\n" + item);
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
            dbItem.Orders_id = updatedItem.Orders_id;
            dbItem.Quantity = updatedItem.Quantity;
            dbItem.Price_of_Item = updatedItem.Price_of_Item;

            await _context.SaveChangesAsync();

            return Ok($"Item ({dbItem.ID}) updated successfully:\n" + updatedItem);
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

            return Ok($"Item ({dbItem.ID}) deleted successfully:\n" + dbItem);
        }

        /// <summary>
        /// Validates an item entity.
        /// </summary>
        /// <param name="item">The item entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided template ID or order ID does not exist in the database,
        /// - <see langword="null"/> if the item entity is valid.
        /// </returns>
        private ActionResult ValidateItem(Item item)
        {
            //Checks the FK exists
            var templateIDs = _context.Templates.Select(template => template.ID).ToList();
            var orderIds = _context.Orders.Select(order => order.ID).ToList();

            if (item.Template_id != null && !templateIDs.Contains(item.Template_id))
            {
                return BadRequest("This ID doesn't match any of Templates ID!");
            }
            if (item.Orders_id != null && !orderIds.Contains(item.Orders_id))
            {
                return BadRequest("This ID doesn't match any of Orders ID!");
            }

            //Quantity Regex
            string quantityPattern = @"^\d+$";
            if (!Regex.IsMatch(item.Quantity.ToString(), quantityPattern))
            {
                return BadRequest("Quantity added is in wrong format!");
            }

            //Price of Item Regex
            string priceOfItemPattern = @"^\d*\.?\d+$";
            if (!Regex.IsMatch(item.Price_of_Item.ToString(), priceOfItemPattern))
            {
                return BadRequest("Price of Item added is in wrong format!");
            }

            return null;
        }

    }
}

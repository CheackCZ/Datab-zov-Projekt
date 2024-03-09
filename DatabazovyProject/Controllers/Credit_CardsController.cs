using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing credit cards.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Credit_CardsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public Credit_CardsController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all credit cards.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Credit_Card>>> GetAllCredit_Cards()
        {
            var creditCards = _context.Credit_Cards.ToList();
            return Ok(creditCards);
        }

        /// <summary>
        /// Retrieves a credit card by ID.
        /// </summary>
        /// <param name="id">The ID of the credit card to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Credit_Card>> GetCreditCardById(int id)
        {
            var creditCards = await _context.Credit_Cards.FindAsync(id);
            if (creditCards == null)
                return NotFound("Credit Card Not Found!");

            return Ok(creditCards);
        }

        /// <summary>
        /// Adds a new credit card.
        /// </summary>
        /// <param name="credit_Card">The credit card to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Credit_Card>>> AddCreditCard([FromBody] Credit_Card credit_Card)
        {
            _context.Credit_Cards.Add(credit_Card);
            await _context.SaveChangesAsync();

            return Ok(await _context.Credit_Cards.ToListAsync());
        }

        /// <summary>
        /// Updates an existing credit card.
        /// </summary>
        /// <param name="updatedCredit_Card">The updated credit card information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Credit_Card>>> UpdateCreditCard(Credit_Card updatedCredit_Card)
        {
            var dbCard = await _context.Credit_Cards.FindAsync(updatedCredit_Card.ID);
            if (dbCard == null)
                return NotFound("Card Not Found!");

            dbCard.Card_Number = updatedCredit_Card.Card_Number;
            dbCard.Expiration_date = updatedCredit_Card.Expiration_date;
            dbCard.CVV = updatedCredit_Card.CVV;

            await _context.SaveChangesAsync();

            return Ok(await _context.Credit_Cards.ToListAsync());
        }

        /// <summary>
        /// Deletes a credit card.
        /// </summary>
        /// <param name="id">The ID of the credit card to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Credit_Card>>> DeleteCreditCard(int id)
        {
            var dbCard = await _context.Credit_Cards.FindAsync(id);
            if (dbCard == null)
                return NotFound("Card Not Found!");

            _context.Credit_Cards.Remove(dbCard);
            await _context.SaveChangesAsync();

            return Ok(await _context.Credit_Cards.ToListAsync());
        }

    }
}

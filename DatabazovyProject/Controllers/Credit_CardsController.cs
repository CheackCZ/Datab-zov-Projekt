using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

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
            //ID specification condition
            if (credit_Card.ID == 0)
                return BadRequest("You have to specify the credit card's ID!");

            //Checking for ID usage
            var cards = _context.Credit_Cards.ToList();
            foreach (Credit_Card card in cards)
            {
                if (credit_Card.ID == card.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateCard(credit_Card);
            if (result != null)
            {
                return result;
            }

            _context.Credit_Cards.Add(credit_Card);
            await _context.SaveChangesAsync();

            return Ok($"Credit Card ({credit_Card.ID}) added successfully: \n" + credit_Card);
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

            var result = ValidateCard(updatedCredit_Card);
            if (result != null)
            {
                return result;
            }

            dbCard.Card_Number = updatedCredit_Card.Card_Number;
            dbCard.Expiration_date = updatedCredit_Card.Expiration_date;
            dbCard.CVV = updatedCredit_Card.CVV;

            await _context.SaveChangesAsync();

            return Ok($"Credit Card ({dbCard.ID} updated successfully:\n" + updatedCredit_Card);
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

            return Ok($"Credit Card ({dbCard.ID} deleted successfully: " + dbCard);
        }

        /// <summary>
        /// Validates a credit card entity.
        /// </summary>
        /// <param name="credit_Card">The credit card entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided credit card number is already in use by another credit card,
        /// - <see cref="BadRequestObjectResult"/> with a message if the credit card number does not have a length of 16 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the CVV does not have a length of 3 characters,
        /// - <see langword="null"/> if the credit card entity is valid.
        /// </returns>
        private ActionResult ValidateCard(Credit_Card credit_Card)
        {
            var cards = _context.Credit_Cards.ToList();

            foreach (Credit_Card card in cards)
            {
                if (credit_Card.Card_Number == card.Card_Number)
                {
                    if (credit_Card.ID != card.ID)
                        return BadRequest("This credit card number is already in use!\n -> Use differrent card number.");
                }
            }

            //Card Number Regex
            string cardNumPattern = @"\b(?:\d[ -]*?){16}\b";
            if (!Regex.IsMatch(credit_Card.Card_Number, cardNumPattern))
            {
                return BadRequest("Credit card number has length of 16 characters and is supposed to be numeric!");
            }

            //CVV Regex
            string cvvPattern = @"^\d{3}$";
            if (!Regex.IsMatch(credit_Card.CVV, cvvPattern))
            {
                return BadRequest("CVV has length of 3 characters and is supposed to be numeric!");
            }
            return null;
        }
    }
}

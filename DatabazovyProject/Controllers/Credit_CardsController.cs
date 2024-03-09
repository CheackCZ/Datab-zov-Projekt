using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]

    [ApiController]
    public class Credit_CardsController : ControllerBase
    {
        private readonly DataContext _context;
        public Credit_CardsController(DataContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<List<Credit_Card>>> GetAllCredit_Cards()
        {
            var creditCards = _context.Credit_Cards.ToList();
            return Ok(creditCards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Credit_Card>> GetCreditCardById(int id)
        {
            var creditCards = await _context.Credit_Cards.FindAsync(id);
            if (creditCards == null)
                return NotFound("Credit Card Not Found!");

            return Ok(creditCards);
        }

        [HttpPost]
        public async Task<ActionResult<List<Credit_Card>>> AddCreditCard([FromBody] Credit_Card credit_Card)
        {
            _context.Credit_Cards.Add(credit_Card);
            await _context.SaveChangesAsync();

            return Ok(await _context.Credit_Cards.ToListAsync());
        }

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

using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByID(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound("Author Not Found!");

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<List<Author>>> AddAuthor([FromBody]Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author updatedAuthor)
        {
            var dbAuthor = await _context.Authors.FindAsync(updatedAuthor.ID);
            if (dbAuthor == null)
                return NotFound("Author Not Found!");

            dbAuthor.Name = updatedAuthor.Name;
            dbAuthor.LastName = updatedAuthor.LastName;
            dbAuthor.Email = updatedAuthor.Email;
            dbAuthor.Portfolio = updatedAuthor.Portfolio;

            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Author>>> DeleteAuthor(int id)
        {
            var dbAuthor = await _context.Authors.FindAsync(id);
            if (dbAuthor == null)
                return NotFound("Author Not Found!");

            _context.Authors.Remove(dbAuthor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }
    }
}

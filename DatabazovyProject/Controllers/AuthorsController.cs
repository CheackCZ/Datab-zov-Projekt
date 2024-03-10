using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing authors.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public AuthorsController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        /// <summary>
        /// Retrieves an author by ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByID(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound("Author Not Found!\n -> Use different ID.");

            return Ok(author);
        }

        /// <summary>
        /// Adds a new author.
        /// </summary>
        /// <param name="author">The author to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Author>>> AddAuthor([FromBody]Author author)
        {
            //ID specification condition
            if (author.ID == 0)
                return BadRequest("You have to specify the user's ID!");

            //Checking for ID usage
            var authors = _context.Authors.ToList();
            foreach (Author auth in authors)
            {
               if (author.ID == auth.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateAuthor(author);
            if (result != null)
            {
                return result;
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok($"Author ({author.ID}) added successfully: \n" + author);
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        /// <param name="updatedAuthor">The updated author information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author updatedAuthor)
        {
            //Checking existance of Author
            var dbAuthor = await _context.Authors.FindAsync(updatedAuthor.ID);
            if (dbAuthor == null)
                return NotFound("Author Not Found! \n -> Specify the ID.");

            var result = ValidateAuthor(updatedAuthor);
            if (result != null)
            {
                return result;
            }

            dbAuthor.Name = updatedAuthor.Name;
            dbAuthor.LastName = updatedAuthor.LastName;
            dbAuthor.Email = updatedAuthor.Email;
            dbAuthor.Portfolio = updatedAuthor.Portfolio;

            await _context.SaveChangesAsync();

            return Ok($"Author ({dbAuthor.ID}) updated successfully: \n" + updatedAuthor);
        }

        /// <summary>
        /// Deletes an author.
        /// </summary>
        /// <param name="id">The ID of the author to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Author>>> DeleteAuthor(int id)
        {
            var dbAuthor = await _context.Authors.FindAsync(id);
            if (dbAuthor == null)
                return NotFound("Author Not Found! \n ->  Use different ID.");

            _context.Authors.Remove(dbAuthor);
            await _context.SaveChangesAsync();

            return Ok($"Author ({dbAuthor.ID}) deleted successfully: \n" + dbAuthor);
        }

        /// <summary>
        /// Validates the provided Author object.
        /// </summary>
        /// <param name="author">The Author object to be validated.</param>
        /// <returns>
        /// Returns a BadRequest ActionResult if any validation fails, otherwise returns null.
        /// </returns>
        /// <remarks>
        /// This method checks if the provided author's email or portfolio already exists in the database.
        /// It also validates the format and length constraints for the author's name, last name, email, and portfolio.
        /// </remarks>
        private ActionResult ValidateAuthor(Author author)
        {
            var authors = _context.Authors.ToList();

            foreach (Author auth in authors)
            {
                if (author.Email == auth.Email)
                {
                    if (author.ID != auth.ID)
                        return BadRequest("This email is already in use!\n -> Use differrent Email.");
                }

                if (author.Portfolio == auth.Portfolio)
                {
                    if (author.ID != auth.ID)
                        return BadRequest("This portfolio is already in use!\n -> Use differrent Portfolio. ");
                }
            }

            //Name / Last Name Regex
            string namePattern = @"^[a-zěščřžýáíéůA-ZĚŠČŘŽÝÁÍÉ]{1,20}$";
            if ((!Regex.IsMatch(author.Name, namePattern)) || ((!Regex.IsMatch(author.LastName, namePattern))))
            {
                return BadRequest("Author's name / last name can have maximum length of 20 characters and can only contain Alphabetical characters!");
            }

            //Email Regex
            string emailPattern = @"^(?=.{1,30}$)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(author.Email, emailPattern))  
            {
                return BadRequest("Author's email can have maximum length of 30 characters and has to be in a special format!");
            }

            if (author.Portfolio != null)
            {
                if (author.Portfolio.Length > 50)
                {
                    return BadRequest("Author's portfolio can have maximum length of 50 characters!");
                }
            }
            return null;
        }
    }
}

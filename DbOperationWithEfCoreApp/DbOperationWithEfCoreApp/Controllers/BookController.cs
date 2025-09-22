using DbOperationWithEfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DbOperationWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book Model)
        {
            var author = new Author()
            {
                Name = "John",
                Email = "Cena" 
            };
            Model.Author = author;
            if (Model == null)
                return BadRequest("Book data is null");

            _appDbContext.Books.Add(Model);
            await _appDbContext.SaveChangesAsync();

            return Ok(Model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> Model)
        {
            if (Model == null)
                return BadRequest("Book data is null");

            _appDbContext.Books.AddRange(Model);
            await _appDbContext.SaveChangesAsync();

            return Ok(Model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] Book model)
        {
            if (model == null)
                return BadRequest("Book data is null");

            var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
                return NotFound($"Book with Id = {id} not found");

            // Update properties
            book.Title = model.Title;
            book.Description = model.Description;
            //book.PublishedYear = model.PublishedYear;
            book.AuthorId = model.AuthorId;

            await _appDbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateBooksBySingleQuery([FromBody] Book model)
        {
            _appDbContext.Books.Update(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);    
        }

    }
}

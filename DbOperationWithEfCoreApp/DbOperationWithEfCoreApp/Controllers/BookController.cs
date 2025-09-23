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

        [HttpGet("")]
        public async  Task<IActionResult> GetAllBookasync()
        {
            var book = await _appDbContext.Books.Select(x => new Book
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
            }
            ).AsNoTracking().ToListAsync();
            return Ok(book);
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
        public async Task<IActionResult> AddBooks([FromBody] List<Book> book)
        {
            if (book == null)
                return BadRequest("Book data is null");

            _appDbContext.Books.AddRange(book);
            await _appDbContext.SaveChangesAsync();

            return Ok(book);
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

        [HttpPut("bul-update")]
        public async Task<IActionResult> UpdateBooksInBulk()
        {
            await _appDbContext.Books.Where(x=>x.Id==5).ExecuteUpdateAsync(x => x
            .SetProperty(p =>p.Description, p=> "4th description")
            .SetProperty(p => p.Title,p=>p.Title +  "4rth new updated title")
            .SetProperty(p => p.NoOfPages, p=>23));
            return Ok();
        }

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeletebyIdAsync([FromRoute] int id)
        //{
        //    var book= await _appDbContext.Books.FindAsync(id);
        //    if(book==null)
        //    {
        //        return NotFound();
        //    }
        //    _appDbContext.Books.Remove(book);
        //    await _appDbContext.SaveChangesAsync();
        //    return Ok();
        //}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletebyIdAsync([FromRoute] int id)
        {
            var book = new Book { Id = id };
            _appDbContext.Entry(book).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("bulk")]

        public async Task<IActionResult> DeleteBulkAsync()
        {
            var books =await _appDbContext.Books.Where(x => x.Id < 20).ExecuteDeleteAsync();
            return Ok();
        }

        //[HttpDelete("bulk-delete")]
        //public async Task<IActionResult> DeleteBooksInBulk()
        //{
        //    // Example: delete all books with NoOfPages less than 100
        //    var rowsAffected = await _appDbContext.Books
        //        .Where(b => b.NoOfPages < 100)
        //        .ExecuteDeleteAsync();

        //    return Ok(new { DeletedRows = rowsAffected });
        //}   


    }
}

using DbOperationWithEfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
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
    }
}

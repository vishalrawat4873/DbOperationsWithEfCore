using DbOperationWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyTypeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyTypeController(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencyTypeAsync()
        {
            var result =await (from CurrencyType 
                               in _appDbContext.CurrencyType
                               select CurrencyType).ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCurrencyTypeByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.CurrencyType.FindAsync(id);
            return Ok(result);
        }


        [HttpGet]
        [Route("{name}/{description}")]
        public async Task<IActionResult> GetCurrencyTypeByName([FromRoute] string name, [FromRoute] string description)
        {
            var result =await _appDbContext.CurrencyType.FirstOrDefaultAsync(x => x.Currency==name && x.Description== description);
            return Ok(result);
        }

        //[HttpPost("all")]
        //public async Task <IActionResult> GetCurrenyTypeByIdAsync([FromBody] List<int>ids)
        //{
        //    //var ids = new List<int> { 1,3,2,4 };
        //    var result = await _appDbContext.CurrencyType.Where(x => ids.Contains(x.Id)).ToListAsync();
        //    return Ok(result);
        //}

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenyTypeByIdAsync([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1,3,2,4 };
            var result = await _appDbContext.CurrencyType.
                Where(x => ids.Contains(x.Id)).
                Select(x=>new CurrencyDto()
                { 
                    CurrencyId=x.Id,
                    CurrencyTitle=x.Currency
                }).
                ToListAsync();
            return Ok(result);
        }

    }
}

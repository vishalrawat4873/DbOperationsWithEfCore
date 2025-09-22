using DbOperationWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LaguageController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguage()
        {
            //_appDbContext.Languages.ToList();
            var result = await (from Language in _appDbContext.Languages select Language).ToListAsync();
            return Ok(result);
        }

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F2.Repository.Demo.Repositories;

namespace F2.Repository.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;

        public LibraryController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> Get()
        {
            var authors = await _authorRepository.Include(a => a.Books).ToListAsync();
            return Ok(authors);
        }
    }
}
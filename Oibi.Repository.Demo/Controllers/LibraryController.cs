using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oibi.Repository.Demo.Mapper.Dto;
using Oibi.Repository.Demo.Repositories;
using System.Threading.Tasks;

namespace Oibi.Repository.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public LibraryController(AuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dto = _authorRepository.GetAuthorsAndBooks<AuthorDto>();

            return Ok(dto);
        }
    }
}
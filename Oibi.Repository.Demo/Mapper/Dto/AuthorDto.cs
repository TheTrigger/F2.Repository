using Oibi.Repository.Demo.Models;
using System;
using System.Collections.Generic;

namespace Oibi.Repository.Demo.Mapper.Dto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}

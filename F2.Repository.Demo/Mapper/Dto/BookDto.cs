using System;

namespace F2.Repository.Demo.Mapper.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Isbn { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
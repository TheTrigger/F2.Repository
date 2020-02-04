using System;

namespace Oibi.Repository.Demo.Models
{
    public class BookAuthors : BaseEntity
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
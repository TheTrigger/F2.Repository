using System.Collections.Generic;

namespace Oibi.Repository.Demo.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
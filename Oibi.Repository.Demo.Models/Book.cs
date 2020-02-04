using System;
using System.Collections.Generic;

namespace Oibi.Repository.Demo.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public virtual ICollection<Author> BookAuthors { get; set; }
    }
}
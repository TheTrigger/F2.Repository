using System;
using System.Collections.Generic;

namespace F2.Repository.Demo;

public class Author : BaseEntity
{
    public string Name { get; set; }

    public DateTimeOffset Birthdate { get; set; }

    /// <summary>
    /// See <see cref="LibraryContext.OnModelCreating"/>
    /// </summary>
    public DateTimeOffset ExcludedDate { get; set; }

    public virtual List<Book> Books { get; set; }
}
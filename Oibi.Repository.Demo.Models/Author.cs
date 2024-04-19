using System;
using System.Collections.Generic;

namespace Oibi.Repository.Demo.Models;

public class Author : BaseEntity
{
    public string Name { get; set; }

    /// <summary>
    /// See <see cref="LibraryContext.OnModelCreating"/>
    /// </summary>
    public DateTimeOffset ExcludedDate { get; set; }

    public virtual List<Book> Books { get; set; }
}
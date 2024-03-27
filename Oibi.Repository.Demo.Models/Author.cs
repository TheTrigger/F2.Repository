using System.Collections.Generic;

namespace Oibi.Repository.Demo.Models;

public class Author : BaseEntity
{
	public string Name { get; set; }

	public virtual List<Book> Books { get; set; }
}
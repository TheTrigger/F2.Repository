using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oibi.Repository.Demo.Models;

public class Book : BaseEntity
{
	public string Title { get; set; }

	[StringLength(13, MinimumLength = 13)]
	public string Isbn { get; set; }

	public DateOnly PublishedAt { get; set; }
	public DateTimeOffset ArrivedAt { get; set; }

	public virtual List<Author> Authors { get; set; }
}
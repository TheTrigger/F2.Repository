using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace F2.Repository.Demo.Models;

public class Book : BaseEntity
{
    public string Title { get; set; }

    [StringLength(13, MinimumLength = 13)]
    public string Isbn { get; set; }

    public DateOnly PublishedAt { get; set; }
    public DateTimeOffset ArrivedAt { get; set; }

    public DateTimeOffset? BannedDate { get; set; }
    public DateTimeOffset AuthorBirthdate { get; set; }

    public virtual List<Author> Authors { get; set; }

    public Guid? PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
}
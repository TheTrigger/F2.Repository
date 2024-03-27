using Oibi.Repository.Interfaces;
using System;

namespace Oibi.Repository.Demo.Models;

public abstract class BaseEntity : IEntity<Guid>, ITimestampedEntity
{
	public Guid Id { get; set; }

	public DateTimeOffset CreatedAt { get; set; }

	public DateTimeOffset UpdatedAt { get; set; }
}
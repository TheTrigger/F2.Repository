using F2.Repository.Interfaces;
using System;

namespace F2.Repository.Demo.Models;

public abstract class BaseEntity : IEntity<Guid>, ITimestampedEntity
{
	public Guid Id { get; set; }

	public DateTimeOffset CreatedAt { get; set; }

	public DateTimeOffset UpdatedAt { get; set; }
}
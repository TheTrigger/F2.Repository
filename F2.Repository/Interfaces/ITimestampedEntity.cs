using System;

namespace F2.Repository.Interfaces;

public interface ITimestampedEntity
{
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}
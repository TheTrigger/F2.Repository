using System;

namespace Oibi.Repository.Interfaces;

public interface ITimestampedEntity
{
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}
using System;

namespace Oibi.Repository.Interfaces
{
	public interface ITimestampedEntity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
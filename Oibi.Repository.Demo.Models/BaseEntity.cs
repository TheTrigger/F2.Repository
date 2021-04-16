using Oibi.Repository.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oibi.Repository.Demo.Models
{
	public abstract class BaseEntity : IEntity<Guid>, ITimestampedEntity
	{
		public Guid Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
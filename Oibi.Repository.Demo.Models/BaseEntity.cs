using Oibi.Repository.Interfaces;
using System;

namespace Oibi.Repository.Demo.Models
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
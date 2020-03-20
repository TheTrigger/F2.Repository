using Oibi.Repository.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Oibi.Repository.Demo.Models
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = CreateCryptographicallySecureGuid();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        protected static Guid CreateCryptographicallySecureGuid()
        {
            using var provider = new RNGCryptoServiceProvider();
            
            var bytes = new byte[16];
            provider.GetBytes(bytes);

            return new Guid(bytes);
        }
    }
}
using System;

namespace Oibi.Repository
{
    public interface IEntity<PKT>
    {
        PKT Id { get; set; }
    }
}
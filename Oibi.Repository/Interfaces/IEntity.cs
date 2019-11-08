using System;

namespace Oibi.Repository.Interfaces
{
    /// <summary>
    /// Optional interface to use by-id functions
    /// </summary>
    /// <typeparam name="PK">Your primaty key type (Could be any <see cref="struct"/>: <see cref="Guid"/>, <see cref="int"/>, <see cref="long"/> ...)</typeparam>
    public interface IEntity<PK> where PK : struct
    {
        PK Id { get; set; }
    }
}
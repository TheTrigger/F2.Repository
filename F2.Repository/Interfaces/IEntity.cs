using System;

namespace F2.Repository.Interfaces
{
    /// <summary>
    /// Entity with primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">Primary key type (<see cref="System.IEquatable{T}"/>: <see cref="System.Guid"/>, <see cref="int"/>, <see cref="long"/> ...)</typeparam>
    public interface IEntity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey> 
    {
        TPrimaryKey Id { get; set; }
    }
}
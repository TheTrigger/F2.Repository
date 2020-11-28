namespace Oibi.Repository.Interfaces
{
    /// <summary>
    /// Interface to use by-id functions
    /// </summary>
    /// <typeparam name="TPrimaryKey">Primary key type (<see langword="struct"/>: <see cref="System.Guid"/>, <see cref="int"/>, <see cref="long"/> ...)</typeparam>
    public interface IEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        TPrimaryKey Id { get; set; }
    }
}
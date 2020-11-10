namespace Oibi.Repository.Interfaces
{
    /// <summary>
    /// Interface to use by-id functions
    /// </summary>
    /// <typeparam name="PK">Your primary key type (<see langword="struct"/>: <see cref="System.Guid"/>, <see cref="int"/>, <see cref="long"/> ...)</typeparam>
    public interface IEntity<PK> where PK : struct
    {
        PK Id { get; set; }
    }
}
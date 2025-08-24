namespace Restuarant_Management.Interfaces
{
    public interface INamedEntityRepository<T> : IRepository<T> where T : class
    {
        Task<T?> GetByNameAsync(string name);
    }

}

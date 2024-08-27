namespace ProductApp.Interfaces
{
    public interface IRepository<K,T> where T : class
    {
        Task<List<T>> GetAll();
    }
}

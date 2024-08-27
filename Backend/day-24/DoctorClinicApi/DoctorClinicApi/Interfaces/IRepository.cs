namespace DoctorClinicApi.Interfaces
{
    public interface IRepository<T,K> where T : class
    {

        public Task<T> Add(T item);
        public Task<T> Get(K key);

        public Task<T> Update(T item);

        public Task<bool> Delete(K key);
        public Task<IEnumerable<T>> Get();

    }
}

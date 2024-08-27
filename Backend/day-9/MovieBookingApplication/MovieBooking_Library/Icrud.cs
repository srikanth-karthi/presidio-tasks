using System.Collections.Generic;

namespace MovieBooking_Library
{
    public interface IRepository<K, T> where T : class
    {
        Dictionary<K, T> GetAll();
        T Get(K key);
        void Add(T item);
        T Update(T item);
        T Delete(K key);
    }
}

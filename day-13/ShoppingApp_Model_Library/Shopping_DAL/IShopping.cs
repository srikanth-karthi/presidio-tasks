namespace Shopping_DAL
{

        internal interface IShoppingApp<T, K>
        {
        Task<T> Get(K key);
        Task<ICollection<T>> GetAll();
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<bool> Delete(T item);
    }
}



        

    



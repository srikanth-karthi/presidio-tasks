namespace Shopping_DAL
{

        internal interface IShoppingApp<T, K>
        {
            T Get(K key);
            ICollection<T> GetAll();
            T Add(T item);
            T Update(T item);
            bool Delete(T item);


        }

    }



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_DAL
{
  
        public abstract class AbstractRepository<T, K> : IShoppingApp<T, K>
        {
            protected static IList<T> items = new List<T>();

            public AbstractRepository() { }

            public virtual T Add(T item)
            {
                
                items.Add(item);
                return item;
            }

            public bool Delete(T item) =>
                items.Contains(item) ? items.Remove(item) : throw new KeyNotFoundException($"{item} not found");


            public abstract T Get(K key);
        public ICollection<T> GetAll()
        {
            var sortedList = items.ToList();
            sortedList.Sort();
            return sortedList;
        }


        public abstract T Update(T item);


            protected int GenerateId()
            {
                int count = items.Count;
                if (count == 0) return 1;
                return count + 1;
            }


    }
    }



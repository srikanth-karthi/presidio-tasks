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
        protected IList<T> items = new List<T>();

        public AbstractRepository() { }

        public virtual async Task<T> Add(T item)
        {
            await Task.Run(() => items.Add(item));
            return item;
        }

        public async Task<bool> Delete(T item)
        {
            if (items.Contains(item))
            {
                await Task.Run(() => items.Remove(item));
                return true;
            }
            else
            {
                throw new KeyNotFoundException($"{item} not found");
            }
        }

        public abstract Task<T> Get(K key);
        public async Task<ICollection<T>> GetAll()
        {
            var sortedList = items.ToList();
            sortedList.Sort();
            return sortedList;
        }

 

        public abstract Task<T> Update(T item);


            protected int GenerateId()
            {
                int count = items.Count;
                if (count == 0) return 1;
                return count + 1;
            }

       
    }
    }



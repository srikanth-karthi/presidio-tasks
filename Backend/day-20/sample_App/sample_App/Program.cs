
using sample_App.Model;
namespace sample_App
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Store stores = new Store
            {
                StorId = "1234",
                StorName = "Sample Store",
                StorAddress = "123 Main St",
                City = "Sample City",
          
                Zip = "12345"
            };

            PubsContext context = new PubsContext();
            //context.Stores.Add(stores);
            context.SaveChanges();
            var Stores = context.Stores.ToList();
            foreach (var s in Stores)
            {
                Console.WriteLine(s.StorId + " " + s.StorName);
            }


   
            var store = Stores.SingleOrDefault(a => a.StorId == "1234");
            store.StorName = "00000";
            context.Stores.Update(store);
            context.SaveChanges();
            foreach (var s in Stores)
            {
                Console.WriteLine(s.StorId + " " + s.StorName);
            }

            context.Stores.Remove(store);
            context.SaveChanges();
      
            foreach (var s in Stores)
            {
                Console.WriteLine(s.StorId + " " + s.StorName);
            }

        }
    }
}

namespace ClinicManagementSystem_Library
{
    public interface ICrud<T, K> where T : class
    {

        T Add(T doctor);
        T Get(K key);
        T Update(T doctor);
        bool Delete(K key);
    }
}

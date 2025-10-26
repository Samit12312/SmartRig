namespace SmartRigWeb
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(string Id);
    }
}

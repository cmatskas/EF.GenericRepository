namespace EF.GenericRepository.Interfaces
{
    public interface IUnitOfWork
    {
        string ConnectionString { get; }
        void Dispose();
        void Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }
}

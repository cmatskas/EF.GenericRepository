namespace EF.GenericRepository
{
    using System;
    using System.Collections;
    using DataModel.Interfaces;
    using Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;

        private bool disposed;

        private Hashtable repositories;

        public string ConnectionString 
        {
            get
            {
                return context.ConnectionString;
            }
        }
        
        public UnitOfWork(IDbContext contextPar)
        {
            context = contextPar;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed) if (disposing) context.Dispose();

            disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);

                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}

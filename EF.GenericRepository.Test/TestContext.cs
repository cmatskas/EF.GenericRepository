namespace EF.GenericRepository.Test
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using DataModel.Interfaces;
    using Mapping;

    public class TestContext : DbContext, IDbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);
        }

        public TestContext()
            : base("DefaultConnection")
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 120; // value in seconds
            ConnectionString = Database.Connection.ConnectionString;
        }

        public TestContext(string connectionString)
            : base(connectionString)
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 120; // value in seconds
            ConnectionString = Database.Connection.ConnectionString;
        }

        public string ConnectionString { get; private set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyStateChanges();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new OwnerMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new PropertyFeatureMap());
            modelBuilder.Configurations.Add(new PropertyImageMap());
        }
    }
}

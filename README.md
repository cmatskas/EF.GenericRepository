EF.GenericRepository
====================

A C# Generic repository implementation for Entity Framework.

Yes, I know, there are more of them out there, but what I found hard to with the other ones is managing object graphs. Sooner or later, once you start moving outside the basic CRUD functionalities of atomic objects, you are faced with the issue of object state management, especially in case of disconnected entities.

What this simple implementation attempts to do, is give the developers the option to manually manage object state in a safe and efficient way.

### How to use EF.GenericRepository ###

To add the EF.GenericRepository to your project, either get the source from here or use NuGet. Please use NuGet!

Once you add the binary to your solution, you need to implement your ```DbContext``` and your data model objects and you can start interacting with the database
## 1. Set up ##
### 1.1 Basic Context Example ###
    public class TestContext : DbContext, IDbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);
        }

        public TestContext()
            : base("DefaultConnection")
        {
            ConnectionString = Database.Connection.ConnectionString;
        }

        public TestContext(string connectionString)
            : base(connectionString)
        {
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

The concrete context needs to derive from ```DbContext``` and implement the ```IDbContext```, which is part of the EF.GenericRepository project. The context constructor accepts either a default connection string or a user specified one. In addition, it exposes the connection string so that you can easily access it in your unit/integration tests.

### 1.2 Basic POCO example with EF Code-First ###

    public class Owner : EntityBase
    {
        public Owner()
        {
            Properties = new Collection<Property>();
        }

        public int OwnerId { get; set; }

        public string Address { get; set; }

        public string FullName { get; set; }

        public string Telephone1 { get; set; }

        public string Telephone2 { get; set; }

        public bool IsAgency { get; set; }

        public string LandlordRegNumber { get; set; }

        public bool HasDepositAccount { get; set; }
    }

This is one of the classes in the data model. As long as any class derives from ```EntityBase```, it can work with the ```EF.GenericRepository```. Remember that you also need to define the EF specific properties either through the fluent API or the data annotations. An example of such map is provided below:

### 1.3 Poco Map for Entity Framework ###

    public class OwnerMap: EntityTypeConfiguration<Owner>
    {
        public OwnerMap()
        {
            HasKey(t => t.OwnerId);
            Property(t => t.FullName).IsRequired().HasMaxLength(500);
            Property(t => t.Telephone1).IsRequired().HasMaxLength(500);
            Property(t => t.Address).IsRequired();
            Property(t => t.Telephone2).HasMaxLength(500);
            Property(t => t.LandlordRegNumber).HasMaxLength(500);

            ToTable("Owner");
        }
    }

## 2. Using the UnitOfWork for CRUD operations ##
### 2.1 Create/insert new object ###
    var object = new ConcreteObject()
    object.State = ObjectState.Added;
    unitOfWork.Repository<ConcreteObject>().Insert(object);
    unitOfWork.Save();

### 2.2 Create/Insert new object graph
    var parent = new ParentObject();
    parent.State = ObjectState.Added;
    var child = new ChildObject();
    child.Parent = parent;
    child.State = ObjectState.Added;
    unitOfWork.Repository<ChildObject>().InsertGraph(child);
    unitOfWork.Save();
    
### 2.3 Update object ###
    var objectToUpdate = unitOfWork.Repository<ConcreteObject>.Find(someObjectId);
    objectToUpdate.SomeProperty = "new value"
    objectToUpdate.State = ObjectState.Modified;
    unitOfWork.Repository<ConcreteObject>.Update(objectToUpdate);
    unitOfWork.Save();
    
### 2.4 Delete object ###
    var objectToDelete = unitOfWork.Repository<ConcreteObject>.Find(someObjectId);
    objectToUpdate.State = ObjectState.Deleted;
    unitOfWork.Repository<ConcreteObject>.Delete(objectToUpdate);
    unitOfWork.Save();
    
### 2.5 Querying objects ###
    var result = unitOfWork.Repository<ConcreteObject>.Query().Filter(o =>o.objectId = thisId).Get().First();
    
    

For more examples on how to use the code, head to the ```EF.GenericRepository.Test```.











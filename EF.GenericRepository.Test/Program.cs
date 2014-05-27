namespace EF.GenericRepository.Test
{
    using System;
    using Enums;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionName = @"Server=(localdb)\v11.0;Integrated Security=true;Initial Catalog=" + Guid.NewGuid() + ";";
            var testContext = new TestContext(connectionName);
            var unitOfWork = new UnitOfWork(testContext);

            // Initialize the database
            InitializeDatabaseForTests(testContext);

            Console.WriteLine("[{0}] - Creating new owner", DateTime.Now);
            var owner = CreateTestOwner();
            unitOfWork.Repository<Owner>().InsertGraph(owner);
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Owner created successfully",DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property, without parent owner", DateTime.Now);
            unitOfWork.Repository<Property>().Insert(CreateTestProperty());
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property, with parent owner", DateTime.Now);
            owner.State = ObjectState.Modified;
            var property = CreateTestProperty(owner);
            unitOfWork.Repository<Property>().Insert(property);
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property feature, without parent property", DateTime.Now);
            unitOfWork.Repository<PropertyFeature>().Insert(CreateTestPropertyFeature());
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property feature created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property feature, without parent property", DateTime.Now);
            property.State = ObjectState.Modified;
            unitOfWork.Repository<PropertyFeature>().Insert(CreateTestPropertyFeature(property));
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property feature created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property imange, without parent property", DateTime.Now);
            unitOfWork.Repository<PropertyImage>().Insert(CreateTestPropertyImage());
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property image created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new property image, without parent property", DateTime.Now);
            property.State = ObjectState.Modified;
            unitOfWork.Repository<PropertyImage>().Insert(CreateTestPropertyImage(property));
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Property image created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);

            Console.WriteLine("[{0}] - Creating new complex graph", DateTime.Now);
            owner = CreateTestOwner();
            property = CreateTestProperty(owner);
            var propertyImage = CreateTestPropertyImage(property);

            unitOfWork.Repository<PropertyImage>().InsertGraph(propertyImage);
            unitOfWork.Save();
            Console.WriteLine("[{0}] - Complex graph created successfully", DateTime.Now);
            Console.WriteLine(string.Empty);


            Console.WriteLine("Deleting test database");
            testContext.Database.Delete();
            Console.WriteLine("Done! Press Enter to exit...");
            Console.WriteLine(string.Empty);

            Console.ReadLine();
            
        }

        private static void InitializeDatabaseForTests(TestContext context)
        {
            if (context.Database.Exists())
            {
                 context.Database.Delete();
            }
            context.Database.Create();
        }

        private static Owner CreateTestOwner()
        {
            return new Owner
            {
                FullName = "John Smith",
                HasDepositAccount = true,
                IsAgency = false,
                LandlordRegNumber = "123456",
                Telephone1 = "01234567780",
                Address = "1 Buckingham Palace, London, LE1 2AA",
                State = ObjectState.Added
            };

        }

        public static Property CreateTestProperty(Owner owner = null)
        {
            return new Property
            {
                Address = "2 Buckingham Palace, London, LE3 1AB",
                BedroomNo = 2,
                Description = "Test description",
                Price = 100.00,
                PropertyGuid = Guid.NewGuid().ToString("N"),
                Title = "Test Title",
                Owner = owner,
                PostDate = DateTime.Now,
                State = ObjectState.Added
            };
        }

        public static PropertyImage CreateTestPropertyImage(Property property = null)
        {
            return new PropertyImage
            {
                ImageUrl = "http://random.url",
                IsFeature = true,
                Property = property,
                State = ObjectState.Added
            };
        }

        public static PropertyFeature CreateTestPropertyFeature(Property property = null)
        {
            return new PropertyFeature
            {
                Feature = "Test Feature",
                Property = property,
                State = ObjectState.Added
            };
        }
    }
}

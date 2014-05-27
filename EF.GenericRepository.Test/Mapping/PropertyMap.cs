namespace EF.GenericRepository.Test.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    class PropertyMap: EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            HasKey(t => t.PropertyId);

            Property(t => t.PropertyGuid).IsRequired().HasMaxLength(40);
            Property(t => t.Title).IsRequired().HasMaxLength(500);
            Property(t => t.Address).IsRequired();
            Property(t => t.Description).IsRequired();
            Property(t => t.PostDate).IsRequired();

            ToTable("Property");
        }
    }
}

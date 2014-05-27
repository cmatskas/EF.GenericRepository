namespace EF.GenericRepository.Test.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class PropertyImageMap: EntityTypeConfiguration<PropertyImage>
    {
        public PropertyImageMap()
        {
            HasKey(t => t.PropertyImageId);
            Property(t => t.ImageUrl).IsRequired().HasMaxLength(500);

            ToTable("PropertyImage");
        }
    }
}

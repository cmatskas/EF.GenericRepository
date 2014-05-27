namespace EF.GenericRepository.Test.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Test;

    public class PropertyFeatureMap: EntityTypeConfiguration<PropertyFeature>
    {
        public PropertyFeatureMap()
        {
            HasKey(t => t.PropertyFeatureId);
            Property(t => t.Feature).IsRequired().HasMaxLength(300);

            ToTable("PropertyFeature");
        }
    }
}

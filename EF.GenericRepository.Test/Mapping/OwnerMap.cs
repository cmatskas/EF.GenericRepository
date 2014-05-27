namespace EF.GenericRepository.Test.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Test;

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
}

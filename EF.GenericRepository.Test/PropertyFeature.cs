namespace EF.GenericRepository.Test
{
    using EF.GenericRepository;

    public class PropertyFeature : EntityBase
    {
        public int PropertyFeatureId { get; set; }

        public virtual Property Property { get; set; }

        public string Feature { get; set; }

        #region Equality Overrides
        
        public static bool operator ==(PropertyFeature right, PropertyFeature left)
        {
            if (ReferenceEquals(right, left))
            {
                return true;
            }

            if (((object)right == null) || ((object)left == null))
            {
                return false;
            }
           
            return right.Property == left.Property && string.Equals(right.Feature, left.Feature);
        }

        public static bool operator !=(PropertyFeature right, PropertyFeature left)
        {
            return !(right == left);
        }

        public override int GetHashCode()
        {
            var propertyAddressHash = (this.Property == null) ? 57 : this.Property.Address.GetHashCode();
            var featureHash = (string.IsNullOrEmpty(this.Feature)) ? 19 : this.Feature.GetHashCode();

            var hash = 37;
            hash = hash * 23 + propertyAddressHash;
            hash = hash * 49 + featureHash;
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as PropertyFeature;
            if (other == null)
            {
                return false;
            }

            return this.Property == other.Property && string.Equals(this.Feature, other.Feature);
        }

        public void Copy(PropertyFeature other)
        {
            this.Feature = other.Feature;
        }

        #endregion
    }
}
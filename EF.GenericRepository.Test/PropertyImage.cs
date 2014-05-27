namespace EF.GenericRepository.Test
{
    using GenericRepository;

    public class PropertyImage : EntityBase
    {
        public int PropertyImageId { get; set; }

        public virtual Property Property { get; set; }

        public bool IsFeature { get; set; }

        public string ImageUrl { get; set; }

        #region Equality Overrides

        public static bool operator ==(PropertyImage right, PropertyImage left)
        {
            if (ReferenceEquals(right, left))
            {
                return true;
            }

            if (((object)right == null) || ((object)left == null))
            {
                return false;
            }

            return right.Property == left.Property && string.Equals(right.ImageUrl, left.ImageUrl)
                   && right.IsFeature == left.IsFeature;
        }

        public static bool operator !=(PropertyImage right, PropertyImage left)
        {
            return !(right == left);
        }

        public override int GetHashCode()
        {
            var propertyIdHash = (this.Property == null) ? 57 : this.Property.PropertyId.GetHashCode();
            var featureHash = (string.IsNullOrEmpty(this.ImageUrl)) ? 19 : this.ImageUrl.GetHashCode();

            var hash = 37;
            hash = hash * 23 + propertyIdHash;
            hash = hash * 49 + featureHash;
            hash = hash * 7 + this.IsFeature.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to County return false.
            var other = obj as PropertyImage;
            if (other == null)
            {
                return false;
            }

            return this.Property == other.Property && string.Equals(this.ImageUrl, other.ImageUrl)
                   && this.IsFeature == other.IsFeature;
        }

       public void Copy(PropertyImage other)
        {
            this.IsFeature = other.IsFeature;
            this.ImageUrl = other.ImageUrl;
        }

        #endregion
    }
}

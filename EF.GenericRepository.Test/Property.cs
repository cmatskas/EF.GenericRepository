namespace EF.GenericRepository.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using GenericRepository;

    public class Property : EntityBase
    {
        public Property()
        {
            Images = new Collection<PropertyImage>();
            Features = new Collection<PropertyFeature>();
        }

        public int PropertyId { get; set; }

        public string PropertyGuid { get; set; }

        public virtual Owner Owner { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }

        public int BedroomNo { get; set; }

        public string Description { get; set; }

        public DateTime PostDate { get; set; }

        public string Address { get; set; }

        public virtual ICollection<PropertyImage> Images { get; set; }

        public virtual ICollection<PropertyFeature> Features { get; set; }

        #region Equality members

        public static bool operator ==(Property left, Property right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }

            return string.Equals(left.Address, right.Address)
                && string.Equals(left.PropertyGuid, right.PropertyGuid);
        }

        public static bool operator !=(Property left, Property right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Property;
            if (other == null)
            {
                return false;
            }

            return string.Equals(this.Address, other.Address) && string.Equals(this.PropertyGuid, other.PropertyGuid);
        }

        public override int GetHashCode()
        {
            var addressHash = string.IsNullOrEmpty(this.Address) ? 17 : this.Address.GetHashCode();
            var descriptionHash = string.IsNullOrEmpty(this.Description) ? 31 : this.Description.GetHashCode();

            var hash = 13;
            hash = hash + addressHash;
            hash = hash + descriptionHash;

            return hash * 321;
        }

        public void Copy(Property other, bool isShallow = true)
        {
            this.PropertyGuid = other.PropertyGuid;
            this.Price = other.Price;
            this.Title = other.Title;
            this.BedroomNo = other.BedroomNo;
            this.Description = other.Description;
            this.Address = other.Address;

            if (!isShallow)
            {
                this.Images = other.Images;
                this.Features = other.Features;
            }
        }

        #endregion
    }
}

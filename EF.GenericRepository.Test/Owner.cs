namespace EF.GenericRepository.Test
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using GenericRepository;

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

        public virtual ICollection<Property> Properties { get; set; }

        public void Copy(Owner other, bool isShallow = true)
        {
            FullName = other.FullName;
            Telephone1 = other.Telephone1;
            Telephone2 = other.Telephone2;
            Address = other.Address;
            IsAgency = other.IsAgency;
            LandlordRegNumber = other.LandlordRegNumber;
            HasDepositAccount = other.HasDepositAccount;
            Properties = isShallow ? new Collection<Property>() : other.Properties;
        }

        #region Equality Overrides

        public static bool operator ==(Owner right, Owner left)
        {
            if (ReferenceEquals(right, left))
            {
                return true;
            }

            if (((object)right == null) || ((object)left == null))
            {
                return false;
            }

            return string.Equals(right.FullName, left.FullName)
                   && string.Equals(right.Telephone1, left.Telephone1) && (right.IsAgency == left.IsAgency)
                   && string.Equals(right.Telephone2, left.Telephone2) && (right.Address ==  left.Address)
                   && (right.LandlordRegNumber == left.LandlordRegNumber)
                   && (right.HasDepositAccount == left.HasDepositAccount);
        }

        public static bool operator !=(Owner right, Owner left)
        {
            return !(right == left);
        }

        public override int GetHashCode()
        {
            var fullNameHash = string.IsNullOrEmpty(this.FullName)? 17 : this.FullName.GetHashCode();
            var telephoneHash = string.IsNullOrEmpty(this.Telephone1)? 31 :this.Telephone1.GetHashCode();

            var hash = 13;
            hash = hash + fullNameHash;
            hash = hash + telephoneHash;

            return hash * 321;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Owner;
            if (other == null)
            {
                return false;
            }

            return string.Equals(this.FullName, other.FullName)
                   && string.Equals(this.Telephone1, other.Telephone1) && (this.IsAgency == other.IsAgency)
                   && string.Equals(this.Telephone2, other.Telephone2) && (this.Address == other.Address)
                   && (this.LandlordRegNumber == other.LandlordRegNumber)
                   && (this.HasDepositAccount == other.HasDepositAccount);
        }

        #endregion
    }
}

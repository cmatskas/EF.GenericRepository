namespace EF.GenericRepository
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Interfaces;

    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}

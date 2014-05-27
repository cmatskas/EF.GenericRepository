namespace EF.GenericRepository.Interfaces
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }
}

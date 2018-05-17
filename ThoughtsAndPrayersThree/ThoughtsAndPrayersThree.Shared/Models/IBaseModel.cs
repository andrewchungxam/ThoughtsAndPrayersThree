using System;
namespace ThoughtsAndPrayersThree.Models
{
    public interface IBaseModel
    {
        string SharedStringId { get;  }

        DateTimeOffset UpdatedAt { get; set; }

        //THIS CAN BE DELETED
        DateTimeOffset CreatedDateTime { get; set; }
    }
}
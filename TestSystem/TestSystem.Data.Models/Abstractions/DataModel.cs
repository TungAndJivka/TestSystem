using System;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Data.Models.Abstractions
{
    public abstract class DataModel : IDeletable
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}

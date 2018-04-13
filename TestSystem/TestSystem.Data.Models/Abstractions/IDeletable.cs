using System;

namespace TestSystem.Data.Models.Abstractions
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        //DateTime? DeletedOn { get; set; }
    }
}

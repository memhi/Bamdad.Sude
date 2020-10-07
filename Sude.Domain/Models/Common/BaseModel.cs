using System;
using System.ComponentModel.DataAnnotations;

namespace Sude.Domain.Models.Common
{
    public abstract class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveDate { get; set; }
    }

    public abstract class BaseModel : BaseModel<Guid>
    {

        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

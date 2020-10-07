using System;

namespace Sude.Domain.Models.Common
{
    public abstract class BaseModelNotId
    {
        public DateTime RegDate { get; set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; }

        public bool IsRemoved { get; set; } = false;

        public DateTime? RemoveDate { get; set; }
    }
}

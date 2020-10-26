﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Serving
{
    public class ServingDetailDtoModel
    {
        public string ServingId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }

        public bool IsActive { get; set; }
        public string WorkId { get; set; }
        public string WorkName { get; set; }
        public bool HasInventoryTracking { get; set; }
        public double InventoryCount { get; set; }


    }
}

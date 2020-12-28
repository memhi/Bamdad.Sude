﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Account;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderStatisticsDtoModel
    {
      

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
       public string WorkId { get; set; }
        public int SearchCount { get; set; }
         


    }
}
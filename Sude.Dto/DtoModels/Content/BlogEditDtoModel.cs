﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sude.Dto.DtoModels.Content
{
    public class BlogEditDtoModel
    {
        public string BlogId { get; set; }
        public string Title { get; set; }
  
        public string ShortBody { get; set; }
        public string FullBody { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublish { get; set; }
        public bool AllowComment { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Tags { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class SubCategoryAboutItem
    {
        public long? Id {get;set;}
        public long? SubCategoryAboutItemId {get;set;}
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string  AboutItemDecription { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
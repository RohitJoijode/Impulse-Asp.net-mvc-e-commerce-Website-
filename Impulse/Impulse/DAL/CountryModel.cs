﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class CountryModel
    {
           public long? Id { get; set; }
           public string CountryCode { get; set; }
           public string CountryName { get; set; }
           public bool? IsActive { get; set; }
           public long? CreatedBy { get; set; }
           public DateTime? CreatedDate { get; set; }
           public long? UpdatedBy { get; set; }
           public DateTime? UpdatedDate { get; set; }
    }
}
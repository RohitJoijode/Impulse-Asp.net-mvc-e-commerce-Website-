using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class ColorModal
    {
        public long? Id   {get;set;}
        public long? ColorId  {get;set;}
        public string ColorName {get;set;}
        public string ColorHexCode { get; set; }
        public string ColorImagePath{get;set;}
        public bool? IsActive {get;set;}
        public long? CreatedBy {get;set;}
        public DateTime? CreatedDate {get;set;}
        public long? UpdatedBy  {get;set;}
        public DateTime? UpdatedDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class SizeModel
    {
        public long? Id { get; set; }
        public long? SizeId { get; set; }
        public long? CategoryId { get; set; }
        public string SizeName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }


    }
}
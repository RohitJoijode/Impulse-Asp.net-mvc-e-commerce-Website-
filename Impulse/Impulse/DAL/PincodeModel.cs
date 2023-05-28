using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class PincodeModel
    {
        public long? id { get; set; }
        public string PostOfficeName { get; set; }
        public long? Pincode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
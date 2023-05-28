using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class CityModel
    {
        public long? Id { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
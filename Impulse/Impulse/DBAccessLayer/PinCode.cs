using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Impulse.DBAccessLayer
{
    [Table("PinCode")]
    public class PinCode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long id  {get;set;}
        public string PostOfficeName  {get;set;}
        public long? Pincode {get;set;}
        public string  City    {get;set;}
        public string District {get;set;}
        public string State    {get;set;}
        public bool? IsActive {get;set;}
        public long? CreatedBy {get;set;}
        public DateTime? CreatedDate {get;set;}
        public long? UpdatedBy  {get;set;}
        public DateTime? UpdatedDate { get; set; }
    }
}
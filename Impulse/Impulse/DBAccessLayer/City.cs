using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("City")]
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id {get;set;}
        public long? CountryId  {get;set;}
        public long? StateId    {get;set;}
        public string CityCode  {get;set;}
        public string CityName  {get;set;}
        public bool? IsActive  {get;set;}
        public long? CreatedBy {get;set;}
        public DateTime? CreatedDate{get;set;}
        public long? UpdatedBy  {get;set;}
        public DateTime? UpdatedDate { get; set; }

    }
}
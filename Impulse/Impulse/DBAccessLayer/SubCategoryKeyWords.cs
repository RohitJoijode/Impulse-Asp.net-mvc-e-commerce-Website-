using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("SubCategoryKeyWords")]
    public class SubCategoryKeyWords
    {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Key, Column(Order = 1)]
            public long Id {get;set;}
            public long? KeyWordsId  {get;set;}
            public long? SubCategoryId {get;set;}
            public long? CategoryId  {get;set;}
            public string SubCategoryKeyWordsName  {get;set;}
            public bool? IsActive  {get;set;}
            public long? CreatedBy {get;set;}
            public DateTime? CreatedDate {get;set;}
            public long? UpdatedBy   {get;set;}
            public DateTime? UpdatedDate { get; set; }
    }
}
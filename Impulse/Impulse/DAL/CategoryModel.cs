using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class CategoryModel
    {
        public long? Id { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryKeyWords { get; set; }
        public long? CategoryTypeId { get; set; }
        public string CategoryType { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? CurrencyId { get; set; } //New Column Add On (20-01-2023)  
        public string CurrencyName { get; set; } //New Column Add On (20-01-2023)
        public string CurrencySymbol { get; set; } //New Column Add On (20-01-2023)  
    }
}
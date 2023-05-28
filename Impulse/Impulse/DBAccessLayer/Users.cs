using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{

    #region Users
    [Table("Users")]
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long id { get; set; }
        public long? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddress { get; set; }
        public int UserRole { get; set; }
        public long? UserPincode { get; set; } // here added New column on (26-01-2023)
        public string  UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserMobileNumber { get; set; }
        public int? UserStatus { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsKeepMeUpToDateOnNews { get; set; }
        public string RegisterOTP { get; set; }
        public long? UserCoutryId { get; set; }
        public long? UserStateId { get; set; }
        public long? UserCityId { get; set; }
    }
    #endregion
}
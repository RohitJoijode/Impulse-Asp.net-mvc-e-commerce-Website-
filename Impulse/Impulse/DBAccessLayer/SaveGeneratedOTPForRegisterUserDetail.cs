using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{

    #region SaveGeneratedOTPForRegisterUserDetail
    [Table("SaveGeneratedOTPForRegisterUserDetail")]
    public class SaveGeneratedOTPForRegisterUserDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long ID { get; set; }
        public string OTP { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public long? UserRole { get; set; }
        public string UserAddress { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserStatus { get; set; }
        public string UserEmailId { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public bool? IsKeepMeUpToDateOnNews { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UserCoutryId { get; set; }
        public long? UserStateId { get; set; }
        public long? UserCityId { get; set; }
    }
    #endregion
}
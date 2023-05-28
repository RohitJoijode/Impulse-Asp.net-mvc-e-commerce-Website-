using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Impulse.DAL
{
    public class RegisterModel
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string Address { get; set; }
        public string OTP { get; set; }
        public long? PinCode { get; set; }
        public bool? IsKeepMeUpToDateOnNews { get; set; }
        public string UserType { get; set; }
        public SelectList CountryList { get; set; }
        public long? CountryId { get; set; }
        public long? UserCoutryId { get; set; }
        public long? UserCityId { get; set; }
        public long? UserStateId { get; set; }
        public long? UserId { get; set; } // Here new column added On (26-01-2023)
    }
}
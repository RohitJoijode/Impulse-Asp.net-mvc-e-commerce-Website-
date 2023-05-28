using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Please Insert User Name.")]
        public string  UserName { get; set; }

        [Required(ErrorMessage = "Please Insert Password.")]
        public string UserPassword { get; set; }
    }
}
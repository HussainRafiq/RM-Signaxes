using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RM_Signaxes.Areas.Admin.Models
{
    public class LoginAuth
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
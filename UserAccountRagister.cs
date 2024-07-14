using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chatingweb.DBWorkChat
{
    public class UserAccountRagister
    {
        public int UserId { get; set; }
        [Display(Name = "Enter The name")]
        [Required(ErrorMessage = "This field Required")]
        public string UserName { get; set; }
        [Display(Name = "Enter Enter Email")]
        [Required(ErrorMessage = "This field Required")]
        public string UserEmail { get; set; }
        [Display(Name = "Enter Mobile No.")]
        [Required(ErrorMessage = "This field Required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string UserMobile { get; set; }
        [Display(Name = "Enter Password")]
        [Required(ErrorMessage = "This field Required")]
        public string UserPassword { get; set; }
        public string UserRagisterDate { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
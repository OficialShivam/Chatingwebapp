using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chatingweb.DBWorkChat
{
    public class UserAccountLoginstust
    {
        public int LoginId { get; set; }
        [Display(Name = "Enter The name")]
        [Required(ErrorMessage = "This field Required")]
        
        public string UserName { get; set; }
        [Display(Name = "Enter Email")]
        [Required(ErrorMessage = "This field Required")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Display(Name = "Enter Phone Number")]
        [Required(ErrorMessage = "This field Required")]
        [DataType(DataType.PhoneNumber)]
        public string UserMobile { get; set; }
        [Display(Name = "Enter Phone Password")]
        [Required(ErrorMessage = "This field Required")]
       
        public string UserPassword { get; set; }
        public string UserRagisterDate { get; set; }
        public string UserLoginDate { get; set; }
        public Nullable<int> RagisterStatus { get; set; }
        public Nullable<int> Loginstatus { get; set; }
    }
}
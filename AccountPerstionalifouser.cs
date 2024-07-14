using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chatingweb.DBWorkChat
{
    public class AccountPerstionalifouser
    {
        public int ChatId { get; set; }
        [Display(Name ="Enter Phone Number")]
        [Required (ErrorMessage ="This field Required")]
        [DataType(DataType.PhoneNumber)]
        public string LoginUserMobileNo { get; set; }
        [Display(Name = "Enter the name")]
        [Required(ErrorMessage = "This field Required")]
        public string LoginUserName { get; set; }
        [Display(Name = "Enter Phone Number")]
        [Required(ErrorMessage = "This field Required")]
        [DataType(DataType.PhoneNumber)]
        public string AddNewUserPhone { get; set; }
        [Display(Name = "Enter the Name")]
        [Required(ErrorMessage = "This field Required")]
        public string NewUerName { get; set; }
        public string NewuserPictureProfile { get; set; }
        public Nullable<int> NewUserStatus { get; set; }
    }
}
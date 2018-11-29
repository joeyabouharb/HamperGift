using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
    public class UserPasswordViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Current Password"),
              DataType(DataType.Password),
        Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password Here"),
        DataType(DataType.Password),
        Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password Again to Confirm Password"),
         DataType(DataType.Password),
         Display(Name = "Confirm Password")]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPass { get; set; }

      
    }
}

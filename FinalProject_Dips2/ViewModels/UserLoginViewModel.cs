using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectUI.ViewModels
{
    public class UserLoginViewModel
    {
        [Required, MaxLength(256),
        Display(Name = "User Name")]
    
        public string UserName { get; set; }

        [Required,
         DataType(DataType.Password),
         Display(Name ="Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

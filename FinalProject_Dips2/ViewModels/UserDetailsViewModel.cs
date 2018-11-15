using ProjectUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.ViewModels
{
    public class UserDetailsViewModel
    {
        public string UserName { get; set; }
       
        

        [Required(ErrorMessage = "Please Enter An Email Address")
          , DataType(DataType.EmailAddress),
          Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter An Address Number")
           , DataType(DataType.Text),
        Display(Name = "Address")]
        public string DeliveryAddress { get; set; }

     

        [Required(ErrorMessage = "Please Enter A Post Code")
          , DataType(DataType.Text),
       Display(Name = "Post Code ")]
        [RegularExpression(@"^[0-9]{4}", ErrorMessage = "Please Enter A valide Post Code")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Please Select a AU State in the options below")
          , EnumDataType(typeof(AddressEnum)),
       Display(Name = "State")]
        public AddressEnum State { get; set; }

        [Required(ErrorMessage = "Please enter a Phone Number Below")
         , DataType(DataType.PhoneNumber),
            Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}

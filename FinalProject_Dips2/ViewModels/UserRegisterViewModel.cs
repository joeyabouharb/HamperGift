using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FinalProject_Dips2.Models;
namespace FinalProject_Dips2.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="Please Enter a Username"),
            MaxLength(256),
            Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Your Password"),
            DataType(DataType.Password),
            Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password to Confirm Password"),
            DataType(DataType.Password),
            Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter An Email Address")
            , DataType(DataType.EmailAddress),
            Display(Name ="Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter An Address Number")
            , DataType(DataType.Text),
         Display(Name = "Address Line 1 ")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Please Enter A Street Address")
            , DataType(DataType.Text),
         Display(Name = "Address Line 2")]
        public string DeliveryAddress2 { get; set; }

        [Required(ErrorMessage = "Please Enter A Post Code")
          , DataType(DataType.Text),
       Display(Name = "Post Code ")]
        [RegularExpression(@"^[0-9]{4}", ErrorMessage = "Please Enter A valide Post Code" )]
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
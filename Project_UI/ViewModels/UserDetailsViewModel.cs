using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
    public class UserDetailsViewModel
    {

   
       
       public IEnumerable<SelectListItem> Addresses { get; set; }

        [Required(ErrorMessage = "Please Enter An Email Address")
          , DataType(DataType.EmailAddress),
          Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter a Phone Number Below")
         , DataType(DataType.PhoneNumber),
            Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

		[Required]
		public string AddressId { get; set; }
    }
}

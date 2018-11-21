using Project_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
	public class UserAddressDetailsViewModel
	{
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

		public int AddressId {get; set;}
	}
}

using ProjectUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.ViewModels
{
    public class AdminAddProductViewModel
    {
        [Required, DataType(DataType.Text),
            Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required, DataType(DataType.Text),
            RegularExpression(@"^[0-9]", ErrorMessage = "Please Enter a Number"),
            Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required, EnumDataType(typeof(productSize)),
            Display(Name ="Net Size")]
        public productSize ProductSizeType { get; set; }

    }
}

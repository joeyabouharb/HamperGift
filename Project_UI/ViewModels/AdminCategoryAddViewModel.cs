using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_UI.ViewModels
{
    public class AdminCategoryAddViewModel
    {
        [Required, DataType(DataType.Text),
            Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}

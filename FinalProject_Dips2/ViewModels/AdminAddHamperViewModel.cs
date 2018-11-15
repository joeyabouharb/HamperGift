using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjectUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ProjectUI.ViewModels
{
    public class AdminAddHamperViewModel
    {
        [Required, DataType(DataType.Text), 
            Display(Name = "Hamper Name")]
        public string HamperName { get; set; }
       
        public IEnumerable<SelectListItem> FileNames { get; set; }

        [Required,
            Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required, DataType(DataType.Currency),
            Display(Name = "Cost")]
        public decimal Cost { get; set; }
       
      
        public IList<SelectListItem> CategoryNamesList { get; set; }

        [Required,
            Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public IEnumerable<ProductCheckList> ProductNamesList { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Project_Infastructure.Models;

namespace Project_UI.ViewModels
{
    public class HamperDetailsViewModel
    {
        public string Name {get; set;}
    

        public decimal Cost {get; set;}

        public int ImageId {get; set;}
        public string Category {get; set;}

        public IEnumerable<Product> Products { get; set; }
    }
}
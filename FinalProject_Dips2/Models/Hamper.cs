using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FinalProject_Dips2.Models
{
    public class Hamper
    {
      
        public int HamperId { get; set; }

        public string HamperName { get; set; }

        public int ImageId { get; set; }

     
      
        public double Cost { get; set; }

        public int CategoryId { get; set; }

        public ICollection<HamperProduct> HamperProducts { get; set; }

    }
}

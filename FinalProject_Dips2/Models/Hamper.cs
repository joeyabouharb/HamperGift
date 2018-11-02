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
      
        public Guid HamperId { get; set; }

        public string HamperName { get; set; }

        public string Description {get; set; }

        public Guid ImageId { get; set; }

        public Guid CategoryId { get; set; }

        public IList<HamperProduct> HamperProducts { get; set; }

    }
}

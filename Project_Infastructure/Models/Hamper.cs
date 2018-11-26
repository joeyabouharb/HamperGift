using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_Infastructure.Models
{
    public class Hamper
    {
      
        public int HamperId { get; set; }

        public string HamperName { get; set; }
        [ForeignKey("TblImage")]
        public int ImageId { get; set; }

        public decimal Cost { get; set; }
        [ForeignKey("TblCategory")]
        public int CategoryId { get; set; }

		public bool isDiscontinued { get; set; }

        public ICollection<HamperProduct> HamperProducts { get; set; }

        public ICollection<Cart> Carts { get; set; }

		public ICollection<Feedback> Feedbacks { get; set; }

    }
}

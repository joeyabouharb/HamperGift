using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinalProject_Dips2.Models
{
    public class Hamper
    {
     
        public int HamperId { get; set; }

        public string HamperName { get; set; }

        public string Description {get; set; }

        public byte[] Image { get; set; }

        
        public int CategoryId { get; set; }

        public IList<HamperProduct> HamperProducts { get; set; }

    }
}

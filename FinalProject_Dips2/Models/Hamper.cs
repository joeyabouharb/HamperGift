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

        public int CategoryId { get; set; }

        public string HamperDetails { get; set; }

        public byte HamperImage { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.Models
{
 
    public class Image
    {
        public int ImageId { get; set; }

        public byte[] Data { get; set; }

        public string FileName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public ICollection<Hamper> Hampers { get; set; }
    }
}

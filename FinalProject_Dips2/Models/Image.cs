using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject_Dips2.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }

        public byte[] Data { get; set; }

        public string FileName { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public ICollection<Hamper> Hampers { get; set; }
    }
}

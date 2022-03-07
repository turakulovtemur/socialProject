using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.Models
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileExtention { get; set; }

        public string ContentType { get; set; }

        public byte[] Body { get; set; }

        public DateTime UploadedDate { get; set; }
    }
}

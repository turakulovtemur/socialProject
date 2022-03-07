using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.Models
{
    public class Document
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public string Description { get; set; }

        public DateTime OrderDate { get; set; }

        public int FileId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}

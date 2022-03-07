﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public int FileId { get; set; }
    }
}

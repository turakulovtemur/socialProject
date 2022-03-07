﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.ViewModels
{
    public class CreateDocumentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }


        public IFormFile File { get; set; }
    }
}

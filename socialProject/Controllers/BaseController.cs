using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using socialProject.Models;

namespace socialProject.Controllers
{
    public class BaseController: Controller
    {
        public ClaimsPrincipal CurrentUser => User;
        private readonly UserManager<User> UserManager;
        public BaseController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }
    }
}

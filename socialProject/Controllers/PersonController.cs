using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using socialProject.Models;
using socialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialProject.Controllers
{
    public class PersonController : Controller
    {
        public readonly ApplicationContext context;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public PersonController(ApplicationContext db, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            context = db;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = Request.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userName);
            var passport = context.Pasports.FirstOrDefault(x => x.UserId == user.Id);
            var snils = context.Snils.FirstOrDefault(x => x.UserId == user.Id);

            var personViewModel = new PersonViewModel
            {
                FirstName = passport?.FirstName,
                Name = passport?.Name,
                LastName = passport?.LastName,
                DateOfBirth = passport?.DateOfBirth,
                DateOfIssue = passport?.DateOfIssue,
                BornPlace = passport?.BornPlace,
                Address = passport?.Address,
                PasportNumber = passport?.PasportNumber,
                PasportSeria = passport?.PasportSeria,
                DepartmentCode = passport?.DepartmentCode,
                IssueBy = passport?.IssueBy,
                SexOfPerson = passport?.SexOfPerson,
                SnilsNumber = snils?.Number,
                PhoneNumber = user?.PhoneNumber
            };



            return View(personViewModel);
        }
    }
}

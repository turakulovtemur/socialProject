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
    public class PasportController : Controller
    {
        ApplicationContext context;
        private readonly UserManager<User> _userManager;

        public PasportController(ApplicationContext db, UserManager<User> userManager)
        {
            context = db;
            _userManager = userManager;
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PasportViewModel model)
        {
            var userName = Request.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userName);
            User isIN = _userManager.FindByEmailAsync(userName).Result; //находит пользователя по Email
            if (ModelState.IsValid)
            {
                Pasport pasport = new Pasport
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    Name = model.Name,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth, //дата рождения
                    BornPlace = model.BornPlace, //место рождения
                    Address = model.Address, //адрес проживания
                    PasportSeria=model.PasportSeria, //серия паспорта
                    PasportNumber=model.PasportNumber, //номер паспорта
                    DepartmentCode =model.DepartmentCode, // код подразделения
                    DateOfIssue=model.DateOfIssue, // дата выдачи
                    IssueBy=model.IssueBy, //кем выдан
                    SexOfPerson=model.SexOfPerson, //пол человека
                    UserId = user?.Id
                };
                context.Pasports.Add(pasport);
                await context.SaveChangesAsync();

                var userInRole =  _userManager.IsInRoleAsync(isIN, "user");  //проверяет имеет ли пользователь указанную роль


                if (userInRole.Result) //проверяет имеет ли пользователь указанную роль если да то переход на страницу админа
                {
                    return RedirectToAction("Index", "Person");
                }
                else
                {
                    return RedirectToAction("Index", "Pasport");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Pasport pasport = await context.Pasports.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            if (pasport == null)
            {
                return NotFound();
            }

            return View(pasport);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(PasportViewModel model)
        {
            var userName = Request.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userName);

            User isIN =  _userManager.FindByEmailAsync(userName).Result; //находит пользователя по Email
            Pasport pasport = context.Pasports.Find(model.Id);
            if (ModelState.IsValid)
            {
                //pasport.Id = model.Id;
                pasport.FirstName = model.FirstName;
                pasport.Name = model.Name;
                pasport.LastName = model.LastName;
                pasport.DateOfBirth = model.DateOfBirth; //дата рождения
                pasport.BornPlace = model.BornPlace; //место рождения
                pasport.Address = model.Address; //адрес проживания
                pasport.PasportSeria = model.PasportSeria; //серия паспорта
                pasport.PasportNumber = model.PasportNumber; //номер паспорта
                pasport.DepartmentCode = model.DepartmentCode; // код подразделения
                pasport.DateOfIssue = model.DateOfIssue; // дата выдачи
                pasport.IssueBy = model.IssueBy; //кем выдан
                pasport.SexOfPerson = model.SexOfPerson; //пол человека
                pasport.UserId = user.Id;

                context.Pasports.Update(pasport);
                await context.SaveChangesAsync();

                var userInRole = _userManager.IsInRoleAsync(isIN, "admin");  //проверяет имеет ли пользователь указанную роль


                if (userInRole.Result) //проверяет имеет ли пользователь указанную роль если да то переход на страницу админа
                {
                    return RedirectToAction("AdminPage", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Pasport");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundResult();
            }
            var pasport = context.Pasports.FirstOrDefault(r => r.Id == id);
            if (pasport == null)
            {
                return new NotFoundResult();
            }
            return View(pasport);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var pasport = await context.Pasports.FindAsync(id);
            context.Pasports.Remove(pasport);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Pasport");
        }


     
        public IActionResult Index()
        {
            return View(context.Pasports);
        }
    }
}

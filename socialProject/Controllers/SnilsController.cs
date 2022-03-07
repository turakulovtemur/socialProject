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
    public class SnilsController : Controller
    {
        ApplicationContext context;
        private readonly UserManager<User> _userManager;
        public SnilsController(ApplicationContext db, UserManager<User> userManager)
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
        public async Task<IActionResult> Create(SnilsViewModel model)
        {
            var userName = Request.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userName);
            User isIN = _userManager.FindByEmailAsync(userName).Result; //находит пользователя по Email
            if (ModelState.IsValid)
            {
                Snils snils = new Snils
                {
                    Id = model.Id,                    
                    Number = model.Number,
                    UserId=user?.Id
                };

                context.Snils.Add(snils);
                await context.SaveChangesAsync();
                var userInRole = _userManager.IsInRoleAsync(isIN, "user");  //проверяет имеет ли пользователь указанную роль


                if (userInRole.Result) //проверяет имеет ли пользователь указанную роль если да то переход на страницу админа
                {
                    return RedirectToAction("Index", "Person");
                   
                }
                else
                {
                    return RedirectToAction("Index", "Snils");
                }
               
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Snils snils = await context.Snils.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            if (snils == null)
            {
                return NotFound();
            }

            return View(snils);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(SnilsViewModel model)
        {
            var userName = Request.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userName);
            Snils snils = context.Snils.Find(model.Id);
            User isIN = _userManager.FindByEmailAsync(userName).Result; //находит пользователя по Email
            if (ModelState.IsValid)
            {              
                snils.Number = model.Number;
                snils.UserId = user.Id;
                
                context.Snils.Update(snils);
                await context.SaveChangesAsync();

                var userInRole = _userManager.IsInRoleAsync(isIN, "user");  //проверяет имеет ли пользователь указанную роль


                if (userInRole.Result) //проверяет имеет ли пользователь указанную роль если да то переход на страницу админа
                {
                    return RedirectToAction("Index", "Person");

                }
                else
                {
                    return RedirectToAction("Index", "Snils");
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
            var snils = context.Snils.FirstOrDefault(r => r.Id == id);
            if (snils == null)
            {
                return new NotFoundResult();
            }
            return View(snils);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var snils = await context.Snils.FindAsync(id);
            context.Snils.Remove(snils);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Snils");
        }


        public IActionResult Index()
        {
            return View(context.Snils);
        }
    }
}

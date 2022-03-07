using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using socialProject.Models;
using socialProject.Services;
using socialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace socialProject.Controllers
{
    public class DocumentController : Controller
    {
        ApplicationContext context;
        private readonly UserManager<User> _userManager;
        private readonly IFileService fileService;

        public DocumentController(ApplicationContext db, UserManager<User> userManager, IFileService fileService)
        {
            context = db;
            _userManager = userManager;
            this.fileService = fileService;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User?.Identity?.Name;
                var user = await _userManager.FindByEmailAsync(userName);

                var extentions = model.File?.ContentType?.Split('/');
                string fileExtention = extentions?.Length > 1 ? extentions[1] : string.Empty;

                var file = new File
                {
                    ContentType = model.File?.ContentType,
                    FileExtention = fileExtention,
                    Body = await fileService.ReadDataFromFile(model.File),
                    Name = model.File?.FileName,
                    UploadedDate = DateTime.Now
                };

                context.Files.Add(file);
                await context.SaveChangesAsync();

                Document claim = new Document
                {
                    Id = model.Id,
                    Description = model.Description,
                    UserId = user?.Id,
                    FileId = file.Id
                };
                context.Documents.Add(claim);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Document сlaimModel = await context.Documents.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            if (сlaimModel == null)
            {
                return NotFound();
            }

            return View(сlaimModel);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(CreateDocumentViewModel model)
        {
            Document сlaimModel = context.Documents.Find(model.Id);
            if (ModelState.IsValid)
            {
                сlaimModel.Id = model.Id;
                сlaimModel.Description = model.Description;



                context.Documents.Update(сlaimModel);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
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
            var сlaimModel = context.Documents.FirstOrDefault(r => r.Id == id);
            if (сlaimModel == null)
            {
                return new NotFoundResult();
            }
            return View(сlaimModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var room = await context.Documents.FindAsync(id);
            context.Documents.Remove(room);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Index()
        {
            var userName = User?.Identity?.Name;
            var user = await _userManager.FindByEmailAsync(userName);
            var passport = context.Pasports.FirstOrDefault(x => x.UserId == user.Id);
            var document = context.Documents.Where(x => x.UserId == user.Id);

            var viewModel = document?.Select(x=> new DocumentViewModel
            {
                Id = x.Id,
                FirstName = passport.FirstName,
                Name = passport.Name,
                LastName = passport.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Description = x.Description,
                FileId = x.FileId
            }).ToList();

            return View(viewModel);
        }


        public async Task<IActionResult> AllDoc()
        {

            var docy = context.Documents.Select(x => x);


            return View(docy);
        }


        [HttpGet]
        public async Task<FileResult> DownloadFile(int fileId)
        {
            var file = context.Files.FirstOrDefault(x => x.Id == fileId);

            return File(file.Body, file.ContentType, file.Name);
        }
    }
}

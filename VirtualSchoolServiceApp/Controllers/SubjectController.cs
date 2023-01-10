using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SubjectController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;

        }
        public IActionResult Index()
        {
            IEnumerable<Subject> objSubjectsList = _db.Subjects;
            return View(objSubjectsList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"files\subjects");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ContentOfEducation != null)
                    {
                        var oldFilePath = Path.Combine(wwwRootPath, obj.ContentOfEducation.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ContentOfEducation = @"\files\subjects\" + fileName + extension;
                }
                _db.Subjects.Add(obj);
                _db.SaveChanges();
                ClassSubjects classSubjects = new ClassSubjects()
                {
                    SubjectId = obj.Id
                };
                _db.ClassSubjects.Add(classSubjects);
                _db.SaveChanges();
                TempData["success"] = "Subject created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var subject = _db.Subjects.Find(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subject obj)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Subject edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var subject = _db.Subjects.Find(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Subjects.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            _db.Subjects.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Subject deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

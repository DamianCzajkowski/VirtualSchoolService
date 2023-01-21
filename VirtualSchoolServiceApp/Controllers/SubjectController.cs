using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Administrator, Teacher, Student, Parent")]
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
            IEnumerable<Subject> objSubjectsList = _db.Subjects.Include(s => s.Teacher).ThenInclude(t => t.User);
            return View(objSubjectsList);
        }

        public IActionResult SubjectGrades(int? id)
        {
            return View();
        }

        // GET
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(_db.Set<Teacher>().Include(x => x.User), "Id", "User");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
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
                TempData["success"] = "Subject created successfully";
                return RedirectToAction("Index");
            }
            ViewData["Teachers"] = new SelectList(_db.Set<Teacher>().Include(x => x.User), "Id", "User");
            return View(obj);
        }

        // GET
        [Authorize(Roles = "Administrator, Teacher")]
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
            ViewData["Teachers"] = new SelectList(_db.Set<Teacher>().Include(x => x.User), "Id", "User");
            return View(subject);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Edit(Subject obj, IFormFile? file)
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
                _db.Subjects.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Subject edited successfully";
                return RedirectToAction("Index");
            }
            ViewData["Teachers"] = new SelectList(_db.Set<Teacher>().Include(x => x.User), "Id", "User");
            return View(obj);
        }

        // GET
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public IActionResult DeletePost(int? id)
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
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (subject.ContentOfEducation != null)
            {
                var oldFilePath = Path.Combine(wwwRootPath, subject.ContentOfEducation.TrimStart('\\'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _db.Subjects.Remove(subject);
            _db.SaveChanges();
            TempData["success"] = "Subject deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAllGrades(int? id)
        {
			var user = User.Identity.Name;
			var gradeList = _db.Grades.Include(g => g.Student).ThenInclude(s => s.User).Where(g => g.SubjectId == id);
			ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Student).First(u => u.UserName == user);
            if (applicationUser != null && applicationUser.IsStudent)
            {
                gradeList = _db.Grades.Include(g => g.Student).ThenInclude(s => s.User).Where(g => g.SubjectId == id && g.StudentId == applicationUser.Student.Id);
            }
			
            return Json(new { data = gradeList });
        }

        #endregion
    }
}

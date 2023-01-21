using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;


namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Teacher, Administrator, Parent")]
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GradeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult ParentIndex()
        {
            List<GradeVM> objGradeVMList = new List<GradeVM>();
            var user = User.Identity.Name;
            ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Student).First(u => u.UserName == user);
            foreach (var subject in _db.Subjects)
            {
                GradeVM gradeVM = new()
                {
                    Subject = subject,
                    Grades = _db.Grades.Where(g => g.StudentId == applicationUser.Student.Id && g.SubjectId == subject.Id).ToList(),
                    Student = applicationUser.Student
              
                };
                objGradeVMList.Add(gradeVM);
            }

            return View(objGradeVMList);
        }

        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Index()
        {
            IEnumerable<Grade> objGradeList = _db.Grades.Include(g => g.Student).Include(g => g.Subject).ThenInclude(s => s.Teacher).ToList();
            return View(objGradeList);
        }

        // GET
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Create()
        {
            ViewData["Students"] = new SelectList(_db.Set<Student>().Include(s => s.User), "Id", "User");
            ViewData["Subjects"] = new SelectList(_db.Set<Subject>(), "Id", "Name");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Create(Grade obj)
        {
            if (ModelState.IsValid)
            {
                _db.Grades.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Students"] = new SelectList(_db.Set<Student>().Include(s => s.User), "Id", "User");
            ViewData["Subjects"] = new SelectList(_db.Set<Subject>(), "Id", "Name");
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
            var grade = _db.Grades.Find(id);

            if (grade == null)
            {
                return NotFound();
            }
            ViewData["Students"] = new SelectList(_db.Set<Student>().Include(s => s.User), "Id", "User");
            ViewData["Subjects"] = new SelectList(_db.Set<Subject>(), "Id", "Name");
            return View(grade);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Edit(Grade obj)
        {
            if (ModelState.IsValid)
            {
                _db.Grades.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Students"] = new SelectList(_db.Set<Student>().Include(s => s.User), "Id", "User");
            ViewData["Subjects"] = new SelectList(_db.Set<Subject>(), "Id", "Name");
            return View(obj);
        }

        // GET
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var grade = _db.Grades.Find(id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Teacher")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var grade = _db.Grades.Find(id);

            if (grade == null)
            {
                return NotFound();
            }

            _db.Grades.Remove(grade);
            _db.SaveChanges();
            TempData["success"] = "Grade deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

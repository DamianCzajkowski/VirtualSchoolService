using Microsoft.AspNetCore.Mvc;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubjectController(ApplicationDbContext db)
        {
            _db = db;

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
        public IActionResult Create(Subject obj)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Add(obj);
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

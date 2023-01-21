using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
	[Authorize(Roles = "Teacher")]
	public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            IEnumerable<Student> objStudentsList = _db.Students.Include(s => s.Class).Include(s => s.User);
            return View(objStudentsList);
        }

        public IActionResult StudentGrades(int? id)
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAllGrades(int? id)
        {
            var gradeList = _db.Grades.Include(g => g.Subject).Where(g => g.StudentId == id);
            return Json(new { data = gradeList });
        }

        #endregion
    }
}

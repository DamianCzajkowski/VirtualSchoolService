using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GradeController(ApplicationDbContext db)
        {
            _db = db;

        }
        
        public IActionResult StudentIndex()
        {
            IEnumerable<Grade> objGradeList = _db.Grades.Include(g => g.Subject).ToList();
            return View(objGradeList);
        }
    }
}

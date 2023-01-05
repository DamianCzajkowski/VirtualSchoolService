using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
	public class UsersController : Controller
	{
		private readonly ApplicationDbContext _db;

		public UsersController(ApplicationDbContext db)
		{
			_db = db;

		}
		public IActionResult Index()
		{
			return View();
		}

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
				_db.ApplicationUsers.Add(obj);
                if (obj.IsStudent)
                {
                    Student student = new()
                    {
                        User = obj
                    };
                    _db.Students.Add(student);
				}
                if (obj.IsTeacher)
                {
                    Teacher teacher = new()
                    {
                        User = obj
                    };
                    _db.Teachers.Add(teacher);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var usersList = _db.ApplicationUsers;
            return Json(new { data = usersList });
        }

        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            var obj = _db.ApplicationUsers.Find(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _db.ApplicationUsers.Remove(obj);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful." });
        }
        #endregion
    }
}

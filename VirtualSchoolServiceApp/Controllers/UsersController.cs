using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
	{
		private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

		public UsersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
		{
			_db = db;
            _userManager = userManager;

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
        public async Task<IActionResult> Create(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
				//_db.ApplicationUsers.Add(obj);
                var role = "Administrator";
                if (obj.IsStudent)
                {
                    Student student = new()
                    {
                        User = obj
                    };
                    _db.Students.Add(student);
                    role = "Student";
				}
                if (obj.IsTeacher)
                {
                    Teacher teacher = new()
                    {
                        User = obj
                    };
                    _db.Teachers.Add(teacher);
                    role = "Teacher";
                }
				if (obj.IsParent)
				{
					Parent parent = new()
					{
						User = obj
					};
					_db.Parents.Add(parent);
                    role = "Parent";
				}

                var existingUser = await _userManager.FindByEmailAsync(obj.Email);

                if (existingUser == null)
                {
                    await _userManager.CreateAsync(obj, "Qwerty12#@.");
                    if (!string.IsNullOrWhiteSpace(role))
                    {
                        await _userManager.AddToRoleAsync(obj, role);
                    }
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

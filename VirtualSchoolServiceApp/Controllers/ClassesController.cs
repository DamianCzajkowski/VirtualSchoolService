using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClassesController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Class> objClassesList = _db.Classes.Include(s => s.Students);
            return View(objClassesList);
        }

        // GET
        public IActionResult Create()
        {
            ViewData["Supervisors"] = new SelectList(_db.Set<Teacher>().Include(x => x.User), "Id", "User.FirstName");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class obj)
        {
            if (ModelState.IsValid)
            {
                _db.Classes.Add(obj);
                _db.SaveChanges();
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
            var school_class = _db.Classes.Include(c=>c.Supervisor).ThenInclude(s => s.User).FirstOrDefault(c => c.Id == id);

            if (school_class == null)
            {
                return NotFound();
            }
            IEnumerable<Teacher> objClassesList = new List<Teacher>
            {
                school_class.Supervisor
            };
            ViewData["Supervisors"] = new SelectList(_db.Set<Teacher>().Include(x => x.User).Include(t => t.Class).Where(t => t.Class == null || t.Class==school_class), "Id", "User.FirstName");

            return View(school_class);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Class obj)
        {
            if (ModelState.IsValid)
            {
                _db.Classes.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult AddStudent(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AddStudentClassVM addStudentClassVM = new()
            {
                Class = _db.Classes.Find(id),
                StudentList = new MultiSelectList(_db.Set<Student>().Include(x => x.User).Where(s => s.ClassId != id), "Id", "User.FirstName")
            };
			ViewData["StudentId"] = id;
			return View(addStudentClassVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(int? id, AddStudentClassVM obj)
        {
            if (obj.Students != null)
            {
				var real_class = _db.Classes.Find(obj.Class.Id);
				foreach (var studentId in obj.Students)
				{
					var student_obj = _db.Students.Find(studentId);
					if (student_obj == null)
					{
						return NotFound();
					}

					real_class.Students.Add(student_obj);
				}
				_db.Update(real_class);
				_db.SaveChanges();
			}

			return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "Classes", action = "Edit", id }));
		}

        // GET
        public IActionResult AddSubject(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Subjects = _db.Subjects.Where(s => s.ClassSubjects.Count() == 0);
            AddSubjectClassVM addSubjectClassVM = new()
            {
                Class = _db.Classes.Find(id),
                SubjectList = new MultiSelectList(Subjects, "Id", "Name")
            };
			ViewData["SubjectId"] = id;
			return View(addSubjectClassVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSubject(int? id, AddSubjectClassVM obj)
        {
            if (obj.Subjects != null)
            {
                var real_class = _db.Classes.Find(obj.Class.Id);
                foreach (var subjectId in obj.Subjects)
                {
                    var subject_obj = _db.Subjects.Find(subjectId);
                    if (subject_obj == null)
                    {
                        return NotFound();
                    }
                    ClassSubjects classSubjects = new()
                    {
                        Subject = subject_obj,
                        SubjectId = subjectId,
                        Class = real_class,
                        ClassId = real_class.Id
                    };
                    _db.ClassSubjects.Add(classSubjects);
                }
                _db.SaveChanges();
            }

			return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "Classes", action = "Edit", id }));
		}



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Classes });
        }


        [HttpGet]
        public IActionResult GetAllStudents(int? id)
        {
            var school_class = _db.ApplicationUsers.Where(c => c.Student.ClassId == id);
            
            return Json(new { data = school_class });
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int? classId, string? userId)
        {
            var student_obj = _db.Students.Include(u=>u.User).FirstOrDefault(u => u.User.Id == userId);
            if (student_obj == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            var class_obj = _db.Classes.Find(classId);
            if (class_obj == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            class_obj.Students.Remove(student_obj);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful." });
        }

		[HttpDelete]
		public IActionResult Delete(int? classId)
		{
			var class_obj = _db.Classes.Find(classId);
			if (class_obj == null)
			{
				return Json(new { success = false, message = "Error while deleting." });
			}
			_db.Remove(class_obj);
			_db.SaveChanges();

			return Json(new { success = true, message = "Delete Successful." });
		}


		[HttpGet]
        public IActionResult GetAllSubjects(int? id)
        {
            var subjects = _db.ClassSubjects.Include(cs => cs.Subject).Where(cs => cs.ClassId == id);

            return Json(new { data = subjects });
        }

        [HttpDelete]
        public IActionResult RemoveSubject(int? classId, int? subjectId)
        {
            var class_subject_obj = _db.ClassSubjects.Where(c => c.ClassId == classId).FirstOrDefault(u => u.SubjectId == subjectId);
            if (class_subject_obj == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _db.ClassSubjects.Remove(class_subject_obj);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful." });
        }

        #endregion
    }
}

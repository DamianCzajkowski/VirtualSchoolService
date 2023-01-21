using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var user = User.Identity.Name;
            dynamic mymodel = new ExpandoObject();
            IEnumerable<Annoucement> objAnnoucementsList = new List<Annoucement>();
            if(user != null) 
            { 
                ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Student).Include(a => a.Teacher).ThenInclude(t => t.Class).Include(a => a.Parent).ThenInclude(p => p.Kids).ThenInclude(k => k.Class).ThenInclude(c => c.Supervisor).First(u => u.UserName == user);
                if (applicationUser != null && applicationUser.IsParent)
                {
                    if (applicationUser.Parent != null && applicationUser.Parent.Kids != null && applicationUser.Parent.Kids.Count() > 0) {
                        foreach (var student in applicationUser.Parent.Kids)
                        {
                            IEnumerable<Annoucement> objAnnoucementsList2 = _db.Annoucements.Include(a => a.Teacher).ThenInclude(t => t.User).Where(a => a.TeacherId == student.Class.Supervisor.Id);
                            objAnnoucementsList = objAnnoucementsList.Concat(objAnnoucementsList2);
                            mymodel.Annoucements = objAnnoucementsList;
                        }
                    }
                }
                else if (applicationUser != null && applicationUser.IsStudent) 
                {
                    List<GradeVM> objGradeVMList = new List<GradeVM>();
                    foreach (var subject in _db.Subjects)
                    {
                        var grades = _db.Grades.Where(g => g.StudentId == applicationUser.Student.Id && g.SubjectId == subject.Id).ToList();
                        if(grades.Any())
                        {
							GradeVM gradeVM = new()
							{
								Subject = subject,
								Grades = _db.Grades.Where(g => g.StudentId == applicationUser.Student.Id && g.SubjectId == subject.Id).ToList(),
								Student = applicationUser.Student

							};
							objGradeVMList.Add(gradeVM);
						}
                        
                    }
                    mymodel.Grades = objGradeVMList;
                }
            }
            
            return View(mymodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
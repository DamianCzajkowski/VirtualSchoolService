using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class AnnoucementController : Controller
    {
        private readonly ApplicationDbContext _db;
		private readonly IEmailSender _emailSender;

		public AnnoucementController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;

        }

        public IActionResult Index()
        {
            IEnumerable<Annoucement> objAnnoucementsList = _db.Annoucements.Include(a => a.Teacher).ToList();
            return View(objAnnoucementsList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnnoucementVM obj)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Teacher).ThenInclude(t => t.Class).First(u => u.UserName == user);

                Annoucement annoucement = new Annoucement()
                {
                    Teacher = applicationUser.Teacher,
                    Content = obj.Content,
                    Title = obj.Title
                };
                _db.Annoucements.Add(annoucement);

                _db.SaveChanges();
                foreach (var parent in _db.Parents.Include(p => p.User))
                {
                    _emailSender.SendEmailAsync(parent.User.Email,
                        "Annoucement from:  " + applicationUser,
                        obj.Content);
                }
				return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET
        public IActionResult Detail(int? id)
        {
            var annoucement = _db.Annoucements.First(m => m.Id == id);

            return View(annoucement);
        }
    }
}

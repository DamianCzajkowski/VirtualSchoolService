using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using VirtualSchoolServiceApp.Data;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Controllers
{
    [Authorize(Roles = "Teacher, Parent")]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MessageController(ApplicationDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            IEnumerable<Message> objMessageList = _db.Messages.ToList();
            return View(objMessageList);
        }

        // GET
        [Authorize(Roles = "Parent")]
        public IActionResult Create()
        {

            ViewData["Teachers"] = new SelectList(_db.Set<Teacher>().Include(t => t.User), "Id", "User");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Parent")]
        public IActionResult Create(MessageVM obj)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Parent).First(u => u.UserName == user);
                Message real_message = new Message()
                {
                    Context = obj.Context,
                    TeacherId = obj.TeacherId,
                    Title = obj.Title,
                    ParentId = applicationUser.Parent.Id
                };
                _db.Messages.Add(real_message);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        [Authorize(Roles = "Parent, Teacher")]
        public IActionResult Detail(int? id)
        {
            var message = _db.Messages.Include(m => m.MessageThreads).ThenInclude(mt => mt.Author)
                .Include(m => m.Parent)
                .ThenInclude(p => p.User)
                .Include(m => m.Teacher)
                .ThenInclude(p => p.User)
                .First(m => m.Id == id);
            
			return View(message);
        }

        // GET
        [Authorize(Roles = "Parent, Teacher")]
        public IActionResult SendResponse(int? id)
        {

            ViewData["MessageId"] = id;
			return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Parent, Teacher")]
        public IActionResult SendResponse(int? id, MessageThreadVM obj)
        {
            if (ModelState.IsValid)
            {
				var user = User.Identity.Name;
				ApplicationUser applicationUser = _db.ApplicationUsers.Include(a => a.Parent).First(u => u.UserName == user);
				MessageThread real_response = new MessageThread()
				{
					Context = obj.Context,
					MessageId = id,
					ApplicationUserId = applicationUser.Id
				};
				_db.MessageThreads.Add(real_response);
				_db.SaveChanges();
				return RedirectToAction("Detail", new RouteValueDictionary(new { controller = "Message", action = "Detail", id }));

			}
			return View(obj);
        }
    }
}

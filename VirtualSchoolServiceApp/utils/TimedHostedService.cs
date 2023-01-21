using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Text;
using VirtualSchoolServiceApp.Data;

namespace VirtualSchoolServiceApp.utils
{
	public class TimedHostedService : IJob
	{
		private readonly ApplicationDbContext _db;
		private readonly IEmailSender _emailSender;

		public TimedHostedService(ApplicationDbContext db, IEmailSender emailSender)
		{
			_db = db;
			_emailSender = emailSender;
		}
		public Task Execute(IJobExecutionContext context)
		{
			foreach (var parent in _db.Parents.Include(p => p.User).Include(p => p.Kids).ThenInclude(k => k.User))
			{
				foreach (var kid in parent.Kids)
				{
					var Grades = _db.Grades.Include(g => g.Subject).Where(g => g.StudentId == kid.Id).Where(g => g.DateTime > DateTime.Now.AddDays(-1)).ToList();
					StringBuilder sb = new StringBuilder();
					foreach (var grade in Grades)
					{
						sb.Insert(0, grade.Mark + ": " + grade.Subject.Name + "\n");
					}
					var task = _emailSender.SendEmailAsync(parent.User.Email,
						"Grades of " + kid.User,
						sb.ToString());
					task.Wait();
				}

			}

			return Task.CompletedTask;
		}
	}
}
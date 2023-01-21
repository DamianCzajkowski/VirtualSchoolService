using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace VirtualSchoolServiceApp.utils
{
	public class EmailSender: IEmailSender
	{
		public EmailSender(IConfiguration _config)
		{
			SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
		}

		public string SendGridSecret { get; }

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			//var emailToSend = new MimeMessage();
			//emailToSend.From.Add(MailboxAddress.Parse("hello@virtualschool.com"));
			//emailToSend.To.Add(MailboxAddress.Parse(email));
			//emailToSend.Subject = subject;
			//emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text= htmlMessage };

			//using (var emailClient = new SmtpClient())
			//{
			//	emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
			//	emailClient.Authenticate("virtualschoolserviceapp@gmail.com", "nkjfduUlZGrPHxx");
			//	emailClient.SendAsync(emailToSend);
			//	emailClient.Disconnect(true);
			//}
			//return Task.CompletedTask;

			var client = new SendGridClient(SendGridSecret);
			var from = new EmailAddress("nagok21251@quamox.com", "School Service");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
			return client.SendEmailAsync(msg);
		}
	}
}

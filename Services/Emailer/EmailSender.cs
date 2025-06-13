using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace zintersid.Services.Emailer
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Log to console for now (replace with actual email sending logic)
            Console.WriteLine($"Sending email to {email} with subject '{subject}' and message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
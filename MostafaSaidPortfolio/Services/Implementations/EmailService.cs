using System.Net.Mail;
using System.Threading.Tasks;
using MostafaSaidPortfolio.Services.Interfaces;

namespace MostafaSaidPortfolio.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            return SendEmailAsync(to, subject, body, false);
        }

        public Task SendEmailAsync(string to, string subject, string body, bool isHtml)
        {
            using var mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isHtml;

            using var smtp = new SmtpClient("smtp.yourserver.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("youruser", "yourpassword"),
                EnableSsl = true
            };
            return smtp.SendMailAsync(mail);
        }
    }
}

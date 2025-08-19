using System.Threading.Tasks;

namespace MostafaSaidPortfolio.Services.Interfaces
{
    public interface IEmailService
    {
        // Send an email
        Task SendEmailAsync(string to, string subject, string body);

        // Optional: Send email with HTML content
        Task SendEmailAsync(string to, string subject, string body, bool isHtml);
    }
}

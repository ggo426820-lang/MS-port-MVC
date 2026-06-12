using System.ComponentModel.DataAnnotations;

namespace MostafaSaidPortfolio.Domain.ViewModels.Contact;

/// <summary>
/// Contact form view model
/// </summary>
public class ContactViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(100)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [StringLength(5000)]
    public string Message { get; set; } = string.Empty;
}

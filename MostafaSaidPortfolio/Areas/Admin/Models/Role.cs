namespace MostafaSaidPortfolio.Areas.Admin.Models;

public class Role
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
    public int UserCount { get; set; }
    public IList<string> Permissions { get; set; } = new List<string>();
}

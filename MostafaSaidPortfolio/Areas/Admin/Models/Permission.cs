namespace MostafaSaidPortfolio.Areas.Admin.Models;

public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Group { get; set; } = string.Empty;
    public bool IsSystemPermission { get; set; }

    // Well-known permission names
    public static class Names
    {
        public const string BlogRead    = "blog.read";
        public const string BlogWrite   = "blog.write";
        public const string BlogDelete  = "blog.delete";
        public const string ProjectRead = "project.read";
        public const string ProjectWrite= "project.write";
        public const string UsersManage = "users.manage";
        public const string SettingsEdit= "settings.edit";
    }
}

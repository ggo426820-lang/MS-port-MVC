namespace MostafaSaidPortfolio.Areas.Admin.Models;

public enum SettingType { String, Int, Bool, Json }

public class SystemSetting
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    public SettingType Type { get; set; } = SettingType.String;
    public string Group { get; set; } = "General";
    public bool IsReadOnly { get; set; }
}

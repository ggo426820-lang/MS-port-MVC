using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MostafaSaidPortfolio.Areas.Admin.Components.TagHelpers;

/// <summary>
/// Usage: &lt;admin-card title="Recent Posts" class="..."&gt;content&lt;/admin-card&gt;
/// </summary>
[HtmlTargetElement("admin-card")]
public class AdminCardTagHelper : TagHelper
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        var existingClass = output.Attributes["class"]?.Value?.ToString() ?? "";
        output.Attributes.SetAttribute("class", ("stat-card " + existingClass).Trim());

        var content = await output.GetChildContentAsync();
        var header = string.IsNullOrEmpty(Title) ? "" :
            $"<div class=\"flex items-center justify-between mb-4\"><h3 class=\"font-semibold text-white\">{Title}</h3>" +
            (string.IsNullOrEmpty(Subtitle) ? "" : $"<span class=\"text-xs text-slate-400\">{Subtitle}</span>") + "</div>";

        output.Content.SetHtmlContent(header + content.GetContent());
    }
}

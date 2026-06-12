using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MostafaSaidPortfolio.Areas.Admin.Components.TagHelpers;

/// <summary>
/// Usage: &lt;admin-table&gt;&lt;thead&gt;...&lt;/thead&gt;&lt;tbody&gt;...&lt;/tbody&gt;&lt;/admin-table&gt;
/// Wraps the table in a responsive scroll container and applies data-table CSS class.
/// </summary>
[HtmlTargetElement("admin-table")]
public class AdminTableTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "overflow-x-auto");

        var content = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(
            "<table class=\"data-table\">" + content.GetContent() + "</table>");
    }
}

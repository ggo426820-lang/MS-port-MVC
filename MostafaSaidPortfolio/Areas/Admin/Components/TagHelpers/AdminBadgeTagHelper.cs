using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MostafaSaidPortfolio.Areas.Admin.Components.TagHelpers;

/// <summary>
/// Usage: &lt;admin-badge type="success"&gt;Active&lt;/admin-badge&gt;
/// Types: success | warning | danger | info | default
/// </summary>
[HtmlTargetElement("admin-badge")]
public class AdminBadgeTagHelper : TagHelper
{
    public string Type { get; set; } = "default";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        var css = Type.ToLowerInvariant() switch
        {
            "success" => "badge badge-green",
            "warning" => "badge badge-yellow",
            "danger"  => "badge badge-red",
            "info"    => "badge badge-blue",
            _         => "badge"
        };
        output.Attributes.SetAttribute("class", css);
        var content = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(content.GetContent());
    }
}

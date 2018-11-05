using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Northwind.Web.Helpers
{
    [HtmlTargetElement("a", Attributes = "image-id")]
    public class ImageLinkTagHelper : TagHelper
    {
        public string ImageId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("href", $"images/{ImageId}");
        }
    }
}

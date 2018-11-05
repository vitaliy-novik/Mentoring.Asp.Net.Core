using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Northwind.Web.Helpers
{
    [HtmlTargetElement("a", Attributes = "image-id")]
    public class ImageLinkTagHelper : TagHelper
    {
        public string ImageId { get; set; }

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UrlHelper urlHelper = new UrlHelper(ActionContext);
            output.Attributes.SetAttribute("href", urlHelper.RouteUrl("images", new { id = ImageId }));

        }
    }
}

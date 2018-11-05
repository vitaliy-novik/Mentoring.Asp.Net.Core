using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace Northwind.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent NorthwindImageLink<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper, string imageId, string linkText)
        {
            var a = htmlHelper.RouteLink(linkText, "images", new { id = imageId });

            var writer = new StringWriter();
            a.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.GetStringBuilder().ToString());
        }
    }
}

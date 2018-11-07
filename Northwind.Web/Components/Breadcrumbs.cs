using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Web.Components
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            string actionName = ViewContext.RouteData.Values["action"]?.ToString();
            string controllerName = ViewContext.RouteData.Values["controller"]?.ToString();
            string id = ViewContext.RouteData.Values["id"]?.ToString();
            List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>();
            breadcrumbs.Add(new BreadcrumbItem
            {
                Display = "Home",
                Controller = "Home",
                Action = "Index"
            });
            breadcrumbs.Add(new BreadcrumbItem
            {
                Display = controllerName,
                Action = "Index",
                Controller = controllerName
            });
            if (name != null)
            {
                breadcrumbs.Add(new BreadcrumbItem
                {
                    Display = name
                });
            }

            return View(breadcrumbs);
        }
    }

    public class BreadcrumbItem
    {
        public string Display { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public string Id { get; set; }
    }
}

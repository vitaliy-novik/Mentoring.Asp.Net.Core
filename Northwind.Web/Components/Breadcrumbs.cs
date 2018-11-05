using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Web.Components
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string actionName = ViewContext.RouteData.Values["action"].ToString();
            string controllerName = ViewContext.RouteData.Values["controller"].ToString();
            List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>();
            if (actionName != "Index")
            {

            }
            return View(breadcrumbs);
        }
    }

    public class BreadcrumbItem
    {
        public string Display { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
    }
}

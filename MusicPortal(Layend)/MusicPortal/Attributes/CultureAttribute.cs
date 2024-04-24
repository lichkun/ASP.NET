using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using MusicPortal.BLL.Interfaces;

namespace MusicPortal.Attributes
{
    public class CultureAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string? cultureName = null;
            var cultureCookie = context.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie;
            else
                cultureName = "ru";

            List<string> cultures = context.HttpContext.RequestServices.GetRequiredService<ILanguage>()
                                    .Languages().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}

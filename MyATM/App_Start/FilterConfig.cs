using System.Web;
using System.Web.Mvc;

namespace MyATM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyLoggingFilterAttribute());
        }
    }

    public class MyLoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            //Logger.logRequest(request.UserHostAddress);
            base.OnActionExecuted(filterContext);
        }
    }
}

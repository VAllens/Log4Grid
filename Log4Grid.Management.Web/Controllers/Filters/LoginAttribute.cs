using System;
using System.Web.Mvc;

namespace Log4Grid.Management.Web.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Codes.Utils.User != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MJ_CAIS.WebPortal.External
{
    public class RedirectNotActiveAttribute : ActionFilterAttribute
    {
        private readonly string _controllerName;
        private readonly string _actionName;

        public RedirectNotActiveAttribute(string actionName, string controllerName)
        {
            _controllerName = controllerName;
            _actionName = actionName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity != null && 
                filterContext.HttpContext.User.Identity.IsAuthenticated &&
                !filterContext.HttpContext.User.Claims.Any( c => c.Type == "Active"))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = _controllerName,
                        action = _actionName
                    }
                ));
            }

            base.OnActionExecuting(filterContext);
        }
    }

}

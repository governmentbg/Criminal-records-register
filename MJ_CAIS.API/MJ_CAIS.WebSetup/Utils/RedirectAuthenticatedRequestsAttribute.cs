using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace MJ_CAIS.WebSetup.Utils
{
    public class RedirectAuthenticatedRequestsAttribute : ActionFilterAttribute
    {
        private readonly string _controllerName;
        private readonly string _actionName;

        public RedirectAuthenticatedRequestsAttribute(string actionName, string controllerName)
        {
            _controllerName = controllerName;
            _actionName = actionName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace MvcRestauracja.Filters
{
    public class UserSessionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {


                var controllerName = context.RouteData.Values["controller"].ToString();
                var actionName = context.RouteData.Values["action"].ToString();
                var user = controller.HttpContext.Session.GetString("User");
                controller.ViewBag.IsLoggedIn = user != null;
                controller.ViewBag.IsAdmin = controller.HttpContext.Session.GetString("isAdmin");
                if (controllerName == "Login")
                {
                    return;
                }

                else
                {
                    if (user == null)
                    {
                        context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary {
                            { "controller", "Login"}
                            }
                        );

                    }
                }

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing after the action executes
        }
    }
}

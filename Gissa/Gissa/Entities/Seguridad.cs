using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gissa.Entities
{
    public class Seguridad : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","Login" },
                    { "action","IniciarSesion" }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
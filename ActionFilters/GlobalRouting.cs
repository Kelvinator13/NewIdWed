using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.ActionFilters
{
    public class GlobalRouting
    {
        public class GlobalRouting : IActionFilter
        {
            private readonly ClaimsPrincipal _claimsPrincipal;
            public GlobalRouting(ClaimsPrincipal claimsPrincipal)
            {
                _claimsPrincipal = claimsPrincipal;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var controller = context.RouteData.Values["controller"];
                if (controller.Equals("Home"))
                {
                    if (_claimsPrincipal.IsInRole("Guest"))
                    {
                        context.Result = new RedirectToActionResult("Index", "Guests", null);
                    }
                    else if (_claimsPrincipal.IsInRole("Planner"))
                    {
                        context.Result = new RedirectToActionResult("Index", "Planners", null);
                    }
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}

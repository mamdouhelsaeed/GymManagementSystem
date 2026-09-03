using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymManagementSystem.Filters
{
    /// <summary>
    /// Server-side enforcement of Admin-only access.
    /// Never trust the hidden/shown nav links alone - this filter re-checks
    /// the "IsAdmin" session value on every request to a decorated controller/action.
    /// </summary>
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("UserEmail");
            var isAdmin = context.HttpContext.Session.GetString("IsAdmin");

            if (string.IsNullOrEmpty(isLoggedIn))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (isAdmin != "true")
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }

    /// <summary>
    /// Ensures the user is logged in (Admin or Trainer) before accessing a page.
    /// </summary>
    public class LoggedInOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(isLoggedIn))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

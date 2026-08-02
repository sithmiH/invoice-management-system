using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InvoiceManagement.Web.Filters;

// filter that ensures the user is authenticated before accessing protected pages
public class SessionAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult(
                "Login",
                "Account",
                null);
        }

        base.OnActionExecuting(context);
    }
}

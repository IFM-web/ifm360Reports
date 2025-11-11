using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ifm360Reports.AuthFilter
{
  
        public class AuthenticationFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var userId = context.HttpContext.Session.GetString("UserName");

                if (string.IsNullOrEmpty(userId))
                {
                    var controller = (Controller)context.Controller;

               
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
           

        }
        }
    
}


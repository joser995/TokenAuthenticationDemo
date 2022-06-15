using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TokenAuthenticationDemo.TokenAuthentication;

namespace TokenAuthenticationDemo.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            bool result = true;
            if(!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            string token = string.Empty;
            int appId = 0;

            if (result) 
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                appId = Convert.ToInt32(context.HttpContext.Request.Headers.First(x => x.Key == "App").Value);
                
                if (!tokenManager.VerifyToken(appId, token))
                    result = false;
            }
            if (!result) 
            {
                context.ModelState.AddModelError("Unauthorized", "Access is restricted.");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }

        }
    }
}

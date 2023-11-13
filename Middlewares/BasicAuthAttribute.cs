using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuthExample.Middlewares
{
    public class BasicAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization is null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Un-authorized");

            }
            else
            {
                try
                {
                    {
                        if (!actionContext.Request.Headers.Authorization.Scheme.Equals(AuthenticationSchemes.Basic))
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Un-authorized");

                        }
                        else
                        {
                            string decodedAuthHeader = Encoding
                                .UTF8
                                .GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
                            string token = decodedAuthHeader.Substring(6);
                            string username = decodedAuthHeader.Split(':')[0];
                            string password = decodedAuthHeader.Split(':')[1];
                            if (!username.Equals("admin") && !password.Equals("admin123"))
                            {
                                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Un-authorized");

                            }

                        }
                        base.OnAuthorization(actionContext);
                    }
                }
                catch (Exception e)
                {

                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Un-authorized");

                    throw;
                }
          
            }

        }
    }
}

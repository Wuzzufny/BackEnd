using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
//using RAPAPI.Controllers;

namespace RAPAPI
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        //public PublishersController publishersController = new PublishersController();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string userPassword = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
                //if (publishersController.CheckCredentials(userName, userPassword))
                //{
                //    //authorized
                //}
                //else
                //{
                //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                //}
            }
        }
    }

    public class AuthenticationFilterDashboard : ActionFilterAttribute
    {
        //public UsersController usersController = new UsersController();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string userPassword = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
                //if (usersController.CheckCredentials(userName, userPassword))
                //{
                //    //authorized
                //}
                //else
                //{
                //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                //}
            }
        }
    }

    public class AuthenticationFilterOwner : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string userPassword = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
                if (userName == "ahmed.yehia@live.com" && userPassword == "ahmed01020281439")
                {
                    //authorized
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
    }

}
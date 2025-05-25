using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace projektiKomponentGITHUB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = System.Web.Security.FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    // User roles are stored in UserData as comma separated string
                    string[] roles = authTicket.UserData.Split(',');

                    var identity = new System.Security.Principal.GenericIdentity(authTicket.Name);
                    var principal = new System.Security.Principal.GenericPrincipal(identity, roles);

                    Context.User = principal;
                    System.Threading.Thread.CurrentPrincipal = principal;
                }
            }
        }

    }
}

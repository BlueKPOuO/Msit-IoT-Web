using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IoTWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {/*
        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }*/
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ExecuteTaskServiceCallScheduler.StartAsync().GetAwaiter().GetResult();

            SqlDependency.Start(ConfigurationManager.ConnectionStrings["SignalrConnection"].ConnectionString);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["SignalrConnection"].ConnectionString);
        }

        /*
        void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        {
            IIdentity id = Context.User.Identity;
            if (id.IsAuthenticated)
            {
                var a = db.AspNetUsers.Where(n => n.UserName == id.Name).Select(n => n.Id).First();
                var roles = db.AspNetUserRoles.Where(n => n.UserId == a).Select(n => n.RoleId).First();
                
                Context.User = new GenericPrincipal(id,new[] { roles });
            }
        }*/
    }
}

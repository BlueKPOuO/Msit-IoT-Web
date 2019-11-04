using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace IoTWeb.Areas.Admin.Hubs
{
    public class ParkingHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["SignalrConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("ShowStatus")]
        public static void ShowStatus()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ParkingHub>();
            context.Clients.All.show();
            context.Clients.All.showDetails();
        }
    }
}
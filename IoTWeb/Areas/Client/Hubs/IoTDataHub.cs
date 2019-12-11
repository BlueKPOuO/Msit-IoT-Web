using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTWeb.Areas.Client.Hubs
{
    public class IoTDataHub : Hub
    {
        [HubMethodName("Show")]
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<IoTDataHub>();

            context.Clients.All.show();
            context.Clients.All.displayIoTData();
        }
    }
}
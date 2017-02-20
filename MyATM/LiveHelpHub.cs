using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MyATM
{
    [Authorize]
    public class LiveHelpHub : Hub
    {
        public void SendMessage(string message)
        {
            var username = Context.User.Identity.Name;
            Clients.All.showMessage(username,message);
        }
    }
}
using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void Send(string groupid, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
            if (!string.IsNullOrEmpty(groupid))
            {
                Clients.Group(groupid).addNewMessageToPage(name, message);
            }
        }
        public void Join(string newid, string groupid)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
            Groups.Add(newid, groupid);
            //Clients.Group(groupid).addNewMessageToPage(groupid, ids + "," + newid);
        }
    }
    public class MyEndPoint : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast("Connection " + connectionId + " connected");
        }
    }
}
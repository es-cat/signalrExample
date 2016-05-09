using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using SignalR;
using System.Collections.Generic;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            if (this.Context.Request.Cookies.ContainsKey("group"))
            {
                var cookie = this.Context.Request.Cookies["group"];
                Clients.OthersInGroup(cookie.Value).addNewMessageToPage(this.Context.ConnectionId, "已連接");
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            var cookie = this.Context.Request.Cookies["group"];
            if (cookie != null)
            {
                Clients.OthersInGroup(cookie.Value).addNewMessageToPage(this.Context.ConnectionId, "已斷線");
            }
            return base.OnDisconnected();
        }
        public override Task OnReconnected()
        {
            var cookie = this.Context.Request.Cookies["group"];
            if (cookie != null)
            {
                Clients.OthersInGroup(cookie.Value).addNewMessageToPage(this.Context.ConnectionId, "已重新連接");
            }
            return base.OnReconnected();
        }
        public void Send(string groupid, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
            if (!string.IsNullOrEmpty(groupid))
            {
                Clients.Group(groupid).addNewMessageToPage(name, message);
            }
        }
        public void Join(string newid)
        {
            var cookie = this.Context.Request.Cookies["group"];
            var groupid = "";
            if (cookie != null)
            {
                groupid = cookie.Value;
            }
            Dictionary<string, List<string>> groupdic = (Dictionary<string, List<string>>)HttpContext.Current.Cache[groupid] ?? new Dictionary<string, List<string>>();
            groupdic[groupid] = (List<string>)groupdic[groupid] ?? new List<string>();
            if (!groupdic[groupid].Contains(newid))
                groupdic[groupid].Add(newid);
            Groups.Add(newid, groupid);
            //Clients.Group(groupid).addNewMessageToPage(groupid, ids + "," + newid);
        }
        public void Live(string newid)
        {
            var cookie = this.Context.Request.Cookies["group"];
            var groupid = "";
            if (cookie != null)
            {
                groupid = cookie.Value;
            }
            Dictionary<string, List<string>> groupdic = (Dictionary<string, List<string>>)HttpContext.Current.Cache[groupid] ?? new Dictionary<string, List<string>>();

            Groups.Add(newid, groupid);
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
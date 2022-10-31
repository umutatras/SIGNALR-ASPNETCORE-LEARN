using ChatSignalRExample.Data;
using ChatSignalRExample.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatSignalRExample.Hubs
{
    public class ChatHub:Hub
    {
        public async Task GetNickName(string nickName)
        {
            Client client = new Client
            {
                ConnectionId=Context.ConnectionId,
                NickName = nickName,
            };
            ClientSource.Clients.Add(client);
           await Clients.Others.SendAsync("clientJoined", nickName);
            await Clients.All.SendAsync("clients", ClientSource.Clients);
        }

        public async Task SendMessageAsync(string message, string clientName)
        {
            clientName = clientName.Trim();
            Client senderClient=ClientSource.Clients.FirstOrDefault(x=>x.ConnectionId==Context.ConnectionId);
            if(clientName == "Tümü")
            {
                await Clients.Others.SendAsync("receiveMessage",message,senderClient.NickName);
            }
            else
            {
                Client client=ClientSource.Clients.FirstOrDefault(x => x.NickName == clientName);
                await Clients.Client(client.ConnectionId).SendAsync("receiveMessage", message, senderClient.NickName);
            }
            

        }

        public async Task AddGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Group group=new Group
            {
                GroupName = groupName
            };
            group.Clients.Add(ClientSource.Clients.FirstOrDefault(x=>x.ConnectionId == Context.ConnectionId));
            GroupSource.Groups.Add(group);
            await Clients.All.SendAsync("groups",GroupSource.Groups);
        }
        public async Task AddClientToGroup(IEnumerable<string> groupNames)
        {
            Client client=ClientSource.Clients.FirstOrDefault(x=>x.ConnectionId==Context.ConnectionId);

            foreach (var group in groupNames)
            {
                Group _group = GroupSource.Groups.FirstOrDefault(g => g.GroupName == group);

                var result=_group.Clients.Any(x => x.ConnectionId == Context.ConnectionId);
                if(!result)
                {
                    _group.Clients.Add(client);
                    await Groups.AddToGroupAsync(Context.ConnectionId, group);
                }
             
            }
        }
        public async Task GetClientToGroup (string groupName)
        {
            
           Group group= GroupSource.Groups.FirstOrDefault(x => x.GroupName == groupName);

            await Clients.Caller.SendAsync("clients", groupName == "-1"?ClientSource.Clients:group.Clients);
        }
        public async Task SendGroupMessageAsync(string groupName,string message)
        {
            await Clients.Group(groupName).SendAsync("receiveMessage",message,ClientSource.Clients.FirstOrDefault(x=>x.ConnectionId==Context.ConnectionId).NickName);
        }
    }
}

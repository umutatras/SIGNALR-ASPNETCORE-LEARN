using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class MessageHub:Hub
    {
        //public async Task SendMessage(string message,IEnumerable<string> connectionIds)
        //{
        //public async Task SendMessage(string message, string groupName, IEnumerable<string> connectionIds)
        //{
        //public async Task SendMessage(string message,  IEnumerable<string> groups)
        //{
        public async Task SendMessage(string message,string groupName)
        {
            #region Caller
            //sadece servere'a bildirim gönderen clientele iletişim kurar
            //await Clients.Caller.SendAsync("receiveMessage", message);
            #endregion
            #region All
            // servere'a bağlı tüm clientele iletişim kurar
            //await Clients.All.SendAsync("receiveMessage", message);
            #endregion
            #region Other
            //ssadece servera bildirim gönderen client dışında servera bağlı olan tüm clientlara mesaj gönderir
            await Clients.Others.SendAsync("receiveMessage", message);
            #endregion

            //Hub clients Metotları
            #region AllExcept
            //belirtilen clientler hariç server'a bağlı olan tüm clientlere bildiride bulunur
            //await  Clients.AllExcept(connectionIds).SendAsync("receiveMessage",message);
            #endregion
            #region Client
            //istediğimiz cliente bildirim gönderiminde bulunur.
            //await Clients.Client(connectionIds.First()).SendAsync("receiveMessage", message);
            #endregion
            #region Clients
            //servere bağlı olan sadece belirtilen clientlere bildiride bulunur
            //await Clients.Clients(connectionIds).SendAsync("receiveMessage", message);
            #endregion
            #region Group
            //belirtilen gruptaki tüm clientlere bildiride bulunur
            //önce gruplar oluşturulmalı ve arından clientler gruba abone olmalı
            //await Clients.Group(groupName).SendAsync("receiveMessage", message);
            #endregion
            #region GroupExcept
            //belirtilen gruptaki,belirtilen clientlar dışındaki tüm clientlara mesaj iletmemizi sağlayan bir fonksiyondur

            //await Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMessage", message);

            #endregion
            #region Groups
            //birden çok gruptaki clienlera bildiride bulunmamızı sağlayan fonksiyondur.
            //await Clients.Groups(groups).SendAsync("receiveMessage", message);
            #endregion
            #region OthersInGroup
            //bildiride bulunan client haricinde gruptaki tüm clientlara bildiride bulunan fonksiyondur
            //await Clients.OthersInGroup(groupName).SendAsync("receiveMessage",message);
            #endregion
            #region User
            //
            #endregion
            #region Users
            #endregion

        }
        public override async Task OnConnectedAsync()
        {
           await  Clients.Caller.SendAsync("getConnectionId",Context.ConnectionId);
            
        }
        public async Task addGroup(string connectionId,string groupName)
        {
            await Groups.AddToGroupAsync(connectionId,groupName);
        }
    }
}

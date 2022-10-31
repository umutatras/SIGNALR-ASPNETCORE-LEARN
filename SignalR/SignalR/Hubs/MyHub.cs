using Microsoft.AspNetCore.SignalR;
using SignalR.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class MyHub : Hub<IMessageClient>
    {
        static List<string> clients = new List<string>();
        //public async Task SendMessageAsync(string message)
        //{
        //    //Client tarafında bulunan ve gönderilen mesajı almak için gerekli method
        //    await Clients.All.SendAsync("receiveMessage", message);
        //}
        //bir bağlantı gerçekleştiğinde haber veren fonksiyon
        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.All.SendAsync("userJoined", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserJoined(Context.ConnectionId);
           


        }

        //bir bağlantı kesildiğin  haber veren fonksiyon
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            clients.Remove(Context.ConnectionId);
            //await Clients.All.SendAsync("clients", clients);
            //await Clients.All.SendAsync("userLeaved", Context.ConnectionId);
            await Clients.All.Clients(clients);
            await Clients.All.UserLeaved(Context.ConnectionId);
        }
    }
}
/*
 * connectionıd=Huba bağlantı gerrçekeleştiren clientlere sistem tarafından verilen uniq bir değerdir.amacı clienleri birbirinden ayirmaktır
 * 
 * 
 * Strongly Typed Hubs
 * yazılım uygulamalarında sistemelr arası haberleşmeleri yahut ortak tanımalamaları metinsel/statik değerler üzerinden sağlamaya çalışmak olası hata yapma ihtimallerini artırmakta ve böylece sürece ister isteme ekstra bir zorluk kazandırmaktadır
 */
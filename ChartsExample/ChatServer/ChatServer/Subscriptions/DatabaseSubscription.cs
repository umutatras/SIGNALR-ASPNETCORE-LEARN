using ChatServer.Hubs;
using ChatServer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableDependency.SqlClient;

namespace ChatServer.Subscriptions
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class,new()
    {
        IConfiguration _configuration;
        IHubContext<SatisHub> _hubContext;
        public DatabaseSubscription(IConfiguration configuration, IHubContext<SatisHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        SqlTableDependency<T> _tableDependency;
        public  void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"),tableName);
            _tableDependency.OnChanged += async (o, e)=>{
               

                SatisDbContext context = new SatisDbContext();
                var data =( from personel in context.Personels
                            join satis in context.Satislars
                            on personel.id equals satis.PersonelId
                            select new
                            {
                                personel,
                                satis
                            }).ToList();
                List<object> datas = new List<object>();
                var personelIsımleri = data.Select(data => data.personel.Adi).Distinct().ToList();
                personelIsımleri.ForEach(personelIsımleri =>
                {
                    datas.Add(new
                    {
                        personeladi = personelIsımleri,
                        satıslar = data.where(s => s.personel.adi == p).select(s => s.satis.fiyat)).tolist();
                });
                });
            await _hubContext.Clients.All.SendAsync("receiveMessage",datas);

        };
            _tableDependency.OnError += (o, e) =>
              {
              };
            _tableDependency.Start();

        }

 
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }
    }
}

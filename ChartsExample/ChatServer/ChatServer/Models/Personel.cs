using System;
using System.Collections.Generic;

#nullable disable

namespace ChatServer.Models
{
    public partial class Personel
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
    }
}

//Service Broker:Sql server'da bir veri tabanına herhangi bir veri eklendiğinde/silindiğinde/güncellendiğinde bizlere bilgilendirmede bulunan bir servistir. Bir veritabanında yapılan değişiklikleri yakalayabilmek için service broker aktif olması gerekmektedir.

// Select name,is_broker_enabled From sys.databases broker servisinin açık olup olmadığını anlayabiliriz bu komutla
//ALTER DATABASE SatisDb SET Enable_Broker komutu ile aktif edebilebilir
//SqlTableDependency kütüphanesi ile yönetim sağlayabiliriz
﻿using System.Collections.Generic;

namespace ChatSignalRExample.Models
{
    public class Group
    {
        public string GroupName { get; set; }
        public List<Client> Clients  { get;  }=new List<Client>();
    }
}

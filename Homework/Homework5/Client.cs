using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{

    public class Client
    {
      
        public string Name { get; set; }
        public string ID { get; set; }

        public Client(string name,string id)
        {
            Name = name;
            ID = id;
        }

        public Client() { }

    
       
        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            return client != null&& Name==client.Name&&ID==client.ID;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()*10+ID.GetHashCode()*20;
        }

        public override string ToString()
        {
            return "Name : "+Name+" ID : "+ID;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{

    public class Client
    {

        private string Name;
        private string ID;

        public Client(string name,string id)
        {
            Name = name;
            ID = id;
        }

        public Client() { }

        public string name { get => Name; set => Name = value; }
        public string id { get => ID; set => ID = value; }

        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            return Name==client.Name&&ID==client.ID;
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

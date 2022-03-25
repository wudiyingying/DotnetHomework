using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    [Serializable]
    internal class Client
    {

        private string Name;
        private string ID;

        public Client(string name,string id)
        {
            Name = name;
            ID = id;
        }

        public string name { get => Name; }
        public string id { get => ID; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{

    public class Goods
    {
        private string Name;
        private string SerialNumber;
        private float Price;

        public Goods(string name,string number,float price)
        {
            Name = name;
            SerialNumber = number;
            Price = price;
        }

        public Goods() { }

        public float price { get => Price; set => Price = value;}
        public string serialNum { get => SerialNumber; set => SerialNumber = value; }
        public string name { get => Name; set => Name = value; }


        public override bool Equals(object obj)
        {
            Goods goods=obj as Goods;
            return Name == goods.Name && SerialNumber == goods.SerialNumber && Price == goods.Price;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()*10+SerialNumber.GetHashCode()*20;
        }

        public override string ToString()
        {
            return "Name : "+Name+" NUMBER : "+SerialNumber+" PRICE : "+Price;
        }
    }
}

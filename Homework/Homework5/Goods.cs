using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{

    public class Goods
    {
        public string Name { get; set; }
        public string GoodsId { get; set; }
        public float Price { get; set; }

        public Goods(string name,string number,float price)
        {
            Name = name;
            GoodsId = number;
            Price = price;
        }

        public Goods() { }

   
        public override bool Equals(object obj)
        {
            Goods goods=obj as Goods;
            return goods != null&& Name == goods.Name && GoodsId == goods.GoodsId && Price == goods.Price;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()*10+GoodsId.GetHashCode()*20;
        }

        public override string ToString()
        {
            return "Name : "+Name+" NUMBER : "+GoodsId+" PRICE : "+Price;
        }
    }
}

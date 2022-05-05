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
        private string Name;
        private string goodsId;
        private float Price;

        public Goods(string name,string number,float price)
        {
            Name = name;
            goodsId = number;
            Price = price;
        }

        public Goods() { }

        public float price { get => Price; set => Price = value;}
        [Key]
        public string GoodsId { get => goodsId; set => goodsId = value; }
        
        public string name { get => Name; set => Name = value; }

        public string OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        public OrderDetails ods { get; set; }

        public override bool Equals(object obj)
        {
            Goods goods=obj as Goods;
            return goods != null&& Name == goods.Name && goodsId == goods.goodsId && Price == goods.Price;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()*10+goodsId.GetHashCode()*20;
        }

        public override string ToString()
        {
            return "Name : "+Name+" NUMBER : "+goodsId+" PRICE : "+Price;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Homework12.Model
{
    public class Item
    {
        [Key]
        public string GoodID { get; set; }
        public string Name { get; set; }
        public double perPrice { get; set; }

        public double TotalPrice { get; set; }
        public Item()
        {
        }

        public Item(string name, double price)
        {
            GoodID = Guid.NewGuid().ToString();
            Name = name;
            perPrice = price;
        }

        public override bool Equals(object obj)
        {
            var goods = obj as Item;
            return goods != null &&
                   GoodID == goods.GoodID &&
                   Name == goods.Name;
        }

        public override int GetHashCode()
        {
            int hashCode;
            Int32.TryParse(GoodID, out hashCode);
            return hashCode;
        }
    }
}

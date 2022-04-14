using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homework6;

namespace Homework8
{
    public partial class Form2 : Form
    {
        public static Order orderT;
        public Form2()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.creat_button);
        }

        private void creat_button(object sender,EventArgs e) {
            Goods good = new Goods(textBox4.Text,textBox6.Text , (float)Convert.ToDouble(textBox5.Text));
            OrderDetails o = new OrderDetails(good, (float)Convert.ToDouble(textBox9.Text), Convert.ToInt16(textBox7.Text));
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            orderDetails.Add(o);
            Client c = new Client(textBox3.Text, textBox2.Text);
            orderT = new Order(textBox1.Text, c, orderDetails);

            this.Close();

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

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
        public  Order currentOrder;
        private OrderService os;
        public Form2(OrderService orderService)
        {
            InitializeComponent();
            os = orderService;
            this.button1.Click += new System.EventHandler(this.creat_button);
        }

        private void creat_button(object sender,EventArgs e) {
            try
            {
                Goods good = new Goods(textBox4.Text, textBox6.Text, (float)Convert.ToDouble(textBox5.Text));
                OrderDetails o = new OrderDetails("000005",good, (float)Convert.ToDouble(textBox9.Text), Convert.ToInt16(textBox7.Text));
                List<OrderDetails> orderDetails = new List<OrderDetails>();
                orderDetails.Add(o);
                Client c = new Client(textBox3.Text, textBox2.Text);
                currentOrder = new Order(textBox1.Text, c, orderDetails);
                os.addOrder(currentOrder);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("输入合法订单信息 "+ex.Message);
            }

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

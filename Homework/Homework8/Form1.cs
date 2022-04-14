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
    public partial class Form1 : Form
    {
        private List<Order> orders = OrderService.Import("..\\..\\s.xml");
        private List<NowOrders> nowOrders = new List<NowOrders>();
        private List<NowOrders> nowOrders1 = new List<NowOrders>();
        private OrderService os;
        
        public Form1()
        {
           
            InitializeComponent();
            reInit();
            os = new OrderService(orders);
            nowOrdersBindingSource.DataSource = nowOrders;

            this.button1.Click+=  new System.EventHandler(this.queryBynum);
            this.button2.Click += new System.EventHandler(this.deleteBynum);
            this.button3.Click += new System.EventHandler(this.creatOrder);
            this.button4.Click += new System.EventHandler(this.importOrder);
            this.button5.Click += new System.EventHandler(this.exportOrder);
        }

        private void reInit() {
            nowOrders.Clear();
            foreach (Order o in orders)
            {
                foreach (OrderDetails d in o.OrderDetails)
                {
                    nowOrders.Add(new NowOrders(o.orderNum, o.clientDetail.name, o.clientDetail.id, d.ToString()));
                }
            }
            
           
        }
        private void queryBynum(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                nowOrdersBindingSource.DataSource = nowOrders;
            }
            else
            {
                nowOrdersBindingSource.DataSource = nowOrders.Where(o => o.OrderNum == textBox1.Text).ToList<NowOrders>();
            }
        }

        private void deleteBynum(object sender, EventArgs e) {
            
            
            os.deleteOrderByNum(textBox2.Text);

            nowOrdersBindingSource.DataSource = nowOrders.Where(o => o.OrderNum != textBox2.Text).ToList<NowOrders>();
            
            reInit();

        }

        private void creatOrder(object sender, EventArgs e) {

            Form2 form2 = new Form2();
            form2.Show();
            os.addOrder(Form2.orderT);
            reInit();
            nowOrdersBindingSource.DataSource = nowOrders;
            
        }

        private void importOrder(object sender, EventArgs e) { 
            orders=OrderService.Import("..\\..\\s.xml");
            reInit();
            nowOrdersBindingSource.DataSource = nowOrders;

        }

        private void exportOrder(object sender, EventArgs e)
        {
            os.Export();
            reInit();
            nowOrdersBindingSource.DataSource = nowOrders;

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void form1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nowOrdersBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}

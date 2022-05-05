using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homework6;
using MySql.Data.EntityFramework;

namespace Homework8
{
    public partial class Form1 : Form
    {
        private List<Order> orders = OrderService.Import("..\\..\\s.xml");
        
        private OrderService orderService;
     
        public string txt1 { get; set; }
        public Form1()
        {
           
            InitializeComponent();
            
            orderService = new OrderService(orders);
            
            bindingSource1.DataSource= orderService.Orders;

            textBox1.DataBindings.Add("Text", this, "txt1");
            this.button1.Click+=  new System.EventHandler(this.queryBynum);
            this.button2.Click += new System.EventHandler(this.delete);
            this.button3.Click += new System.EventHandler(this.creatOrder);
            this.button4.Click += new System.EventHandler(this.importOrder);
            this.button5.Click += new System.EventHandler(this.exportOrder);

            
            
        }

        private void queryAll()
        {
            bindingSource1.DataSource = orderService.Orders;
            bindingSource1.ResetBindings(false);
        }

        private void queryBynum(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                queryAll();
            }
            else
            {
                List<Order> result = orderService.queryByOrderNum(txt1);
                if (result.Count == 0) MessageBox.Show("查询无效");
                bindingSource1.DataSource = result;
                
            }
        }

        private void delete(object sender, EventArgs e)
        {
            Order order = bindingSource1.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            orderService.deleteOrder(order);
            queryAll();
        }
        


        private void creatOrder(object sender, EventArgs e) {

            Form2 form2 = new Form2(orderService);
            form2.Show();
            queryAll();
            
        }

        private void importOrder(object sender, EventArgs e) { 
            orders=OrderService.Import("..\\..\\s.xml");
            
        }

        private void exportOrder(object sender, EventArgs e)
        {
           

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

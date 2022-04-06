using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework7
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        int n1 = 10;
        double leng = 100;
        
        public Form1()
        {
            InitializeComponent();
            graphics = panel2.CreateGraphics();
            graphics.Clear(Color.White);
            this.BackColor = Color.Gray;

        }

        private void draw_Click(object sender,EventArgs e)
        {
            
            try
            {
                n1 = Convert.ToInt16(numericUpDown1.Value);
                leng = Convert.ToInt16(numericUpDown2.Value);
                per1 = Convert.ToDouble(numericUpDown3.Value);
                per2 = Convert.ToDouble(numericUpDown4.Value);
                th1 = Convert.ToDouble(textBox1.Text);
                th2 = Convert.ToDouble(textBox2.Text);
              
            }
            catch
            {
                MessageBox.Show("检查输入格式");
            }

            drawCayleyTree(n1, 200, 300, leng, -Math.PI / 2);
        }

        void drawCayleyTree(int n,
            double x0, double y0, double leng, double th)
        {
            if (n == 0) return;

            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);
        }
        void drawLine(double x0, double y0, double x1, double y1)
        {
            Pen pen = Pens.Blue;
            switch (comboBox1.Text)
            {
                case "BLUE": pen = Pens.Blue; break;
                case "BLACK": pen = Pens.Black; break;
                case "RED": pen = Pens.Red; break;
                case "PINK": pen = Pens.Pink; break;
                case "GREEN": pen = Pens.Green; break;
            }
            graphics.DrawLine(
                    pen, (int)x0, (int)y0, (int)x1, (int)y1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }
        
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

       
    }
}

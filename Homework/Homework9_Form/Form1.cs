using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Homework9;

namespace Homework9_Form
{
    public partial class Form1 : Form
    {
        SimpleCrawler simpleCrawler = new SimpleCrawler();
        BindingSource source = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = source;
            simpleCrawler.DownloadPage += pageDownload;
            this.button1.Click += new System.EventHandler(this.btn1_Click);

        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") 
            {
                simpleCrawler.StartURL = textBox1.Text;
               
            }
            else
            {
                simpleCrawler.StartURL="http://www.cnblogs.com/dstang2000/";
            }

            simpleCrawler.Start();
           
          
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pageDownload(SimpleCrawler s,string arg1,string arg2)
        {
            var pageInfo = new { Index = source.Count + 1, URL = arg1, Status = arg2 };
            Action action = () => { source.Add(pageInfo); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        
    }
}

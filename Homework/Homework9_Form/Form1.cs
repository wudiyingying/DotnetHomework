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

namespace Homework9_Form
{
    public partial class Form1 : Form
    {
        private List<strInfo> strings = new List<strInfo>();
        private Hashtable urls = new Hashtable();
        private int count = 0;


        public Form1()
        {
            InitializeComponent();
            strInfoBindingSource.DataSource = strings;
            this.button1.Click += new System.EventHandler(this.btn1_Click);

        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                urls.Add(textBox1.Text, false);
            }
            else
            {
                string startUrl = "http://www.cnblogs.com/dstang2000/";
                urls.Add(startUrl, false);
            }

            new Thread(Crawl).Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Crawl()
        {
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 10) break;
                strings.Add(new strInfo("开始爬行"+current+"界面"));
                string html = DownLoad(current); // 下载
                urls[current] = true;

                count++;
                Parse(html, current);//解析,并加入新的链接
                strings.Add(new strInfo("爬行结束"));
                strInfoBindingSource.ResetBindings(false);
            }
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void Parse(string html, string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+(html|aspx|jsp)[""']";

            string strRef1 = @"(href|HREF)[]*=[]*[""']/[^""'#>]+(html|aspx|jsp)[""']";
            string strRef2 = @"(href|HREF)[]*=[]*[""']./[^""'#>]+(html|aspx|jsp)[""']";
            string strRef3 = @"(href|HREF)[]*=[]*[""']../[^""'#>]+(html|aspx|jsp)[""']";

            string currentRef1 = "^(https://|http://)[^/]*";
            string currentRef2 = "[/]$";
            string currentRef3 = "[/][^/]*[/]$";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                if (Regex.IsMatch(match.Value, strRef1))
                {
                    //根目录下把相对地址转化为绝对地址
                    string s = new Regex(currentRef1).Match(current).Value;

                    strRef = s + (match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>'));
                }
                else if (Regex.IsMatch(match.Value, strRef2))
                {
                    //当前目录下把相对地址转化为绝对地址
                    string s = current.Substring(0, current.Length - 1);

                    strRef = s + (match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>'));
                }
                else if (Regex.IsMatch(match.Value, strRef3))
                {
                    //上一级目录下把相对地址转化为绝对地址
                    string s = current.Substring(0, current.Length - new Regex(currentRef3).Match(current).Value.Length);

                    strRef = s + (match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>'));
                }
                else
                {
                    //绝对地址直接应用
                    strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                              .Trim('"', '\"', '#', '>');
                }

                if (strRef.Length == 0) continue;
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
    }
}

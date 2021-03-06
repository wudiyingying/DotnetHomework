using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Homework9
{
    internal class SimpleCrawler
    {
        private Dictionary<string, bool> downloaded = new Dictionary<string, bool>();
        private Queue<string> urls = new Queue<string>();
        public int MaxPage { get; set; } = 15;
     
        //解析出url
        private string urlDetectRef = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";
        //对URL进行处理
        private string urlParseRef = @"(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";

        public event Action<SimpleCrawler,string,string> DownloadPage;
        
        
        static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Enqueue(startUrl);
            myCrawler.DownloadPage += myCrawler.SimpleCrawler_DownloadPage;
            new Thread(myCrawler.Crawl).Start();  
        }

        private void SimpleCrawler_DownloadPage(SimpleCrawler arg1, string arg2, string arg3)
        {
            Console.WriteLine("下载" + arg2+ "\n"+ arg3);   
        }

        private void Crawl()
        {
            while (downloaded.Count <= MaxPage && urls.Count>0)
            {
                string current = urls.Dequeue();
                string html = DownLoad(current); // 下载
                Parse(html,current);//解析,并加入新的链接
            }
        }

   

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = downloaded.Count+1.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                DownloadPage(this, url, "success");
                downloaded.Add(url, true);
                return html;
            }
            catch (Exception ex)
            {
                downloaded.Add(url, false);
                Console.WriteLine(ex.Message);
                DownloadPage(this, url, "failed:"+ex.Message);
                return "";
            }
        }

        private void Parse(string html,string currentPage)
        {
    
            MatchCollection matches = new Regex(urlDetectRef).Matches(html);

            foreach (Match match in matches)
            {
                string url = match.Groups["url"].Value;
                if (url == null) continue;
                url=changeUrl(url, currentPage);
                Match linkUrlMatch = Regex.Match(url,urlParseRef);
                if (!downloaded.ContainsKey(url) && !urls.Contains(url))
                {
                    urls.Enqueue(url);
                }
            }
        }

        private string changeUrl(string url,string current)
        {
            if (url.Contains("://")) return url;

            if (url.StartsWith("//"))
            {
                Match urlMatch = Regex.Match(url, urlParseRef);
                string protocal = urlMatch.Groups["protocal"].Value;
                return protocal + ":" + url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(url, urlParseRef);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = current.LastIndexOf('/');
                return changeUrl(url, current.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return changeUrl(url.Substring(2), current);
            }

            return "";
        }
    }
}

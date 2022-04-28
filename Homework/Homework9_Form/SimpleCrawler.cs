using System;
using System.Collections;
using System.Collections.Concurrent;
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
        
        private ConcurrentDictionary<string, bool> DownloadedPages = new ConcurrentDictionary<string, bool>();
        private ConcurrentQueue<string> pending = new ConcurrentQueue<string>();
        public int MaxPage { get; set; } = 15;
     
        //解析出url
        private string urlDetectRef = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";
        //对URL进行处理
        private string urlParseRef = @"(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";

        public event Action<SimpleCrawler,string,string> DownloadPage;

        public string StartURL { get; set; }
        /*
        //测试用的event
        private void SimpleCrawler_DownloadPage(SimpleCrawler arg1, string arg2, string arg3)
        {
            Console.WriteLine("下载" + arg2+ "/n"+ arg3);   
        }

        
        public void Crawl()
        {
            
            while (downloaded.Count <= MaxPage && urls.Count>0)
            {
                urls.TryDequeue(out string current);
                string html = DownLoad(current); // 下载
                Parse(html,current);//解析,并加入新的链接
            }
        }



        public void addUrl(string url)
        {
            urls.Enqueue(url);
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
                downloaded.TryAdd(url,true);
                return html;
            }
            catch (Exception ex)
            {
                downloaded.TryAdd(url,false);
                Console.WriteLine(ex.Message);
                DownloadPage(this, url, "failed:"+ex.Message);
                return "";
            }
        }
        */
        public int MaxParallel = 100;
        public SimpleCrawler()
        {
            MaxPage = 100;
          
        }

        public async Task Start()
        {
            DownloadedPages.Clear();
            while (pending.TryDequeue(out string result)) { }
            pending.Enqueue(StartURL);

            while (DownloadedPages.Count < MaxPage && pending.Count > 0)
            {
                if (MaxParallel > 0 && DownloadedPages.Count > MaxParallel)
                {
                    await Task.Delay(100);
                    continue;
                }
                string url;
                pending.TryDequeue(out url);
                try
                {
                    string html = await DownLoad(url); // 下载
                    DownloadedPages[url] = true;
                    DownloadPage(this, url, "success");
                    Parse(html, url);//解析,并加入新的链接
                }
                catch (Exception ex)
                {
                    DownloadPage(this, url, "  Error:" + ex.Message);
                }
            }
         
        }

        private async Task<string> DownLoad(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string html = await webClient.DownloadStringTaskAsync(url);
            string fileName = DownloadedPages.Count.ToString();
            await Task.Factory.StartNew(() => File.WriteAllText(fileName, html, Encoding.UTF8));
            return html;
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
                if (!DownloadedPages.ContainsKey(url) && !pending.Contains(url))
                {
                    pending.Enqueue(url);
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

﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HMTL.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            string content= SimulatePost();
            GetInfo(content);
            Console.ReadLine();
        }
        /*
         * 此方法分析读取页面信息
         */
        public static void GetInfo(string content)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            document.LoadHtml(content);

            // 获取Table的节点
            HtmlNodeCollection table = document.DocumentNode.SelectNodes("//table");

            if (table != null)
            {
                // 遍历table下的所有tr节点
                IEnumerable<HtmlNode> trs = table.Descendants("tr");
                foreach (HtmlNode tr in trs)
                {
                    // 遍历每个tr节点下的所有td节点
                    IEnumerable<HtmlNode> tds = tr.Descendants("td");
                    foreach(HtmlNode td in tds)
                    {
                        //输出每个td节点下的内容
                        Console.WriteLine(td.InnerText);
                    }
                }
            }
        }
        public static string SimulatePost()
        {
            StringBuilder pars = new StringBuilder();
            pars.Append("aa","dd");
            WebClient webClient = new WebClient();
            byte[] page = webClient.DownloadData("http://www.randstad.com/press/news/");
            string content = System.Text.Encoding.UTF8.GetString(page);
            return content;

        }
    }
}

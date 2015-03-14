using HtmlAgilityPack;
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
            pars.Append("__EVENTTARGET=ctl09_ctl04_nlGridView");
            pars.Append("&__EVENTARGUMENT=");
            pars.Append("&ctl09$ctl04$YearDropDownList=2009");

            byte[] postData = Encoding.UTF8.GetBytes(pars.ToString());//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = "http://www.randstad.com/press/news/";//地址  
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string content = Encoding.UTF8.GetString(responseData);//解码 

            return content;

        }
    }
}

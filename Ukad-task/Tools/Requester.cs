using System;
using System.Net;
using HtmlAgilityPack;
using Ukad_task.Models;
using System.Diagnostics;

namespace Ukad_task.Tools
{
    public class Requester
    {
        public WebPage GetPage(WebPage page)
        {
            Stopwatch sw = new Stopwatch();
            var doc = new HtmlDocument();
            try
            {
                sw.Start();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(page.Address);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                doc.Load(resp.GetResponseStream());
                sw.Stop();
                
                page.Doc = doc;
                page.MaxSpeed = sw.Elapsed.Milliseconds;
                page.MinSpeed = sw.Elapsed.Milliseconds;
                return page;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
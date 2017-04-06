using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Ukad_task.Models;

namespace Ukad_task.Tools
{
    public class Parser
    {
        public List<WebPage> ParsePage(WebPage page, string website, List<WebPage> pageList)
        {
            List<WebPage> hrefList = new List<WebPage>();
            try
            {
                foreach (HtmlNode link in page.Doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (pageList.FirstOrDefault(p => p.Address == website + hrefValue) != null
                        || hrefList.FirstOrDefault(p => p.Address == website + hrefValue) != null
                        || ((hrefValue.Contains("http://") || (hrefValue.Contains("https://"))) && hrefValue != website && !hrefValue.Contains(website))
                        || hrefValue.Length < 2)
                        continue;
                    hrefList.Add(new WebPage(website + hrefValue, 0, 0));
                }
                return hrefList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
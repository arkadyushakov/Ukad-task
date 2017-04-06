using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ukad_task.Models.Abstract
{
    public interface IPageRepository
    {
        List<string> GetWebsites();
        List<WebPage> GetPages(string url);
        string GetWebsiteByUrl(string address);
        void Save(string website, List<WebPage> pages);
    }
}
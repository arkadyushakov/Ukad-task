using System;
using System.Collections.Generic;
using System.Linq;
using Ukad_task.Models.Abstract;

namespace Ukad_task.Models
{
    public class PageRepository : IPageRepository
    {
        UkadDBEntities dbContext;

        public PageRepository()
        {
             dbContext = new UkadDBEntities();
        }   

        public List<string> GetWebsites()
        {
            return dbContext.websites.Select(w => w.url).ToList();
        }

        public List<WebPage> GetPages(string url)
        {
            List<WebPage> pageList = new List<WebPage>();
            var pages = dbContext.pages.Where(p => p.website1.url == url).ToList();
            foreach (var page in pages)
            {
                pageList.Add(new WebPage(page.url, Convert.ToInt32(page.min_speed), Convert.ToInt32(page.max_speed)));
            }   
            return pageList;
        }

        public string GetWebsiteByUrl(string address)
        {
            return dbContext.websites.Select(w => w.url).Where(u => u == address).FirstOrDefault();
        }

        public void Save(string website, List<WebPage> pages)
        {
            var site = new website();
            site.url = website;
            dbContext.websites.Add(site);
            dbContext.SaveChanges();
            foreach (var p in pages)
            {
                var page = new page();
                page.max_speed = p.MaxSpeed;
                page.min_speed = p.MinSpeed;
                page.url = p.Address;
                page.website = dbContext.websites.Where(w => w.url == website).Select(w => w.id_website).SingleOrDefault();
                dbContext.pages.Add(page);
            }
            dbContext.SaveChanges();
        }
    }
}
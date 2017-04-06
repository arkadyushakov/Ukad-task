using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ukad_task.Models;
using Ukad_task.Tools;
using Ukad_task.Models.Abstract;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        private List<WebPage> pageList;
        private string website;
        private IPageRepository repository;
        private ViewModel newModel;

        public HomeController()
        {
            repository = new PageRepository();
            pageList = new List<WebPage>();
            GetHistory();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Index(ViewModel model)
        {
            if (repository.GetWebsiteByUrl(model.Url) == null)
            {
                Requester requester = new Requester();
                Parser parser = new Parser();
                website = model.Url;
                var homePage = requester.GetPage(new WebPage(model.Url, 0, 0));
                if (homePage == null)
                {
                    return PartialView("~/Views/Partials/TableAndChart.cshtml", null);
                }
                pageList.Add(homePage);
                pageList.AddRange(parser.ParsePage(homePage, website, pageList));
                for (int i = 1; i < pageList.Count; i++)
                {
                    if (requester.GetPage(pageList[i]) != null)
                    {
                        var page = requester.GetPage(pageList[i]);
                        var pages = parser.ParsePage(page, website, pageList);
                        if (pages != null)
                        { 
                            pageList.AddRange(pages);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                newModel = new ViewModel(pageList.OrderByDescending(p => p.MaxSpeed).ToList(), pageList.Max(p => p.MaxSpeed));
                repository.Save(website, pageList);
                return PartialView("~/Views/Partials/TableAndChart.cshtml", newModel);
            }
            else
            {
                newModel = new ViewModel(repository.GetPages(model.Url).OrderByDescending(p => p.MaxSpeed).ToList(), pageList.Max(p => p.MaxSpeed));
                return PartialView("~/Views/Partials/TableAndChart.cshtml", newModel);
            }
        }

        [HttpPost]
        public PartialViewResult History()
        {
            string address = Request.Form["Items"].ToString();
            pageList = repository.GetPages(address).OrderByDescending(p => p.MaxSpeed).ToList();
            if (pageList.Count == 0)
                return PartialView("~/Views/Partials/TableAndChart.cshtml", null);
            newModel = new ViewModel(pageList, pageList.Select(p=>p.MaxSpeed).First());
            return PartialView("~/Views/Partials/TableAndChart.cshtml", newModel);
        }

        public void GetHistory()
        {
            List<string> items = new List<string>();
            items = repository.GetWebsites();
            ViewBag.Items = new SelectList(items);
        }

    }
}   
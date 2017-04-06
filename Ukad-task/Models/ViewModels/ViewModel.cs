using System;
using System.Collections.Generic;

namespace Ukad_task.Models
{
    public class ViewModel
    {
        public ViewModel()
        { }
        public ViewModel(List<WebPage> pages, Int32 slowestSpeed)
        {
            this.Pages = pages;
            this.SlowestSpeed = slowestSpeed;
        }
        public string Url { get; set; }
        public List<WebPage> Pages { get; set; }
        public Int32 SlowestSpeed { get; set; }
    }
}
using System;
using HtmlAgilityPack;

namespace Ukad_task.Models
{
    public class WebPage
    {
        public WebPage()
        { } 

        public WebPage(string address, Int32 minSpeed, Int32 maxSpeed)
        {
            this.Address = address;
            this.MinSpeed = minSpeed;
            this.MaxSpeed = maxSpeed;
        }

        public string Address { get; set; }
        public Int32 MinSpeed { get; set; }
        public Int32 MaxSpeed { get; set; }
        public HtmlDocument Doc { get; set; }
    }
}
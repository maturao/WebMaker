using System;
using System.Collections.Generic;
using System.IO;
using WebMaker.Server;
using WebMaker.Template;
using WebMaker.Web.Elements;
using WebMaker.Web.General;
using System.Runtime.CompilerServices;

namespace WebMaker.ConsoleTest
{

    public class Program
    {
        public static void Main2()
        {
            var webElement = new WebHeader(4) { Content = "hmmmm" };
            var template = WebElementTemplate.FromWebElement(webElement);
            var wl = template.ToBlankWebElement();
        }
        
        public static void Main(string[] args)
        {
            //Main2();
            //return;
            var webSite = new WebSite()
            {
                new WebPage("Main page")
                {
                    new WebHeader(1) { Content = "FirstTitle on First page"},
                    new WebParagraph() {Content = "ěščřžýáíé Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis arcu arcu, ultrices in euismod quis, bibendum nec lectus. Nulla imperdiet sapien bibendum, consectetur odio eu, ultrices arcu. Nullam in nisi in ante eleifend convallis. Nullam lacinia justo quis turpis aliquam sagittis. Nulla ac scelerisque ex, sollicitudin facilisis erat."},
                    new WebHeader(6) {Content = "lowlevel header"}
                },
                new WebPage("Second page")
                {
                    new WebParagraph() {Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis arcu arcu, ultrices in euismod quis, bibendum nec lectus. Nulla imperdiet sapien bibendum, consectetur odio eu, ultrices arcu. Nullam in nisi in ante eleifend convallis. Nullam lacinia justo quis turpis aliquam sagittis. Nulla ac scelerisque ex, sollicitudin facilisis erat."},
                    new WebParagraph() {Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis arcu arcu, ultrices in euismod quis, bibendum nec lectus. Nulla imperdiet sapien bibendum, consectetur odio eu, ultrices arcu. Nullam in nisi in ante eleifend convallis. Nullam lacinia justo quis turpis aliquam sagittis. Nulla ac scelerisque ex, sollicitudin facilisis erat."},
                    new WebParagraph() {Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis arcu arcu, ultrices in euismod quis, bibendum nec lectus. Nulla imperdiet sapien bibendum, consectetur odio eu, ultrices arcu. Nullam in nisi in ante eleifend convallis. Nullam lacinia justo quis turpis aliquam sagittis. Nulla ac scelerisque ex, sollicitudin facilisis erat."},
                    new WebParagraph() {Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis arcu arcu, ultrices in euismod quis, bibendum nec lectus. Nulla imperdiet sapien bibendum, consectetur odio eu, ultrices arcu. Nullam in nisi in ante eleifend convallis. Nullam lacinia justo quis turpis aliquam sagittis. Nulla ac scelerisque ex, sollicitudin facilisis erat."}
                },
                new WebPage("Third page")
                {
                    new WebHeader(1) {Content = "Header1"},
                    new WebList(true) {Content = new List<string> { "First", "Second", "Third" }},
                    new WebHeader(2) {Content = "Header2"},
                    new WebHeader(3) {Content = "Header3"},
                    new WebHeader(4) {Content = "Header4"},
                    new WebHeader(5) {Content = "Header5"},
                    new WebHeader(6) {Content = "Header6"},
                },
                new WebPage("Final Page")
                {
                    new WebHeader(2) { Content = "My Small Page"},
                    new WebParagraph() {Content = "hello there my friends"},
                    new WebImage() {Content = "https://www.cesarsway.com/sites/newcesarsway/files/styles/large_article_preview/public/Natural-Dog-Law-2-To-dogs%2C-energy-is-everything.jpg?itok=Z-ujUOUr"},
                    new WebList(false) {Content = new List<string> {"Hello", "Meine", "Freunde"}}
                }
            };


            var template = WebSiteTemplate.FromWebSite(webSite);
            string json = template.Serialize();
            //File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "abcd.json"), json);
            var templateFromJson = WebSiteTemplate.Deserialize(json);
            var blankSite = templateFromJson.ToBlankWebSite();

            blankSite.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test/"));
            
        }

        public static void CallerName([CallerMemberName] string callerName = "")
        {
            Console.WriteLine(callerName);
        }
    }

    internal class WebSiteProvider : IWebSiteProvider
    {
        public WebSiteProvider(WebSite webSite)
        {
            WebSite = webSite;
        }

        public WebSite WebSite { get; }
    }
}
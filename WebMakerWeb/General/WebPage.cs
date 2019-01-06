using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WebMaker.Html.Elements;
using WebMaker.Html.General;

namespace WebMaker.Web.General
{
    /// <summary>
    /// Třída reprezentující webovou stránku (kolekce webových elementů)
    /// </summary>
    public class WebPage : List<WebElement>
    {
        private const string htmlExtension = ".html";
        private const string mainPageSimpleTitle = "index";

        /// <summary>
        /// Výtvoří instanci třídy WebPage
        /// </summary>
        /// <param name="title">Titulek stránky</param>
        public WebPage(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Název souboru stránky
        /// </summary>
        public string Filename => (IsMain ? mainPageSimpleTitle : SimplifyText(Title)) + htmlExtension;

        /// <summary>
        /// Zda je stránka hlavní na webu
        /// </summary>
        public bool IsMain { get; internal set; } = false;

        /// <summary>
        /// Titulek stránky
        /// </summary>
        public string Title { get; set; }

        internal HtmlCollection Content => new HtmlCollection(this.Select(el => el.HTMLElement).ToList());

        internal MainElement MainElement
        {
            get
            {
                var main = new MainElement();
                main.Children.AddRange(Content);
                return main;
            }
        }

        /// <summary>
        /// Uloží stránku do složky
        /// </summary>
        /// <param name="folder">Složka kam stránku uložit</param>
        /// <param name="webSite">Web, ke které stránka patří</param>
        public void Save(string folder, WebSite webSite)
        {
            File.WriteAllText(Path.Combine(folder, Filename), GenerateHtmlDocument(webSite).ToString());
        }

        internal HtmlDocument GenerateHtmlDocument(WebSite webSite)
        {
            var doc = new HtmlDocument(Title, webSite.GetNavElement(this), MainElement);
            doc.Head.Children.AddRange(webSite.SharedHeadElements);
            return doc;
        }

        private static string SimplifyText(string text)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(Regex.Replace(text.ToLowerInvariant(), @"\W", string.Empty));
            var asciiString = Encoding.ASCII.GetString(asciiBytes);
            return asciiString.Replace("?", string.Empty);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using WebMaker.Html.Elements;
using WebMaker.Html.General;

namespace WebMaker.Web.General
{
    /// <summary>
    /// Třída reprezentující web (kolekce webových stránek)
    /// </summary>
    public class WebSite : List<WebPage>
    {
        private const string selectedCssClass = "selected";

        internal HtmlCollection SharedHeadElements
        {
            get
            {
                return new HtmlCollection()
                {
                    new MetaElement()
                    {
                        Charset = "utf-8"
                    },
                    //new LinkElement()
                    //{
                    //    Rel = "stylesheet",
                    //    Href = "style.css"
                    //},
                    WebSiteStyle.StyleElement
                };
            }
        }

        /// <summary>
        /// Webový styl webu
        /// </summary>
        public WebStyle WebSiteStyle { get; } = new BasicWebStyle();

        /// <summary>
        /// Přidá webovou stránku
        /// </summary>
        /// <param name="webPage">Webová stránka k přidání</param>
        public new void Add(WebPage webPage)
        {
            if (Count == 0)
            {
                webPage.IsMain = true;
            }
            base.Add(webPage);
        }

        /// <summary>
        /// Získá HTML kód hlavní stránky
        /// </summary>
        /// <returns></returns>
        public string GetMainPageHtml() => GetPageHtml(Find(page => page.IsMain));

        /// <summary>
        /// Vygeneruje nav element
        /// </summary>
        /// <param name="selected">Stránka, které vygenerovat nav element</param>
        /// <returns>Vygenerovaný nav element</returns>
        public NavElement GetNavElement(WebPage selected)
        {
            var nav = new NavElement();

            var navElements = this.Select(page =>
            {
                var aElement = new AElement()
                {
                    InnerText = page.Title,
                    Href = page.Filename
                };
                if (page == selected)
                {
                    aElement.ClassList.Add(selectedCssClass);
                }
                return aElement;
            });

            nav.Children.AddRange(navElements);

            return nav;
        }

        /// <summary>
        /// Získá HTML kód stránky, podle indexu
        /// </summary>
        /// <param name="index">index stránky</param>
        /// <returns>HTML kód stránky</returns>
        public string GetPageHtml(int index) => GetPageHtml(this[index]);

        /// <summary>
        /// Získá HTML kód stránky, podle názvu souboru
        /// </summary>
        /// <param name="filename">název souboru stránky</param>
        /// <returns>HTML kód stránky</returns>
        public string GetPageHtml(string filename) => GetPageHtml(Find(page => page.Filename == filename));

        /// <summary>
        /// Zda obsahuje stránku s daným názvem souboru
        /// </summary>
        /// <param name="filename">Název souboru</param>
        /// <returns>True, pokud takovou stránku obsahuje</returns>
        public bool HasPage(string filename) => this.Any(page => page.Filename == filename);

        /// <summary>
        /// Uloží všechny své stránky do složky
        /// </summary>
        /// <param name="folder">Složka, kam stránky uložit</param>
        public void Save(string folder)
        {
            this.ForEach(webPage => webPage.Save(folder, this));
        }

        /// <summary>
        /// Nastaví hlavní stránku
        /// </summary>
        /// <param name="index">Index stránky, kterou nastavit jako hlavné</param>
        public void SetMainPage(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            ForEach(page => page.IsMain = false);
            this[index].IsMain = true;
        }

        private string GetPageHtml(WebPage webPage) => webPage.GenerateHtmlDocument(this).ToString();
    }
}
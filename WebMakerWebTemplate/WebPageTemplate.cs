using System.Collections.Generic;
using System.Linq;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    /// <summary>
    /// Třída reprezentující šablonu pro WebPage
    /// </summary>
    public class WebPageTemplate
    {
        /// <summary>
        /// Vytvoří novou instanci WebPageTemplate
        /// </summary>
        /// <param name="title">Titulek</param>
        /// <param name="isMain">Zda je stránka hlavní</param>
        /// <param name="webElementTemplates">Kolekce šablon pro webové elementy</param>
        public WebPageTemplate(string title, bool isMain, List<WebElementTemplate> webElementTemplates)
        {
            Title = title;
            IsMain = isMain;
            WebElementTemplates = webElementTemplates;
        }

        /// <summary>
        /// Zda je stránka hlavní
        /// </summary>
        public bool IsMain { get; set; }
        /// <summary>
        /// Titulek
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Kolekce šablon pro webové elementy
        /// </summary>
        public List<WebElementTemplate> WebElementTemplates { get; set; }

        /// <summary>
        /// Vytvoří nový WebPageTemplate z WebPage
        /// </summary>
        /// <param name="webPage">WebPage</param>
        /// <returns>Nový WebPage template</returns>
        public static WebPageTemplate FromWebPage(WebPage webPage)
        {
            var webElementTemplates = webPage.Select(element => WebElementTemplate.FromWebElement(element)).ToList();
            return new WebPageTemplate(webPage.Title, webPage.IsMain, webElementTemplates);
        }

        /// <summary>
        /// Vytvoří z šablony prázdnou WebPage
        /// </summary>
        /// <returns>Prazdnou WebPage</returns>
        public WebPage ToBlankWebPage()
        {
            var webPage = new WebPage(Title);
            webPage.AddRange(WebElementTemplates.Select(webElementTemplate => webElementTemplate.ToBlankWebElement()));
            return webPage;
        }
    }
}
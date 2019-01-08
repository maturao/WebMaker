using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    /// <summary>
    /// Třída reprezentující šablonu pro třídu WebSite
    /// </summary>
    public class WebSiteTemplate
    {
        /// <summary>
        /// Vytvoří novou instanci třídy WebSiteTemplate
        /// </summary>
        /// <param name="webPageTemplates">Kolekce šablon pro WebPage</param>
        public WebSiteTemplate(List<WebPageTemplate> webPageTemplates)
        {
            WebPageTemplates = webPageTemplates;
        }

        /// <summary>
        /// Kolekce šablon pro WebPage
        /// </summary>
        public List<WebPageTemplate> WebPageTemplates { get; set; }

        /// <summary>
        /// Vytvoří WebSiteTemplate z jsonu
        /// </summary>
        /// <param name="json">Řeztězec obsahující json</param>
        /// <returns>Nový WebSiteTemplate</returns>
        public static WebSiteTemplate Deserialize(string json) => JsonConvert.DeserializeObject<WebSiteTemplate>(json);

        /// <summary>
        /// Vytvoří nový WebSite template z WebSite
        /// </summary>
        /// <param name="webSite">WebSite</param>
        /// <returns>Nový WebSiteTemplate</returns>
        public static WebSiteTemplate FromWebSite(WebSite webSite)
        {
            return new WebSiteTemplate(webSite.Select(webPage => WebPageTemplate.FromWebPage(webPage)).ToList());
        }

        /// <summary>
        /// Převede template na json
        /// </summary>
        /// <returns>Řeztězec obsahující json</returns>
        public string Serialize() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// Vytvoří z šablony prázdnou WebSite
        /// </summary>
        /// <returns>Prázdnou WebSite</returns>
        public WebSite ToBlankWebSite()
        {
            var webSite = new WebSite();
            webSite.AddRange(WebPageTemplates.Select(webPageTemplate => webPageTemplate.ToBlankWebPage()));
            webSite.SetMainPage(WebPageTemplates.FindIndex(webPageTemplate => webPageTemplate.IsMain));
            return webSite;
        }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    public class WebSiteTemplate
    {
        public WebSiteTemplate(List<WebPageTemplate> webPageTemplates)
        {
            WebPageTemplates = webPageTemplates;
        }

        public List<WebPageTemplate> WebPageTemplates { get; set; }

        public static WebSiteTemplate Deserialize(string json) => JsonConvert.DeserializeObject<WebSiteTemplate>(json);

        public static WebSiteTemplate FromWebSite(WebSite webSite)
        {
            return new WebSiteTemplate(webSite.Select(webPage => WebPageTemplate.FromWebPage(webPage)).ToList());
        }

        public string Serialize() => JsonConvert.SerializeObject(this);

        public WebSite ToBlankWebSite()
        {
            var webSite = new WebSite();
            webSite.AddRange(WebPageTemplates.Select(webPageTemplate => webPageTemplate.ToBlankWebPage()));
            webSite.SetMainPage(WebPageTemplates.FindIndex(webPageTemplate => webPageTemplate.IsMain));
            return webSite;
        }
    }
}
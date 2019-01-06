using System.Collections.Generic;
using System.Linq;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    public class WebPageTemplate
    {
        public WebPageTemplate(string title, bool isMain, List<WebElementTemplate> webElementTemplates)
        {
            Title = title;
            IsMain = isMain;
            WebElementTemplates = webElementTemplates;
        }

        public bool IsMain { get; set; }
        public string Title { get; set; }
        public List<WebElementTemplate> WebElementTemplates { get; set; }

        public static WebPageTemplate FromWebPage(WebPage webPage)
        {
            var webElementTemplates = webPage.Select(element => WebElementTemplate.FromWebElement(element)).ToList();
            return new WebPageTemplate(webPage.Title, webPage.IsMain, webElementTemplates);
        }

        public WebPage ToBlankWebPage()
        {
            var webPage = new WebPage(Title);
            webPage.AddRange(WebElementTemplates.Select(webElementTemplate => webElementTemplate.ToBlankWebElement()));
            return webPage;
        }
    }
}
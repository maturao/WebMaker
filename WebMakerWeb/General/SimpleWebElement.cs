using WebMaker.Html.General;

namespace WebMaker.Web.General
{
    internal abstract class SimpleWebElement<T> : WebElement<string> where T : OpenElement, new()
    {
        internal string HTML
        {
            get
            {
                var element = new T
                {
                    InnerText = Content
                };
                return element.ToString();
            }
        }
    }
}
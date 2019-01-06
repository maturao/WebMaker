using WebMaker.Html.Elements;
using WebMaker.Html.General;
using WebMaker.Web.General;

namespace WebMaker.Web.Elements
{
    /// <summary>
    /// Třída reprezentující webový obrázek
    /// </summary>
    public class WebImage : WebElement<string>
    {
        internal override Element HTMLElement => new ImageElement() { Src = Content };
    }
}
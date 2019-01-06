using WebMaker.Html.Elements;
using WebMaker.Html.General;
using WebMaker.Web.General;

namespace WebMaker.Web.Elements
{
    /// <summary>
    /// Třída reprezentující webový odstavec
    /// </summary>
    public class WebParagraph : WebElement<string>
    {
        private PElement pElement = new PElement();

        /// <summary>
        /// Text v odstavci
        /// </summary>
        public override string Content
        {
            get => pElement.InnerText;
            set => pElement.InnerText = value;
        }

        internal override Element HTMLElement => pElement;
    }
}
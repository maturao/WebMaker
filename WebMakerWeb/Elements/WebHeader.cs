using WebMaker.Html.Elements;
using WebMaker.Html.General;
using WebMaker.Web.General;

namespace WebMaker.Web.Elements
{
    /// <summary>
    /// Třída reprezentující webový nadpis
    /// </summary>
    public class WebHeader : WebElement<string>
    {
        private HElement hElement;

        /// <summary>
        /// Vytvoří instanci třídy WebHeader
        /// </summary>
        /// <param name="level">Level nadpisu</param>
        public WebHeader(int level) => hElement = new HElement(level);

        /// <summary>
        /// Vytvoří instanci třídy WebHeader
        /// </summary>
        /// <param name="level">Level nadpisu</param>
        public WebHeader(long level) : this((int)level)
        {
        }

        /// <summary>
        /// Obsah nadpisu
        /// </summary>
        public override string Content
        {
            get => hElement.InnerText;
            set => hElement.InnerText = value;
        }

        /// <summary>
        /// Level nadpisu
        /// </summary>
        public int Level
        {
            get => hElement.Level;
            set => hElement.Level = value;
        }

        internal override Element HTMLElement => hElement;
    }
}
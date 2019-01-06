using WebMaker.Html.Elements;
using WebMaker.Html.Style;

namespace WebMaker.Web.General
{
    /// <summary>
    /// Webový styl
    /// </summary>
    public class WebStyle
    {
        /// <summary>
        /// Vytvoří instanci třídý WebStyle
        /// </summary>
        protected WebStyle()
        {
        }

        /// <summary>
        /// Vytvoří instanci třídý WebStyle
        /// </summary>
        /// <param name="styleSheet">Styl</param>
        protected WebStyle(StyleSheet styleSheet)
        {
            StyleSheet = styleSheet;
        }

        internal StyleElement StyleElement => new StyleElement(StyleSheet);

        /// <summary>
        /// Styl
        /// </summary>
        protected StyleSheet StyleSheet { get; set; }

        /// <summary>
        /// Spojí dva webové styly dohromady
        /// </summary>
        /// <param name="a">první webový styl</param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static WebStyle operator +(WebStyle a, WebStyle b)
        {
            var styleSheet = new StyleSheet();
            styleSheet.AddRange(a.StyleSheet);
            styleSheet.AddRange(b.StyleSheet);
            return new WebStyle(styleSheet);
        }

        /// <summary>
        /// Převede webový styl na řetězec
        /// </summary>
        /// <returns>Webový styl v HTML notaci</returns>
        public override string ToString() => StyleSheet.ToString();
    }
}
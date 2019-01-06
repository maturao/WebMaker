using WebMaker.Html.General;
using WebMaker.Html.Style;

namespace WebMaker.Html.Elements
{
    /// <summary>
    /// Třída reprezentující HTML "style" element
    /// </summary>
    public class StyleElement : OpenElement
    {
        /// <summary>
        /// Vytvoří instanci třídy StyleElement
        /// </summary>
        /// <param name="styleSheet">Styly uvnitř elementu</param>
        public StyleElement(StyleSheet styleSheet)
        {
            StyleSheet = styleSheet;
        }

        /// <summary>
        /// Styly uvnitř elementu
        /// </summary>
        public StyleSheet StyleSheet { get; set; }

        /// <summary>
        /// Převede element na řetězec
        /// </summary>
        /// <returns>Element v HTML notaci</returns>
        public override string ToString()
        {
            return OpenTag + ((StyleSheet is null) ? string.Empty : StyleSheet.ToString()) + CloseTag;
        }
    }
}
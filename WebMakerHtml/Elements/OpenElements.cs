using WebMaker.Html.General;

namespace WebMaker.Html.Elements
{
    /// <summary>
    /// Třída reprezentující HTML "a" element
    /// </summary>
    public class AElement : OpenElement
    {
        /// <summary>
        /// Href atribut
        /// </summary>
        [HtmlAttribute]
        public string Href { get; set; }
    }

    /// <summary>
    /// Třída reprezentující HTML "body" element
    /// </summary>
    public class BodyElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "div" element
    /// </summary>
    public class DivElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "head" element
    /// </summary>
    public class HeadElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "html" element
    /// </summary>
    public class HtmlElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "li" element
    /// </summary>
    public class LiElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "main" element
    /// </summary>
    public class MainElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "nav" element
    /// </summary>
    public class NavElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "ol" element
    /// </summary>
    public class OlElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "p" element
    /// </summary>
    public class PElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "span" element
    /// </summary>
    public class SpanElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "title" element
    /// </summary>
    public class TitleElement : OpenElement { }

    /// <summary>
    /// Třída reprezentující HTML "ul" element
    /// </summary>
    public class UlElement : OpenElement { }
}
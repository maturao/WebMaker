using WebMaker.Html.General;

namespace WebMaker.Html.Elements
{
    /// <summary>
    /// Třída reprezentující HTML "br" element
    /// </summary>
    public class BrElement : ClosedElement { }

    /// <summary>
    /// Třída reprezentující HTML "image" element
    /// </summary>
    public class ImageElement : ClosedElement
    {
        /// <summary>
        /// Src atribut
        /// </summary>
        [HtmlAttribute]
        public string Src { get; set; }
    }

    /// <summary>
    /// Třída reprezentující HTML "link" element
    /// </summary>
    public class LinkElement : ClosedElement
    {
        /// <summary>
        /// Href atribut
        /// </summary>
        [HtmlAttribute]
        public string Href { get; set; }

        /// <summary>
        /// Rel atribut
        /// </summary>
        [HtmlAttribute]
        public string Rel { get; set; }
    }

    /// <summary>
    /// Třída reprezentující HTML "Meta" element
    /// </summary>
    public class MetaElement : ClosedElement
    {
        /// <summary>
        /// Charset atribut
        /// </summary>
        [HtmlAttribute]
        public string Charset { get; set; }

        /// <summary>
        /// Content atribut
        /// </summary>
        [HtmlAttribute]
        public string Content { get; set; }

        /// <summary>
        /// Name atribut
        /// </summary>
        [HtmlAttribute]
        public string Name { get; set; }
    }
}
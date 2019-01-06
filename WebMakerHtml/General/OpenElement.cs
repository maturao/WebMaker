using System.Collections;
using System.Collections.Generic;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Obecná třída, ze které dědí všechny element s otevřeným tagem
    /// </summary>
    public abstract class OpenElement : Element, IEnumerable<Element>
    {
        private const string closeTagFormat = "</{0}>";
        private const string openTagFormat = "<{0}>";
        private readonly HtmlCollection _children = new HtmlCollection();

        /// <summary>
        /// Obsažené elementy
        /// </summary>
        public override HtmlCollection Children => _children;

        /// <summary>
        /// Uzavírací tag
        /// </summary>
        public string CloseTag => string.Format(closeTagFormat, TagName);
        /// <summary>
        /// Zda obsahuje elementy
        /// </summary>
        public override bool HasChildren => Children.Count > 0;
        /// <summary>
        /// Otevírací tag
        /// </summary>
        public string OpenTag => string.Format(openTagFormat, TagName + Attributes);

        /// <summary>
        /// Přidá element do tohoto elementu
        /// </summary>
        /// <param name="element">Element k přidání</param>
        public void Add(Element element) => Children.Add(element);

        /// <summary>
        /// Vrací enumarator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator GetEnumerator() => Children.GetEnumerator();

        /// <summary>
        /// Převede element na řetězec
        /// </summary>
        /// <returns>Element v HTML notaci</returns>
        public override string ToString() => OpenTag + InnerText + Children + CloseTag;

        IEnumerator<Element> IEnumerable<Element>.GetEnumerator() => Children.GetEnumerator();
    }
}
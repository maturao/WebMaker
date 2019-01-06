using System.Collections.Generic;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Třída reprezentující kolekci elementů
    /// </summary>
    public class HtmlCollection : List<Element>
    {
        /// <summary>
        /// Vytvoří instanci třídy HtmlCollection
        /// </summary>
        public HtmlCollection()
        {
        }

        /// <summary>
        /// Vytvoří instanci třídy HtmlCollection
        /// </summary>
        /// <param name="elements">kolekce elementů</param>
        public HtmlCollection(IEnumerable<Element> elements) => AddRange(elements);

        /// <summary>
        /// Převede kolekci elementů na řetězec
        /// </summary>
        /// <returns>Elementy v HTML notaci</returns>
        public override string ToString()
        {
            return string.Join(string.Empty, this);
        }
    }
}
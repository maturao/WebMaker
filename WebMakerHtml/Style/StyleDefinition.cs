using System.Collections.Generic;
using System.Linq;

namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující definici stylu
    /// </summary>
    public class StyleDefinition : List<StyleAttribute>
    {

        private const string definitionContainer = "{0} {{ {1} }}";

        /// <summary>
        /// Vytvoří instanci třídy StyleDefinition
        /// </summary>
        /// <param name="target">Cíl definice stylu</param>
        public StyleDefinition(string target)
        {
            Target = target;
        }

        /// <summary>
        /// Cíl definice stylu
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Převede definici na řetězec
        /// </summary>
        /// <returns>Definice v CSS notaci</returns>
        public override string ToString()
        {
            return string.Format(definitionContainer, Target, this.Aggregate(string.Empty, (result, attribute) => result + attribute.ToString()));
        }
    }
}
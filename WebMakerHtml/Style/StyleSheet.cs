using System.Collections.Generic;
using System.Linq;

namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující kolekci definicí stylů
    /// </summary>
    public class StyleSheet : List<StyleDefinition>
    {
        /// <summary>
        /// Převede stylesheet na řetezec
        /// </summary>
        /// <returns>Stylesheet v CSS notaci</returns>
        public override string ToString()
        {
            return this.Aggregate(string.Empty, (result, current) => result + current);
        }
    }
}
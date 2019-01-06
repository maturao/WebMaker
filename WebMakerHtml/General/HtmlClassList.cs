using System.Collections.Generic;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Třída reprezentující kolekci class v HTML atributu "class"
    /// </summary>
    public class HtmlClassList : List<string>
    {
        private const char classSeparator = ' ';

        /// <summary>
        /// Nastaví kolekci pomocí řetezce
        /// </summary>
        /// <param name="class">Řetězec, ve kterém jsou jednotlivé classy oddělené mezerou</param>
        public void Set(string @class)
        {
            if (!string.IsNullOrWhiteSpace(@class))
            {
                Clear();
                AddRange(@class.Split(classSeparator));
            }
        }

        /// <summary>
        /// Převede kolekci class na řetězec
        /// </summary>
        /// <returns>Classy v řetězci oddělené mezerou</returns>
        public override string ToString()
        {
            return string.Join(classSeparator.ToString(), this);
        }
    }
}
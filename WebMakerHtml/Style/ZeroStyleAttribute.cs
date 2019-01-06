namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující atribut s nulovou hodnotou
    /// </summary>
    public class ZeroStyleAttribute : StyleAttribute
    {
        private const string zero = "0";

        /// <summary>
        /// Vytvoří instanci třídy ZeroStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        public ZeroStyleAttribute(string name) : base(name)
        {
        }

        /// <summary>
        /// Hodnota atributu
        /// </summary>
        public override string Value => zero;
    }
}
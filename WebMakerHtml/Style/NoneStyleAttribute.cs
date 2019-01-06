namespace WebMaker.Html.Style
{
    /// <summary>
    /// třída reprezentující atribut s hodnotou "none" 
    /// </summary>
    public class NoneStyleAttribute : StyleAttribute
    {
        private const string none = "none";

        /// <summary>
        /// Vytvoří instanci třídy NoneStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        public NoneStyleAttribute(string name) : base(name)
        {
        }

        /// <summary>
        /// Hodnota atributu
        /// </summary>
        public override string Value => none;
    }
}
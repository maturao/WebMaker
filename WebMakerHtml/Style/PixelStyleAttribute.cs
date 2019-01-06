namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující jednotokvý atribut s jednotkou pixel
    /// </summary>
    public class PixelStyleAttribute : UnitStyleAttribute
    {
        /// <summary>
        /// Vytvoří instanci třídy PixelStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        /// <param name="numberValue">Hodnota</param>
        public PixelStyleAttribute(string name, int numberValue) : base(name, numberValue, AttributeUnit.Px)
        {
        }
    }
}
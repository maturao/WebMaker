namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující css atribut s číselnou hodnotou a jednotkou
    /// </summary>
    public class UnitStyleAttribute : StyleAttribute
    {
        /// <summary>
        /// Vytvoří instanci třídy UnitStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        /// <param name="numberValue">Číselná hodnota</param>
        /// <param name="unit">Jednotka</param>
        public UnitStyleAttribute(string name, int numberValue, AttributeUnit unit) : base(name)
        {
            NumberValue = numberValue;
            Unit = unit;
        }

        /// <summary>
        /// Číselná hodnota
        /// </summary>
        public int NumberValue { get; set; }
        /// <summary>
        /// Jednotka
        /// </summary>
        public AttributeUnit Unit { get; set; }

        /// <summary>
        /// Hodnota atributu
        /// </summary>
        public override string Value => NumberValue + Unit.ToString().ToLowerInvariant();
    }
}
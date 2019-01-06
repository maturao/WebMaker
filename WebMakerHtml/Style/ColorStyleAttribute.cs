namespace WebMaker.Html.Style
{
    /// <summary>
    /// Atribut, obsahující barvu jako hodnotu
    /// </summary>
    public class ColorStyleAttribute : StyleAttribute
    {
        /// <summary>
        /// Vytvoří instanci třídy ColorStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        /// <param name="color">Barva</param>
        public ColorStyleAttribute(string name, StyleColor color) : base(name)
        {
            Color = color;
        }

        /// <summary>
        /// Barevná hodnota
        /// </summary>
        public StyleColor Color { get; set; }

        /// <summary>
        /// Hodnota atributus
        /// </summary>
        public override string Value => Color.ToString();
    }
}
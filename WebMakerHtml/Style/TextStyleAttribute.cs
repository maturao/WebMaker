namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující css atribut, který obsahuje text jako hodnotu
    /// </summary>
    public class TextStyleAttribute : StyleAttribute
    {
        /// <summary>
        /// Vytvoří instanci třídy TextStyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        /// <param name="text">Textová hodnota</param>
        public TextStyleAttribute(string name, string text) : base(name)
        {
            Text = text;
        }

        /// <summary>
        /// Textová hodnota
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Hodnota atributu
        /// </summary>
        public override string Value => Text;
    }
}
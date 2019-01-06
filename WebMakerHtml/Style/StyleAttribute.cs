namespace WebMaker.Html.Style
{
    /// <summary>
    /// Obecná třida reprezentující jeden css stylový atribut
    /// </summary>
    public abstract class StyleAttribute
    {
        private const string atrributeSeparator = ";";
        private const string nameValueSeparator = ": ";

        /// <summary>
        /// Vytvoří instanci třídy StyleAttribute
        /// </summary>
        /// <param name="name">Název atributu</param>
        protected StyleAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Název atributu
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// Hodnota atributu
        /// </summary>
        public abstract string Value { get; }

        /// <summary>
        /// Převede atribut na řetězec
        /// </summary>
        /// <returns>Atribut v css notaci</returns>
        public override string ToString()
        {
            return Name + nameValueSeparator + Value + atrributeSeparator;
        }
    }
}
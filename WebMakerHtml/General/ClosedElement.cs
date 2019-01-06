namespace WebMaker.Html.General
{
    /// <summary>
    /// Obecná třída, ze které dědí všechny jednotagové HTML elementy
    /// </summary>
    public abstract class ClosedElement : Element
    {
        /// <summary>
        /// Tvar html tagu
        /// </summary>
        private const string tagFormat = "<{0}/>";

        /// <summary>
        /// Vždy vráti prázdnou kolekci, protože uzevřený element nemůže obsahovat žadné elementy
        /// </summary>
        public override HtmlCollection Children => new HtmlCollection();

        /// <summary>
        /// Zda obsahuje nějaké elementy
        /// </summary>
        public override bool HasChildren => false;

        /// <summary>
        /// Html tag
        /// </summary>
        public string Tag => string.Format(tagFormat, TagName + Attributes);

        /// <summary>
        /// Převede element na řetězec
        /// </summary>
        /// <returns>Element v HTML notaci</returns>
        public override string ToString() => Tag;
    }
}
namespace WebMaker.Html.General
{
    /// <summary>
    /// Obecná třída, ze které dědí každá třída reprezentující HTML element
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Kolekce HTML atributů, vytvořená z propert obsahyjící atrubut HtmlAttributeAttribute
        /// </summary>
        public HtmlAttributes Attributes
        {
            get => new HtmlAttributes(this);
            set => value.SetAttributes(this);
        }

        /// <summary>
        /// Obsažené html elementy
        /// </summary>
        public abstract HtmlCollection Children { get; }

        /// <summary>
        /// Reprezentuje HTML class atribut
        /// </summary>
        [HtmlAttribute]
        public string Class
        {
            get => ClassList.ToString();
            set => ClassList.Set(value);
        }

        /// <summary>
        /// Kolekce všesch css class z class atributu
        /// </summary>
        public HtmlClassList ClassList { get; } = new HtmlClassList();

        /// <summary>
        /// Zda obsahuje nějaké elementy
        /// </summary>
        public abstract bool HasChildren { get; }

        /// <summary>
        /// Reprezentuje HTML id atribut
        /// </summary>
        [HtmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Text uvnitř elementu
        /// </summary>
        public virtual string InnerText { get; set; }

        /// <summary>
        /// Reprezentuje HTML style atribut
        /// </summary>
        [HtmlAttribute]
        public string Style { get; set; }

        /// <summary>
        /// Název elementu použitý uvnitř HTML tagu
        /// </summary>
        public virtual string TagName
        {
            get
            {
                var name = GetType().Name;
                return name.Remove(name.Length - nameof(Element).Length).ToLowerInvariant();
            }
        }

        /// <summary>
        /// Převede element do formy řetězce
        /// </summary>
        /// <returns>Element v HTML notaci</returns>
        public abstract override string ToString();
    }
}
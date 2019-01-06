using System;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Atribut pro označení property, které reprezentují HTML atributy
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class HtmlAttributeAttribute : Attribute
    {
        /// <summary>
        /// Vlastní název HTML atributu (jako výchozí se použije náaev property)
        /// </summary>
        public string CustomName { get; set; }
    }
}
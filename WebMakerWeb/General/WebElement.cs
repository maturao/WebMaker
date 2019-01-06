using WebMaker.Html.General;

namespace WebMaker.Web.General
{
    /// <summary>
    /// Obecná třída pro webový element s obsahem 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WebElement<T> : WebElement
    {
        /// <summary>
        /// Obsah webového elementu
        /// </summary>
        public virtual T Content { get; set; } 
    }

    /// <summary>
    /// Abstraktní třída reprezentující Webový element (část webové stránky, např. odstavec)
    /// </summary>
    public abstract class WebElement
    {
        /// <summary>
        /// Název typu webového elementu
        /// </summary>
        public string Name => GetType().Name.Replace("Web", string.Empty);

        internal abstract Element HTMLElement { get; }
    }
}
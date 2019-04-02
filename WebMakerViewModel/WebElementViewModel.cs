using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel třídy WebElement s obsahem
    /// </summary>
    /// <typeparam name="T">Datový typ obsahu</typeparam>
    public abstract class WebElementViewModel<T> : WebElementViewModel
    {
        private T _content;

        /// <summary>
        /// Obsah
        /// </summary>
        public T Content
        {
            get => _content;
            set
            {
                if (value != null)
                {
                    if (!value.Equals(_content))
                    {
                        _content = value;
                        RaiseNotifyChanged();
                    }
                }
            }
        }
    }

    /// <summary>
    /// ViewModel třídy WebElement
    /// </summary>
    public abstract class WebElementViewModel : BaseViewModel
    {
        /// <summary>
        /// ViewModel převedený na WebElement
        /// </summary>
        public abstract WebElement WebElement { get; }

        public abstract string WebElementName { get; }

        /// <summary>
        /// Vytvoří WebElementViewModel z WebElementu
        /// </summary>
        /// <param name="webElement">WebElement</param>
        /// <returns>WebElementViewModel</returns>
        public static WebElementViewModel FromWebElement(WebElement webElement)
        {
            if (webElement is WebParagraph)
            {
                var el = webElement as WebParagraph;
                return new WebParagraphViewModel()
                {
                    Content = el.Content
                };
            }
            else if (webElement is WebHeader)
            {
                var el = webElement as WebHeader;
                return new WebHeaderViewModel()
                {
                    Content = el.Content,
                    Level = el.Level
                };
            }
            else if (webElement is WebList)
            {
                var el = webElement as WebList;
                return new WebListViewModel()
                {
                    Content = string.Join("\n", el.Content),
                    IsOrdered = el.IsOrdered
                };
            }
            else if (webElement is WebImage)
            {
                var el = webElement as WebImage;
                return new WebImageViewModel()
                {
                    Content = el.Content
                };
            }
            return null;
        }
    }
}
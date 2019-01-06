using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    public abstract class WebElementViewModel<T> : WebElementViewModel
    {
        private T _content;

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

    public abstract class WebElementViewModel : BaseViewModel
    {
        public abstract WebElement WebElement { get; }

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

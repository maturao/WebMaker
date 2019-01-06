using System.Collections.Generic;
using System.Linq;
using WebMaker.Html.Elements;
using WebMaker.Html.General;
using WebMaker.Web.General;

namespace WebMaker.Web.Elements
{
    /// <summary>
    /// Třída reprezentující webový list
    /// </summary>
    public class WebList : WebElement<List<string>>
    {
        /// <summary>
        /// Vytvoří instanci třídy WebList
        /// </summary>
        /// <param name="isOrdered">Zda je list očíslovaný</param>
        public WebList(bool isOrdered)
        {
            Content = new List<string>();
            IsOrdered = isOrdered;
        }

        /// <summary>
        /// Zda je list očíslovaný
        /// </summary>
        public bool IsOrdered { get; set; }

        internal override Element HTMLElement
        {
            get
            {
                OpenElement listElement;
                if (IsOrdered)
                {
                    listElement = new OlElement();
                }
                else
                {
                    listElement = new UlElement();
                }
                listElement.Children.AddRange(Content.Select(item => new LiElement() { InnerText = item }));
                return listElement;
            }
        }
    }
}
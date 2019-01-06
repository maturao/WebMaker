using WebMaker.Html.Elements;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Třída reprezentující celý HTML dokument
    /// </summary>
    public class HtmlDocument
    {
        private const string doctype = "<!DOCTYPE html>";

        private readonly HtmlElement htmlElement;
        private readonly TitleElement titleElement;

        /// <summary>
        /// Vytvoří instanci třídy HTMLDocument
        /// </summary>
        /// <param name="title">Titulek dokumentu</param>
        public HtmlDocument(string title)
        {
            titleElement = new TitleElement()
            {
                InnerText = title
            };
            Head = new HeadElement()
            {
                titleElement
            };

            Body = new BodyElement();

            htmlElement = new HtmlElement()
            {
                Head,
                Body
            };
        }

        /// <summary>
        /// Vytvoří instanci třídy HTMLDocument
        /// </summary>
        /// <param name="title">Titulek dokumentu</param>
        /// <param name="content">Kolekce elementů, které se použijí jako obsah</param>
        public HtmlDocument(string title, HtmlCollection content) : this(title) => Content = content;

        /// <summary>
        /// Vytvoří instanci třídy HTMLDocument
        /// </summary>
        /// <param name="title">Titulek dokumentu</param>
        /// <param name="bodyElements">Elementy, které budou obsažené v tělě dokumentu</param>
        public HtmlDocument(string title, params Element[] bodyElements) : this(title)
        {
            Body.Children.AddRange(bodyElements);
        }

        /// <summary>
        /// Tělo dokumentu
        /// </summary>
        public BodyElement Body { get; }

        /// <summary>
        /// Elementy obsažené v těle dokumentu
        /// </summary>
        public HtmlCollection Content
        {
            get => Body.Children;
            set
            {
                if (!(value is null))
                {
                    Body.Children.Clear();
                    Body.Children.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Hlava dokumentu
        /// </summary>
        public HeadElement Head { get; }

        /// <summary>
        /// Titulek dokuemtu
        /// </summary>
        public string Title
        {
            get => titleElement.InnerText;
            set => titleElement.InnerText = value;
        }

        /// <summary>
        /// Převede dokument na řetězec
        /// </summary>
        /// <returns>Dokument v HTML notaci</returns>
        public override string ToString()
        {
            return doctype + htmlElement;
        }
    }
}
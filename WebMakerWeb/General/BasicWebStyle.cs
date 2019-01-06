using WebMaker.Html.Style;

namespace WebMaker.Web.General
{
    /// <summary>
    /// Základní webový styl
    /// </summary>
    public class BasicWebStyle : WebStyle
    {
        private static readonly StyleColor basicColor = new StyleColor(104, 154, 248);
        private static readonly StyleColor darkGray = new StyleColor(202);
        private static readonly StyleColor lightGray = new StyleColor(231);

        /// <summary>
        /// Vytvoří instanci třídy BasicWebStyle
        /// </summary>
        /// <param name="color">Barva stylu</param>
        public BasicWebStyle(StyleColor color)
        {
            const int navBorderWidth = 10;
            const int navAPadding = 10;
            const int navAMinWidth = 175;
            const int transition = 100;
            const int mainMinWidth = 500;
            const int mainMaxWidth = 1000;
            const int mainMargin = 20;
            const int marginBottom = 20;

            StyleSheet = new StyleSheet()
            {
                new StyleDefinition("*")
                {
                    new ZeroStyleAttribute("margin"),
                    new ZeroStyleAttribute("padding")
                },
                new StyleDefinition("body")
                {
                    new ColorStyleAttribute("background-color", lightGray),
                    new TextStyleAttribute("font-family", "Arial, Helvetica, sans-serif"),
                },
                new StyleDefinition("nav")
                {
                    new ColorStyleAttribute("background-color", lightGray),
                    new TextStyleAttribute("padding", "auto"),

                    new PixelStyleAttribute("border-width", navBorderWidth),
                    new TextStyleAttribute("border-style", "solid"),
                    new ColorStyleAttribute("border-color", color),
                    new NoneStyleAttribute("border-left-style"),
                    new NoneStyleAttribute("border-right-style"),
                    new NoneStyleAttribute("border-top-style")
                },
                new StyleDefinition("nav a")
                {
                    new ColorStyleAttribute("background-color", darkGray),
                    new ColorStyleAttribute("color", StyleColor.Black),

                    new TextStyleAttribute("display", "inline-block"),

                    new PixelStyleAttribute("padding-top", navAPadding),
                    new PixelStyleAttribute("padding-bottom", navAPadding),
                    new PixelStyleAttribute("min-width", navAMinWidth),

                    new TextStyleAttribute("text-align", "center"),
                    new NoneStyleAttribute("text-decoration")
                },
                new StyleDefinition("nav a:visited")
                {
                    new TextStyleAttribute("color", "inherit")
                },
                new StyleDefinition("nav a:hover")
                {
                    new ColorStyleAttribute("background-color", color + darkGray),
                    new UnitStyleAttribute("transition", transition, AttributeUnit.Ms)
                },
                new StyleDefinition("nav a.selected, nav a:active")
                {
                    new ColorStyleAttribute("background-color", color)
                },
                new StyleDefinition("nav a.selected")
                {
                    new TextStyleAttribute("font-weight", "bold")
                },
                new StyleDefinition("main")
                {
                    new PixelStyleAttribute("min-width", mainMinWidth),
                    new PixelStyleAttribute("max-width", mainMaxWidth),
                    new PixelStyleAttribute("margin", mainMargin),
                },
                new StyleDefinition("h1, h2, h3, h4, h5, h6, p, img, ol, ul")
                {
                    new PixelStyleAttribute("margin-bottom", marginBottom)
                },
                new StyleDefinition("ol, ul")
                {
                    new TextStyleAttribute("list-style-position", "inside")
                },
            };
        }

        /// <summary>
        /// Vytvoří instanci třídy BasicWebStyle
        /// </summary>
        public BasicWebStyle() : this(basicColor)
        {
        }
    }
}
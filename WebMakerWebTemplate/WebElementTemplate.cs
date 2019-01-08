using System;
using System.Linq;
using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    /// <summary>
    /// Třída reprezentující šablonu pro třídu WebElement
    /// </summary>
    public class WebElementTemplate
    {
        /// <summary>
        /// Vytvoří instanci třídy WebElementTemplate
        /// </summary>
        /// <param name="assemblyQualifiedTypeName">AssemblyQualifiedTypeName WebElementu</param>
        /// <param name="parameters">Parametry konstruktoru WebElementu</param>
        public WebElementTemplate(string assemblyQualifiedTypeName, object[] parameters)
        {
            AssemblyQualifiedTypeName = assemblyQualifiedTypeName;
            Parameters = parameters;
        }

        /// <summary>
        /// AssemblyQualifiedTypeName WebElementu
        /// </summary>
        public string AssemblyQualifiedTypeName { get; set; }
        /// <summary>
        /// Parametry konstruktoru WebElementu 
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// Vytvoří  WebElementTemplate z WebElementu
        /// </summary>
        /// <param name="webElement">WebElement</param>
        /// <returns>Novoun instanci WebElementTemplate</returns>
        public static WebElementTemplate FromWebElement(WebElement webElement)
        {
            object[] parameters;
            if (webElement is WebHeader)
            {
                parameters = new object[] { (webElement as WebHeader).Level };
            }
            else if (webElement is WebList)
            {
                parameters = new object[] { (webElement as WebList).IsOrdered };
            }
            else
            {
                parameters = new object[0];
            }
            return new WebElementTemplate(webElement.GetType().AssemblyQualifiedName, parameters);
        }

        /// <summary>
        /// Vytvoří prázdný WebElement
        /// </summary>
        /// <returns>Nový prázdný WebElement</returns>
        public WebElement ToBlankWebElement()
        {
            var type = Type.GetType(AssemblyQualifiedTypeName);
            var types = Parameters.Select(parameter => parameter.GetType()).ToArray();
            var constructor = type.GetConstructor(types);
            var webElement = constructor.Invoke(Parameters);
            return webElement as WebElement;
        }
    }
}
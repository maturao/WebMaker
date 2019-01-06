using System;
using System.Linq;
using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.Template
{
    public class WebElementTemplate
    {
        public WebElementTemplate(string assemblyQualifiedTypeName, object[] parameters)
        {
            AssemblyQualifiedTypeName = assemblyQualifiedTypeName;
            Parameters = parameters;
        }

        public string AssemblyQualifiedTypeName { get; set; }
        public object[] Parameters { get; set; }

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
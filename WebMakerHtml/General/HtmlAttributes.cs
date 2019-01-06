using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMaker.Html.General
{
    /// <summary>
    /// Reprezentuje kolekci atributů HTML elementu
    /// </summary>
    public class HtmlAttributes : Dictionary<string, string>
    {
        private const string attributeFormat = @" {0}=""{1}""";

        /// <summary>
        /// Vytvoří instanci HtmlAtributes třídy
        /// </summary>
        public HtmlAttributes()
        {
        }

        /// <summary>
        /// Vytvoří instanci HtmlAtributes třídy
        /// </summary>
        /// <param name="element">Element, ze kterého extrahovat atributy</param>
        public HtmlAttributes(Element element)
        {
            foreach (var attributeProperty in element.GetType().GetProperties())
            {
                var htmlAttributeAtrribute = attributeProperty.GetCustomAttributes(typeof(HtmlAttributeAttribute), false).FirstOrDefault() as HtmlAttributeAttribute;
                if (!(htmlAttributeAtrribute is null))
                {
                    var value = attributeProperty.GetValue(element);
                    if (!string.IsNullOrWhiteSpace((string)value))
                    {
                        var name = htmlAttributeAtrribute.CustomName;
                        if (name is null)
                        {
                            name = attributeProperty.Name.ToLowerInvariant();
                        }
                        Add(name, value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Nastaví atributy pomocí elementu
        /// </summary>
        /// <param name="element">Element, ze kterého extrahovat atributy</param>
        public void SetAttributes(Element element)
        {
            foreach (var attributeProperty in element.GetType().GetProperties())
            {
                var htmlAttributeAtrribute = attributeProperty.GetCustomAttributes(typeof(HtmlAttributeAttribute), false).FirstOrDefault() as HtmlAttributeAttribute;
                if (!(htmlAttributeAtrribute is null))
                {
                    var name = htmlAttributeAtrribute.CustomName;
                    if (name is null)
                    {
                        name = attributeProperty.Name.ToLowerInvariant();
                    }
                    if (ContainsKey(name))
                    {
                        attributeProperty.SetValue(element, this[name]);
                    }
                }
            }
        }

        /// <summary>
        /// Převede kolekci atributu na řetězec
        /// </summary>
        /// <returns>Atributy v HTML notaci</returns>
        public override string ToString()
        {
            var @string = new StringBuilder();
            foreach (var attribute in this)
            {
                @string.Append(string.Format(attributeFormat, attribute.Key, attribute.Value));
            }
            return @string.ToString();
        }
    }
}
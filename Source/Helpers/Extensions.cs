using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{
    static class Extensions
    {
       
        public static string GetElementValueTrimmed(this XElement parent, string elementName)
        {
            string output = (string)parent.Element(elementName);
            return (output == null) ? null : output.Trim();
        }

        /// <summary>
        /// Get child element value or null if element missing.
        /// </summary>        
        public static string GetElementValueOrNull(this XElement parent, string elementName)
        {
            XElement existingElement = parent.Element(elementName);
            return (existingElement != null) ? existingElement.Value : null;
        }

        public static bool GetElementValueBool(this XElement parent, string elementName, bool defaultValue = false)
        {
            string strValue = parent.GetElementValueOrNull(elementName);
            bool output;
            return (bool.TryParse(strValue, out output)) ? output : defaultValue;
        }

        public static int GetElementValueInt(this XElement parent, string elementName, int defaultValue = 0)
        {
            string strValue = parent.GetElementValueOrNull(elementName);
            int output;
            return (int.TryParse(strValue, out output)) ? output : defaultValue;
        }

    }
}

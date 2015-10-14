using System.Xml.Linq;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetElementValue(this XElement element, string key)
        {
            if (element == null)
                return string.Empty;

            var elem = element.Element(key);

            return elem?.Value ?? string.Empty;
        }
         
    }
}

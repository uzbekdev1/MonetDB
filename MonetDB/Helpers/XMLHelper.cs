using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonetDB.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static XDocument GetDocument(string path)
        {
            return XDocument.Parse(File.ReadAllText(path, Encoding.UTF8), LoadOptions.None);
        }
    }
}

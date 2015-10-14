using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Connector.Data.Enums;
using MonetDB.Helpers;

namespace MonetDB.Connector.Data.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataConnectorFactoryHelper
    {
        private static IDataConnector GetDataFactory(string source)
        { 
            var dllPath = Path.Combine(IoHelper.CurrentRoot, $"MonetDB.Connector.{source}.dll"); 
            var assembly = Assembly.LoadFrom(dllPath);
            var type = assembly.DefinedTypes.FirstOrDefault(f => string.Equals(f.Name, $"{source}Connector", StringComparison.InvariantCultureIgnoreCase));

            return type == null
                ? Activator.CreateInstance<IDataConnector>()
                : (IDataConnector)Activator.CreateInstance(type);
        }

        /// <summary>
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static IDataConnector GetDataFactory(DataSourceType sourceType)
        {
            var dataSourceName = Enum.GetName(typeof(DataSourceType), sourceType);

            return GetDataFactory(dataSourceName);
        }
    }
}

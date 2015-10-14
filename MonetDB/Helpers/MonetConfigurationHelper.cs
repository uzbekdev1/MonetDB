using MonetDB.Configurations;
using MonetDB.Extensions;

namespace MonetDB.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonetConfigurationHelper
    {
        /// <summary> 
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static MonetConfiguration GetConfiguration(string filePath)
        {
            var doc = XmlHelper.GetDocument(filePath);
            var root = doc.Element("DbConfiguration");
            var model = root == null
                ? new MonetConfiguration()
                : new MonetConfiguration
                {
                    DbFarmDir = EnvironmentHelper.ConfigureBuildPath(root.GetElementValue("DbFarmDir")),
                    DbInstallerDir = EnvironmentHelper.ConfigureBuildPath(root.GetElementValue("DbInstallerDir")),
                    LogDir = EnvironmentHelper.ConfigureBuildPath(root.GetElementValue("LogDir")),
                    TempDir = EnvironmentHelper.ConfigureBuildPath(root.GetElementValue("TempDir"))
                };

            return model;
        }
    }
}

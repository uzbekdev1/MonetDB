using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnvironmentHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetVariables(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConfigureBuildPath(string path)
        {
            if (path.IndexOf("${BUILD}", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                path = path.Replace("${BUILD}", IoHelper.CurrentRoot);
            }

            if (path.IndexOf("${PLATFORM}", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                path = path.Replace("${PLATFORM}", IoHelper.PlatformName);
            }

            if (Path.HasExtension(path))
            {
                var dir = Path.GetDirectoryName(path);

                if (dir == null || !Directory.Exists(dir))
                    throw new DirectoryNotFoundException("dir");
            }
            else
            {
                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException("path");
            }

            return path;
        }

    }
}

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using MonetDB.Extensions;

namespace MonetDB.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class IoHelper
    {

        /// <summary>
        /// x86 or x64
        /// </summary>
        public static readonly string PlatformName = Environment.Is64BitProcess ? "x64" : "x86";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string CurrentRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// 
        /// </summary>
        public static string PlatformFolder => Path.Combine(CurrentRoot, PlatformName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTmpRandomFileName(string tempPath)
        {
            return Path.Combine(tempPath, Path.GetRandomFileName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveFile(string path)
        {
            if (!File.Exists(path))
                return;

            File.Delete(path);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(string path)
        {
            return !File.ReadAllText(path).IsNull();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            // Add file sizes.
            var fis = d.GetFiles();
            var size = fis.Sum(fi => fi.Length);

            // Add subdirectory sizes.
            var dis = d.GetDirectories();

            size += dis.Sum(di => DirSize(di));

            return size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern int StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);

        /// <summary>
        /// Return a file size created by the StrFormatByteSize API function.
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string ToFileSize(long fileSize)
        {
            var sb = new StringBuilder(20);

            StrFormatByteSize(fileSize, sb, 20);

            return sb.ToString();
        }

    }

}

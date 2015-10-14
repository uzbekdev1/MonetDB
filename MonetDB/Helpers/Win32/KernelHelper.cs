using System;
using System.Runtime.InteropServices;

namespace MonetDB.Helpers.Win32
{
    /// <summary>
    /// 
    /// </summary>
    public static class KernelHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void SetDirectory(string path)
        {
            CKernelLib.SetDllDirectory(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static IntPtr LoadLibrary(string path)
        {
            return CKernelLib.LoadLibrary(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        public static void FreeLibrary(IntPtr ptr)
        {
            CKernelLib.FreeLibrary(ptr);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AttachConsole()
        {
            CKernelLib.AllocConsole();
        }

        private class CKernelLib
        {
            private const string Kernel32 = "kernel32.dll";

            #region Environment

            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern bool SetDllDirectory(string lpPathName);

            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

            [DllImport(Kernel32, SetLastError = true, CharSet = CharSet.Ansi)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            #endregion

            #region Console

            [DllImport(Kernel32)]
            internal static extern bool AllocConsole();

            #endregion
        }
    }
}

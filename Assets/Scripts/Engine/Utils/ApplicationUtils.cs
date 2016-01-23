using System;

namespace Argamente.Utils
{
    public class ApplicationUtils
    {

        public static string GetProcessDirectory ()
        {
            return new System.IO.FileInfo (System.Diagnostics.Process.GetCurrentProcess ().MainModule.FileName).DirectoryName;
        }
    }
}


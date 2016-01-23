using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Argamente.Utils
{
    public class FileUtils
    {

        public static void CreateParentDirectory (string dirPath)
        {
            string parentDirPath = new FileInfo (dirPath).DirectoryName;
            if (!Directory.Exists (parentDirPath))
            {
                Directory.CreateDirectory (parentDirPath);
            }
        }


        public static void AppendTextToFile (string text, string filePath)
        {
            try
            {
                CreateParentDirectory (filePath);
                using (FileStream fs = new FileStream (filePath, FileMode.Append, FileAccess.Write))
                {
                    byte[] data = new System.Text.UTF8Encoding ().GetBytes (text);
                    fs.Write (data, 0, data.Length);
                    fs.Flush ();
                    fs.Close ();
                }
            }
            catch (Exception e)
            {
                Debug.LogError ("AppendTextToFile Faild: " + text + "    " + e);
            }
        }
    }
}


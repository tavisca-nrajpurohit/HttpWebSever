using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Text;

namespace HTTPWebServer_Final
{
    class FileSystem
    {
        internal static byte[] GetFileinBytes(string filePath)
        {

            /*Stream input = new FileStream(filePath, FileMode.Open);
            byte[] buffer = new byte[1024 * 32];
            int nbytes;
            while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                // Do Nothing
            }
            input.Close();
            return buffer;*/

            /*TextReader tr = new StreamReader(filePath);
            string msg = tr.ReadToEnd();  //getting the page's content
            // converting to Bytes
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            return buffer;*/
            FileInfo fileInfo = new FileInfo(filePath);
            FileStream fileStream = fileInfo.OpenRead();
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer,0,buffer.Length);
            return buffer;
        }
    }
}

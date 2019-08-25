using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Text;

namespace HTTPWebServer_Final
{
    
    class HTTPHandler : IWebHandler
    {
        // Mime type Dictionary
        private static IDictionary<string, string> _mimeTypeMapping =
                new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
                {
                {".asf", "video/x-ms-asf"},
                {".asx", "video/x-ms-asf"},
                {".avi", "video/x-msvideo"},
                {".bin", "application/octet-stream"},
                {".cco", "application/x-cocoa"},
                {".crt", "application/x-x509-ca-cert"},
                {".css", "text/css"},
                {".deb", "application/octet-stream"},
                {".der", "application/x-x509-ca-cert"},
                {".dll", "application/octet-stream"},
                {".dmg", "application/octet-stream"},
                {".ear", "application/java-archive"},
                {".eot", "application/octet-stream"},
                {".exe", "application/octet-stream"},
                {".flv", "video/x-flv"},
                {".gif", "image/gif"},
                {".hqx", "application/mac-binhex40"},
                {".htc", "text/x-component"},
                {".htm", "text/html"},
                {".html", "text/html"},
                {".ico", "image/x-icon"},
                {".img", "application/octet-stream"},
                {".iso", "application/octet-stream"},
                {".jar", "application/java-archive"},
                {".jardiff", "application/x-java-archive-diff"},
                {".jng", "image/x-jng"},
                {".jnlp", "application/x-java-jnlp-file"},
                {".jpeg", "image/jpeg"},
                {".jpg", "image/jpeg"},
                {".js", "application/x-javascript"},
                {".mml", "text/mathml"},
                {".mng", "video/x-mng"},
                {".mov", "video/quicktime"},
                {".mp3", "audio/mpeg"},
                {".mpeg", "video/mpeg"},
                {".mpg", "video/mpeg"},
                {".msi", "application/octet-stream"},
                {".msm", "application/octet-stream"},
                {".msp", "application/octet-stream"},
                {".pdb", "application/x-pilot"},
                {".pdf", "application/pdf"},
                {".pem", "application/x-x509-ca-cert"},
                {".pl", "application/x-perl"},
                {".pm", "application/x-perl"},
                {".png", "image/png"},
                {".prc", "application/x-pilot"},
                {".ra", "audio/x-realaudio"},
                {".rar", "application/x-rar-compressed"},
                {".rpm", "application/x-redhat-package-manager"},
                {".rss", "text/xml"},
                {".run", "application/x-makeself"},
                {".sea", "application/x-sea"},
                {".shtml", "text/html"},
                {".sit", "application/x-stuffit"},
                {".swf", "application/x-shockwave-flash"},
                {".tcl", "application/x-tcl"},
                {".tk", "application/x-tcl"},
                {".txt", "text/plain"},
                {".war", "application/java-archive"},
                {".wbmp", "image/vnd.wap.wbmp"},
                {".wmv", "video/x-ms-wmv"},
                {".xml", "text/xml"},
                {".xpi", "application/x-xpinstall"},
                {".zip", "application/zip"}
                };
        public string RootLocation { get; set; }

        public HTTPHandler(string input)
        {
            RootLocation = input;
        }


        public void Handle(HttpListenerContext context)
        {
            if (RootLocation == "")
            {
                FileNotFoundResponse(context);
            }
            else
            {
                // Accessing and Reading File ...
                String filePath = RootLocation + context.Request.Url.LocalPath;
                
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Requested file was not found : " + filePath);
                    FileNotFoundResponse(context);
                }
                else
                {
                    byte[] dataBuffer = FileSystem.GetFileinBytes(filePath);

                    HttpListenerResponse response = context.Response;

                    string mimeType;
                    context.Response.ContentType = _mimeTypeMapping.TryGetValue(Path.GetExtension(filePath), out mimeType)
                        ? mimeType
                        : "text/html";

                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    response.ContentLength64 = dataBuffer.Length;  // set up the messasge's length
                    Stream st = response.OutputStream;  // here we create a stream to send the message
                    st.Write(dataBuffer, 0, dataBuffer.Length); // and this will send all the content to the browser
                    Console.WriteLine("Requested file("+mimeType+") was SENT to Client : " + filePath);
                    context.Response.OutputStream.Flush();
                    //context.Response.Close();
                }
                
            }
            

        }

        private void FileNotFoundResponse(HttpListenerContext context)
        {
            String filePath = @"C:\Users\nrajpurohit\Desktop\a\404.html";
            byte[] dataBuffer = FileSystem.GetFileinBytes(filePath);

            HttpListenerResponse response = context.Response;

            string mimeType;
            context.Response.ContentType = _mimeTypeMapping.TryGetValue(Path.GetExtension(filePath), out mimeType)
                ? mimeType
                : "text/html";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            response.ContentLength64 = dataBuffer.Length;  // set up the messasge's length
            Stream st = response.OutputStream;  // here we create a stream to send the message
            st.Write(dataBuffer, 0, dataBuffer.Length); // and this will send all the content to the browser
            Console.WriteLine("Requested file(" + mimeType + ") was SENT to Client : " + filePath);
            context.Response.OutputStream.Flush();
            
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Text;

namespace HTTPWebServer_Final
{
    class ApiHandler : IWebHandler
    {
        public string RootLocation { get; set; }

        public void Handle(HttpListenerContext context)
        {
            string path = context.Request.Url.LocalPath;
            path = path.Replace("/api/", "");
            Console.WriteLine("Api requested for method - "+ path);
            string[] strlist = path.Split("/");
            string output;

            switch(strlist[0])
            {
                case "year":
                case "Year":
                    output = APIMethods.Year(strlist[1]);
                    break;
                case "Hello":
                case "hello":
                    output = APIMethods.Hello(strlist[1]);
                    break;
                default: output = "wrong API method call";
                    break;
            }

            byte[] dataBuffer = Encoding.UTF8.GetBytes(output);
            HttpListenerResponse response = context.Response;

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            response.ContentLength64 = dataBuffer.Length;  // set up the messasge's length
            Stream st = response.OutputStream;  // here we create a stream to send the message
            st.Write(dataBuffer, 0, dataBuffer.Length); // and this will send all the content to the browser
            Console.WriteLine("API method- "+strlist[0]+" was called!");
            context.Response.OutputStream.Flush();
            //context.Response.Close();
        }


    }
}



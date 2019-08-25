using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Text;

namespace HTTPWebServer_Final
{
    class Dispatcher
    {
        internal IWebHandler GetWebHandler(HttpListenerContext context)
        {
            ServerConfiguration serverConfig = new ServerConfiguration();
            // getting root location from server configuration and initializing handler
            /* <<<<<<<<   TIP : For many handlers we can use here a HANDLER FACTORY !!! >>>>>>>> */
            IWebHandler handler;

            if(context.Request.Url.LocalPath.Contains("/api/"))
            {
                handler = new ApiHandler();
            }
            else
            {
                handler = new HTTPHandler(serverConfig.GetRootLocation(context.Request.Url.Host));
            }

            return handler;
        }
    }
}

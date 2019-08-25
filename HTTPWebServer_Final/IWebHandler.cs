using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HTTPWebServer_Final
{
    interface IWebHandler
    {
        String RootLocation { get; set; }
        void Handle(HttpListenerContext context);
    }
}



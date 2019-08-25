using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPWebServer_Final
{
    class ServerConfiguration
    {
        internal string GetRootLocation(string host)
        {
            switch (host)
            {
                case "localhost":
                    return @"C:\Users\nrajpurohit\Desktop\a";
                    break;
                default:
                    return "";
                    break;
            }
        }
        
    }
}

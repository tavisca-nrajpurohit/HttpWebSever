using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Text;

namespace HTTPWebServer_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            WebListener webListener = new WebListener();
            webListener.Start();
            while (true)
            {
                var userInput = Console.ReadLine();
                if (String.Equals(userInput,"stop")|| String.Equals(userInput, "exit"))
                {
                    webListener.Stop();
                }
            }
        }
    }
}

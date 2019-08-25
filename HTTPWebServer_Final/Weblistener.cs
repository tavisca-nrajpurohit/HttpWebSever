using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace HTTPWebServer_Final
{
    class WebListener
    {
        HttpListener _httpListener;

        internal void Start()
        {
            Thread _serverThread = new Thread(new ThreadStart(()=>Listen()));
            _serverThread.Start();
        }

        void Listen()
        {
            // Starting the Listener !!!!
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add("http://127.0.0.1:8091/");
            _httpListener.Prefixes.Add("http://localhost:8091/");
            _httpListener.Start();
            Console.WriteLine("Server Started at : http://localhost:8091/");

            // Processing Request...
            while (_httpListener.IsListening)
            {
                // Get a Connection Context...
                HttpListenerContext context = _httpListener.GetContext();
                // Get a Dispatcher on Work...
                Dispatcher dispatcher = new Dispatcher();
                // Get the right Webhandler for the Request...
                IWebHandler webHandler = dispatcher.GetWebHandler(context);
                // Generate response and send it to network stream...
                webHandler.Handle(context);
            }            
        }

        internal void Stop()
        {
            _httpListener.Stop();
            Console.WriteLine("Server Stopped !");
        }
    }
}

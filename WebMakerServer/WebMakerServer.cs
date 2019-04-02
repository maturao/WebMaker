using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WebMaker.Server
{
    /// <summary>
    /// Třída poskytující jednoduchý webový server
    /// </summary>
    public class WebMakerServer : IDisposable
    {
        /// <summary>
        /// Výchozí port
        /// </summary>
        public const int DefaultPort = 80;

        private const string httpPrefixFormat = "http://{0}:{1}/";

        private readonly HttpListener httpListener;
        private Thread thread;

        private IPAddress _iPAddress;
        private int _port = DefaultPort;

        public bool IsRunning => httpListener.IsListening;


        /// <summary>
        /// Vytvoří instanci třídy WebMakerServer
        /// </summary>
        public WebMakerServer(bool localOnly = true)
        {
            httpListener = new HttpListener();

            if (!localOnly)
            {
                IPAddress = LocalIPAddress();
            }
            else
            {
                UpdatePrefixes();
            }
            SetUpThread();
        }

        private void SetUpThread()
        {
            thread = new Thread(Server);
        }

        private void Server()
        {
            while (IsRunning)
            {
                try
                {
                    var ctx = httpListener.GetContext();

                    var response = ctx.Response;

                    string responseString;
                    var requestUrl = ctx.Request.RawUrl.TrimStart('/', '\\');
                    if (string.IsNullOrEmpty(requestUrl))
                    {
                        responseString = WebSiteProvider.WebSite.GetMainPageHtml();
                    }
                    else if (requestUrl.Contains(".html"))
                    {
                        responseString = GetPage(requestUrl);
                    }
                    else
                    {
                        response.StatusCode = 404;
                        response.Close();
                        continue;
                    }

                    var buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.LongLength;
                    using (var output = response.OutputStream)
                    {
                        output.Write(buffer, 0, buffer.Length);
                    }
                } catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Destruktor, který vypne webový server
        /// </summary>
        ~WebMakerServer() => Stop();

        /// <summary>
        /// IP adresa
        /// </summary>
        public IPAddress IPAddress
        {
            get => _iPAddress;
            set
            {
                _iPAddress = value;
                UpdatePrefixes();
            }
        }

        /// <summary>
        /// Číslo portu
        /// </summary>
        public int Port
        {
            get => _port;
            set
            {
                if (value != _port)
                {
                    _port = value;
                    UpdatePrefixes();
                }
            }
        }

        /// <summary>
        /// Poskytovatel webu
        /// </summary>
        public IWebSiteProvider WebSiteProvider { get; set; }

        private string[] Prefixes
        {
            get
            {
                var prefixes = new List<string>
                {
                    HttpPrefix(IPAddress.Loopback, Port)
                };
                if (!(IPAddress is null))
                {
                    prefixes.Add(HttpPrefix(IPAddress, Port));
                }
                return prefixes.ToArray();
            }
        }

        /// <summary>
        /// Spustí webový server
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                SetUpThread();
                httpListener.Start();
                thread.Start();
            }
        }

        /// <summary>
        /// Vypne webový server
        /// </summary>
        public void Stop()
        {
            if(IsRunning)
            {
                httpListener.Stop();
                thread.Join();
            }
        }

        private static string HttpPrefix(string address, int port) => string.Format(httpPrefixFormat, address, port);

        private static string HttpPrefix(IPAddress address, int port) => HttpPrefix(address.ToString(), port);

        private static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private string GetPage(string pageFilename)
        {
            if (WebSiteProvider is null)
            {
                return "No website here";
            }

            var webSite = WebSiteProvider.WebSite;
            if (webSite.HasPage(pageFilename))
            {
                return webSite.GetPageHtml(pageFilename);
            }
            else
            {
                return "Sorry, we dont have this one :(";
            }
        }

        private void UpdatePrefixes()
        {
            httpListener.Prefixes.Clear();
            foreach (var prefix in Prefixes)
            {
                httpListener.Prefixes.Add(prefix);
            }
        }

        public void Dispose() => Stop();
    }
}
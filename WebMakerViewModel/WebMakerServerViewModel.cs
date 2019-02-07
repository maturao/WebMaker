using System.Windows.Input;
using WebMaker.Server;
using System.Linq;
using System;
using System.Windows.Forms;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel pro WebMakerServer
    /// </summary>
    public class WebMakerServerViewModel : BaseViewModel
    {
        private const string startServerText = "Start local server";
        private const string stopServerText = "Stop local server";

        private readonly WebMakerServer webMakerServer = new WebMakerServer();
        private string _iPAddress;
        private bool _isRunning = false;

        /// <summary>
        /// Vytvoří novou instanci třídy WebMakerServerViewModel
        /// </summary>
        public WebMakerServerViewModel()
        {
            StartStopServerCommand = new RelayCommand(StartStopServer);
        }

        /// <summary>
        /// Text na tlačítku pro spouštění/vypnutí servru
        /// </summary>
        public string ButtonText => IsRunning ? stopServerText : startServerText;

        public bool CanEditAdress
        {
            get
            {
                return !IsRunning;
            }
        }

        public string IPAddress
        {
            get => _iPAddress;
            set
            {
                if (value != _iPAddress)
                {
                    _iPAddress = value;
                    RaiseNotifyChanged();
                }
            }
        }

        /// <summary>
        /// Zda server běží
        /// </summary>
        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                if (value != _isRunning)
                {
                    _isRunning = value;
                    RaiseNotifyChanged();
                    RaiseNotifyChanged(nameof(CanEditAdress));
                }
            }
        }

        /// <summary>
        /// Command pro spuštění/vypnutí serveru
        /// </summary>
        public ICommand StartStopServerCommand { get; }

        internal IWebSiteProvider WebSiteProvider
        {
            get => webMakerServer.WebSiteProvider;
            set => webMakerServer.WebSiteProvider = value;
        }

        /// <summary>
        /// Spustí/vypne server
        /// </summary>
        public void StartStopServer()
        {
            try
            {
                if (IsRunning)
                {
                    webMakerServer.Stop();
                    IsRunning = false;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(IPAddress))
                    {
                        webMakerServer.IPAddress = new System.Net.IPAddress(IPAddress.Split('.').Select(octet => byte.Parse(octet)).ToArray());
                    }
                    webMakerServer.Start();
                    IsRunning = true;
                }
                RaiseNotifyChanged(nameof(ButtonText));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
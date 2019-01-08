using System.Windows.Input;
using WebMaker.Server;

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
            if (IsRunning)
            {
                webMakerServer.Stop();
                IsRunning = false;
            }
            else
            {
                webMakerServer.Start();
                IsRunning = true;
            }
            RaiseNotifyChanged(nameof(ButtonText));
        }
    }
}
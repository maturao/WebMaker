using System.Windows.Input;
using WebMaker.Server;

namespace WebMaker.ViewModel
{
    public class WebMakerServerViewModel : BaseViewModel
    {
        private const string startServerText = "Start local server";
        private const string stopServerText = "Stop local server";

        private readonly WebMakerServer webMakerServer = new WebMakerServer();
        private bool _isRunning = false;

        public WebMakerServerViewModel()
        {
            StartStopServerCommand = new RelayCommand(StartStopServer);
        }

        public string ButtonText => IsRunning ? stopServerText : startServerText;

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

        public ICommand StartStopServerCommand { get; }

        internal IWebSiteProvider WebSiteProvider
        {
            get => webMakerServer.WebSiteProvider;
            set => webMakerServer.WebSiteProvider = value;
        }

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
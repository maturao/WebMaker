using System.Windows.Input;
using WebMaker.Server;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Net;

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


        public WebMakerServerViewModel ()
        {
            SetIPAddressCommand = new RelayCommand(SetIPAddress);
        }

        ~WebMakerServerViewModel() => webMakerServer.Dispose();

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

        private IPAddress GetIPAddress()
        {
            if (string.IsNullOrWhiteSpace(IPAddress))
            {
                return null;
            }
            return new System.Net.IPAddress(IPAddress.Split('.').Select(octet => byte.Parse(octet)).ToArray());
        }

        /// <summary>
        /// Zda server běží
        /// </summary>
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (value != _isRunning)
                {
                    try
                    {
                        if (IsRunning)
                        {
                            webMakerServer.Stop();
                        }
                        else
                        {
                            webMakerServer.IPAddress = GetIPAddress();
                            webMakerServer.Start();
                        }
                        _isRunning = value;
                        RaiseNotifyChanged();
                        RaiseNotifyChanged(nameof(CanEditAdress));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
        internal IWebSiteProvider WebSiteProvider
        {
            get => webMakerServer.WebSiteProvider;
            set => webMakerServer.WebSiteProvider = value;
        }

        public ICommand SetIPAddressCommand { get; }

        private void SetIPAddress()
        {
            IPAddress = Microsoft.VisualBasic.Interaction.InputBox("Enter IP Address");
        }

        /// <summary>
        /// Spustí/vypne server
        /// </summary>
        //public void StartStopServer()
        //{
        //    try
        //    {
        //        if (IsRunning)
        //        {
        //            webMakerServer.Stop();
        //            IsRunning = false;
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrWhiteSpace(IPAddress))
        //            {
        //                webMakerServer.IPAddress = new System.Net.IPAddress(IPAddress.Split('.').Select(octet => byte.Parse(octet)).ToArray());
        //            }
        //            webMakerServer.Start();
        //            IsRunning = true;
        //        }
        //        RaiseNotifyChanged(nameof(ButtonText));
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using WebMaker.Template;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel pro hlavní okno
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private WebSiteViewModel _webSiteViewModel;

        /// <summary>
        /// Vytvoří novou instancí třídy MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            WebSiteViewModel = new WebSiteViewModel();
            WebSiteViewModel.AddPage();
            WebSiteViewModel.SelectedWebPageViewModel = WebSiteViewModel.WebPageViewModels[0];
            WebSiteViewModel.SetMainPage(WebSiteViewModel.SelectedWebPageViewModel);

            WebMakerServerViewModel = new WebMakerServerViewModel();
            WebMakerServerViewModel.WebSiteProvider = WebSiteViewModel;

            SaveTemplateCommand = new RelayCommand(SaveTemplate);
            LoadTemplateCommand = new RelayCommand(LoadTemplate);
        }

        /// <summary>
        /// Command pro načtění WebSiteViewModel z šablony
        /// </summary>
        public ICommand LoadTemplateCommand { get; }
        /// <summary>
        /// Command pro uložení WebSiteViewModel jako šablony
        /// </summary>
        public ICommand SaveTemplateCommand { get; }

        /// <summary>
        /// ViewModel pro WebMakerServer
        /// </summary>
        public WebMakerServerViewModel WebMakerServerViewModel { get; }

        /// <summary>
        /// ViewModel pro WebSite
        /// </summary>
        public WebSiteViewModel WebSiteViewModel
        {
            get => _webSiteViewModel;
            set
            {
                if (value != _webSiteViewModel)
                {
                    _webSiteViewModel = value;
                    RaiseNotifyChanged();
                }
            }
        }

        /// <summary>
        /// Načte WebSiteViewModel z šablony
        /// </summary>
        public void LoadTemplate()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Json file (*.json)|*.json";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var json = File.ReadAllText(dialog.FileName);
                    var template = WebSiteTemplate.Deserialize(json);
                    WebSiteViewModel = WebSiteViewModel.FromWebSite(template.ToBlankWebSite());
                    WebMakerServerViewModel.WebSiteProvider = WebSiteViewModel;
                }
            }
        }

        /// <summary>
        /// Uloží WebSiteViewModel jako šablonu
        /// </summary>
        public void SaveTemplate()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Json file (*.json)|*.json";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    File.WriteAllText(dialog.FileName, WebSiteTemplate.FromWebSite(WebSiteViewModel.WebSite).Serialize());
                }
            }
        }
    }
}
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using WebMaker.Server;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    public class WebSiteViewModel : BaseViewModel, IWebSiteProvider
    {
        private const string newPageName = "newPage";
        private WebPageViewModel _selectedWebPageViewModel;
        private ObservableCollection<WebPageViewModel> _webPageViewModels = new ObservableCollection<WebPageViewModel>();

        public WebSiteViewModel()
        {
            AddPageCommand = new RelayCommand(AddPage);
            DeletePageCommand = new RelayCommand(DeletePage);
            SaveCommand = new RelayCommand(Save);
            SetMainPageCommand = new RelayCommand(SetMainPage);
        }

        public ICommand AddPageCommand { get; }
        public ICommand DeletePageCommand { get; }
        public ICommand SaveCommand { get; }

        public WebPageViewModel SelectedWebPageViewModel
        {
            get => _selectedWebPageViewModel;
            set
            {
                if (value != _selectedWebPageViewModel)
                {
                    _selectedWebPageViewModel = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public ICommand SetMainPageCommand { get; }

        public ObservableCollection<WebPageViewModel> WebPageViewModels
        {
            get => _webPageViewModels;
            set
            {
                if (value != _webPageViewModels)
                {
                    _webPageViewModels = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public WebSite WebSite
        {
            get
            {
                var webSite = new WebSite();
                webSite.AddRange(WebPageViewModels.Select(webPageViewModel => webPageViewModel.WebPage));
                webSite.SetMainPage(WebPageViewModels.IndexOf(WebPageViewModels.First(webPageViewModel => webPageViewModel.IsMain)));
                return webSite;
            }
        }

        public void AddPage()
        {
            if (ContainsPage(newPageName))
            {
                int newPageNumber = 1;
                while (ContainsPage(newPageName + newPageNumber))
                {
                    newPageNumber++;
                }
                AddPage(newPageName + newPageNumber);
            }
            else
            {
                AddPage(newPageName);
            }
        }

        public void DeletePage(object webPageViewModel)
        {
            if (WebPageViewModels.Count > 1)
            {
                var webPage = (webPageViewModel as WebPageViewModel);
                WebPageViewModels.Remove(webPage);
                if (webPage.IsMain)
                {
                    WebPageViewModels[0].IsMain = true;
                }
            }
        }

        public void Save()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WebSite.Save(dialog.SelectedPath);
                }
            }
        }

        public void SetMainPage(object webPageViewModel)
        {
            var page = webPageViewModel as WebPageViewModel;
            if (!(page is null) && WebPageViewModels.Contains(page))
            {
                foreach (var webPage in WebPageViewModels)
                {
                    webPage.IsMain = false;
                }
                page.IsMain = true;
            }
        }

        private void AddPage(string pageTitle) => WebPageViewModels.Add(new WebPageViewModel() { Title = pageTitle });

        private bool ContainsPage(string pageTitle) => WebPageViewModels.Any(webPageViewModel => webPageViewModel.Title == pageTitle);

        public static WebSiteViewModel FromWebSite(WebSite webSite)
        {
            var webSiteViewModel = new WebSiteViewModel()
            {
                WebPageViewModels = new ObservableCollection<WebPageViewModel>(webSite.Select(webPage => WebPageViewModel.FromWebPage(webPage)))
            };
            webSiteViewModel.SelectedWebPageViewModel = webSiteViewModel.WebPageViewModels[0];
            return webSiteViewModel;
        }
    }
}
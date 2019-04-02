using CefSharp;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using WebMaker.Server;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel třídy WebSite
    /// </summary>
    public class WebSiteViewModel : BaseViewModel, IWebSiteProvider
    {
        private const string newPageName = "newPage";
        private WebPageViewModel _selectedWebPageViewModel;
        private ObservableCollection<WebPageViewModel> _webPageViewModels = new ObservableCollection<WebPageViewModel>();

        public BasicWebStyleViewModel BasicWebStyleViewModel { get; }

        /// <summary>
        /// Vytvoří novou instanci třídy WebSiteViewModel
        /// </summary>
        public WebSiteViewModel()
        {
            BasicWebStyleViewModel = new BasicWebStyleViewModel();
            AddPageCommand = new RelayCommand(AddPage);
            DeletePageCommand = new RelayCommand(DeletePage);
            SaveCommand = new RelayCommand(Save);
            SetMainPageCommand = new RelayCommand(SetMainPage);
            BrowserRequestHandler = new OnBeforeBrowseHandler(OnBeforeBrowseHandler);
            RefreshPageCommand = new RelayCommand(RefreshPage);
        }

        private bool OnBeforeBrowseHandler(IRequest request, bool userGesture)
        {
            if (userGesture)
            {
                var pagefilename = request.Url.Substring(request.Url.LastIndexOf("/") + 1);
                try
                {
                    return true;
                }
                finally
                {
                    var site = WebSite;
                    var pageIndex = site.FindIndex(page => page.Filename == pagefilename);
                    SelectedWebPageViewModel = WebPageViewModels[pageIndex];
                }
            }
            else
            {
                return false;
            }
        }

        public void RefreshPage() => RaiseNotifyChanged(nameof(SelectedWebPageHtml));

        public ICommand RefreshPageCommand { get; }

        public IRequestHandler BrowserRequestHandler { get; }

        /// <summary>
        /// Command pro přidání nové stránky
        /// </summary>
        public ICommand AddPageCommand { get; }
        /// <summary>
        /// Command pro smazání stránky
        /// </summary>
        public ICommand DeletePageCommand { get; }
        /// <summary>
        /// Command pro uložení do složky
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Zvolený WebPageViewModel
        /// </summary>
        public WebPageViewModel SelectedWebPageViewModel
        {
            get => _selectedWebPageViewModel;
            set
            {
                if (value != _selectedWebPageViewModel)
                {
                    _selectedWebPageViewModel = value;
                    RaiseNotifyChanged();
                    RaiseNotifyChanged(nameof(SelectedWebPageHtml));
                }
            }
        }

        public string SelectedWebPageHtml
        {
            get
            {
                var webSite = WebSite;
                var selectedPageIndex = WebPageViewModels.IndexOf(SelectedWebPageViewModel);
                return WebSite.GetPageHtml(selectedPageIndex);
            }
        }

        /// <summary>
        /// Command pro nastavení hlavní stránky
        /// </summary>
        public ICommand SetMainPageCommand { get; }

        /// <summary>
        /// Obsažené stránky
        /// </summary>
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

        /// <summary>
        /// WebSite
        /// </summary>
        public WebSite WebSite
        {
            get
            {
                var webSite = new WebSite(BasicWebStyleViewModel.BasicWebStyle);
                webSite.AddRange(WebPageViewModels.Select(webPageViewModel => webPageViewModel.WebPage));
                webSite.SetMainPage(WebPageViewModels.IndexOf(WebPageViewModels.First(webPageViewModel => webPageViewModel.IsMain)));
                return webSite;
            }
        }

        /// <summary>
        /// Přidá novou stránku
        /// </summary>
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
        /// <summary>
        /// Smaže stránku
        /// </summary>
        /// <param name="webPageViewModel">Stránka ke smazání</param>
        public void DeletePage(object webPageViewModel)
        {
            if (WebPageViewModels.Count > 1)
            {
                var webPage = (webPageViewModel as WebPageViewModel);
                if (webPage == SelectedWebPageViewModel)
                {
                    var index = WebPageViewModels.IndexOf(webPage);
                    if (index == 0)
                    {
                        SelectedWebPageViewModel = WebPageViewModels[1];
                    }
                    else
                    {
                        SelectedWebPageViewModel = WebPageViewModels[index - 1];
                    }
                }
                WebPageViewModels.Remove(webPage);
                if (webPage.IsMain)
                {
                    WebPageViewModels[0].IsMain = true;
                }
                RaiseNotifyChanged(nameof(SelectedWebPageHtml));
            }
        }
        /// <summary>
        /// Uloží web do složky
        /// </summary>
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
        /// <summary>
        /// Nastaví stránku jako hlavní
        /// </summary>
        /// <param name="webPageViewModel">Stránka, kterou nastavit jako hlavní</param>
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
        /// <summary>
        /// Vyvtvoří ViewModel ze třídy WebSite
        /// </summary>
        /// <param name="webSite">WebSite</param>
        /// <returns>WebSite ViewModel</returns>
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
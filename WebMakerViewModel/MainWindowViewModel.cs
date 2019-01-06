using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using WebMaker.Template;

namespace WebMaker.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private WebSiteViewModel _webSiteViewModel;

//        static MainWindowViewModel()
//        {
//            var webSite = new WebSiteViewModel()
//            {
//                WebPageViewModels = new ObservableCollection<WebPageViewModel>()
//                {
//                    new WebPageViewModel()
//                    {
//                        Title = "Hlavni",
//                        WebElementViewModels = new ObservableCollection<WebElementViewModel>()
//                        {
//                            new WebHeaderViewModel()
//                            {
//                                Level = 2,
//                                Content = "Nadpis"
//                            },
//                            new WebParagraphViewModel()
//                            {
//                                Content = "LoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmet"
//                            },
//                            new WebParagraphViewModel()
//                            {
//                                Content = "LoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmetLoremIpsumDolorSitAmet"
//                            },
//                            new WebListViewModel()
//                            {
//                                IsOrdered = true,
//                                Content = @"first
//second
//third"
//                            },
//                            new WebImageViewModel()
//                            {
//                                Content = "https://i.kym-cdn.com/photos/images/original/001/251/488/5bf.jpg"
//                            }
//                        }
//                    },
//                    new WebPageViewModel()
//                    {
//                        Title = "Jen obrazek",
//                        WebElementViewModels = new ObservableCollection<WebElementViewModel>()
//                        {
//                            new WebImageViewModel()
//                            {
//                                Content = "https://i.kym-cdn.com/photos/images/original/001/251/488/5bf.jpg"
//                            }
//                        }
//                    },
//                    new WebPageViewModel()
//                    {
//                        Title = "Jen text",
//                        WebElementViewModels = new ObservableCollection<WebElementViewModel>()
//                        {
//                            new WebParagraphViewModel()
//                            {
//                                Content = "Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text "
//                            }
//                        }
//                    }
//                }
//            };
//            webSite.SelectedWebPageViewModel = webSite.WebPageViewModels[0];
//            webSite.WebPageViewModels[0].IsMain = true;

//            Instance = new MainWindowViewModel() { WebSiteViewModel = webSite };
//            Instance.WebMakerServerViewModel.WebSiteProvider = Instance.WebSiteViewModel;
//        }

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

        public static MainWindowViewModel Instance { get; }
        public ICommand LoadTemplateCommand { get; }
        public ICommand SaveTemplateCommand { get; }
        public WebMakerServerViewModel WebMakerServerViewModel { get; }

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
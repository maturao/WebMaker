using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    public class WebPageViewModel : BaseViewModel
    {
        private bool _isMain;
        private string _title;
        private ObservableCollection<WebElementViewModel> _webElementViewModels = new ObservableCollection<WebElementViewModel>();

        public WebPageViewModel()
        {
            AddHeaderCommand = new RelayCommand(AddHeader);
            AddImageCommand = new RelayCommand(AddImage);
            AddListCommand = new RelayCommand(AddList);
            AddParagraphCommand = new RelayCommand(AddParagraph);

            MoveItemDownCommand = new RelayCommand(MoveItemDown);
            MoveItemUpCommand = new RelayCommand(MoveItemUp);

            RemoveItemCommand = new RelayCommand(RemoveItem);

            SetAsMainCommand = new RelayCommand(SetAsMain);
        }

        public ICommand AddHeaderCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand AddListCommand { get; }
        public ICommand AddParagraphCommand { get; }

        public bool IsMain
        {
            get => _isMain;
            set
            {
                if (value != _isMain)
                {
                    _isMain = value;
                    RaiseNotifyChanged();
                    RaiseNotifyChanged(nameof(TitleFontWeight));
                }
            }
        }

        public ICommand MoveItemDownCommand { get; }
        public ICommand MoveItemUpCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SetAsMainCommand { get; }

        public string Title
        {
            get => _title;
            set
            {
                if (value != _title && !string.IsNullOrWhiteSpace(value))
                {
                    _title = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public string TitleFontWeight => IsMain ? "Bold" : "Normal";

        public ObservableCollection<WebElementViewModel> WebElementViewModels
        {
            get => _webElementViewModels;
            set
            {
                if (value != _webElementViewModels)
                {
                    _webElementViewModels = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public WebPage WebPage
        {
            get
            {
                var webPage = new WebPage(Title);
                if (WebElementViewModels?.Count > 0)
                {
                    webPage.AddRange(WebElementViewModels.Select(webElementViewModel => webElementViewModel.WebElement));
                }
                return webPage;
            }
        }

        public void AddHeader() => WebElementViewModels.Add(new WebHeaderViewModel() { Level = WebHeaderViewModel.HighestLevel });

        public void AddImage() => WebElementViewModels.Add(new WebImageViewModel());

        public void AddList() => WebElementViewModels.Add(new WebListViewModel() { IsOrdered = false });

        public void AddParagraph() => WebElementViewModels.Add(new WebParagraphViewModel());

        public void MoveItemDown(object webElementViewModel) => MoveElement(webElementViewModel as WebElementViewModel, true);

        public void MoveItemUp(object webElementViewModel) => MoveElement(webElementViewModel as WebElementViewModel, false);

        public void RemoveItem(object webElementViewModel) => WebElementViewModels.Remove(webElementViewModel as WebElementViewModel);

        public void SetAsMain(object webSiteViewModel) => (webSiteViewModel as WebSiteViewModel).SetMainPage(this);

        private void MoveElement(WebElementViewModel webElementViewModel, bool moveDown)
        {
            var newIndex = WebElementViewModels.IndexOf(webElementViewModel);
            if (moveDown)
            {
                newIndex++;
            }
            else
            {
                newIndex--;
            }
            if (newIndex >= 0 && newIndex < WebElementViewModels.Count)
            {
                MoveElement(webElementViewModel, newIndex);
            }
        }

        private void MoveElement(WebElementViewModel webElementViewModel, int index)
        {
            WebElementViewModels.Remove(webElementViewModel);
            WebElementViewModels.Insert(index, webElementViewModel);
        }

        public static WebPageViewModel FromWebPage(WebPage webPage)
        {
            return new WebPageViewModel()
            {
                Title = webPage.Title,
                IsMain = webPage.IsMain,
                WebElementViewModels = new ObservableCollection<WebElementViewModel>(webPage.Select(webElement => WebElementViewModel.FromWebElement(webElement)))
            };
        }
    }
}
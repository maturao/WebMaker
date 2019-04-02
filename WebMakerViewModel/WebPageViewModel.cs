using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel pro WebPage
    /// </summary>
    public class WebPageViewModel : BaseViewModel, IDropTarget
    {
        private bool _isMain;
        private string _title;
        private ObservableCollection<WebElementViewModel> _webElementViewModels = new ObservableCollection<WebElementViewModel>();

        /// <summary>
        /// Vyvoří novou instanci třídy WebPageViewModel
        /// </summary>
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

        /// <summary>
        /// Command pro přídání nového nadpisu
        /// </summary>
        public ICommand AddHeaderCommand { get; }

        /// <summary>
        /// Command pro přídání nového obrázku
        /// </summary>
        public ICommand AddImageCommand { get; }

        /// <summary>
        /// Command pro přídaní nového odrážkového seznamu
        /// </summary>
        public ICommand AddListCommand { get; }

        /// <summary>
        /// Command pro přídání nového odstavce
        /// </summary>
        public ICommand AddParagraphCommand { get; }

        /// <summary>
        /// Zda je stránka hlavní
        /// </summary>
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

        /// <summary>
        /// Comand pro posunutí položky nahoru
        /// </summary>
        public ICommand MoveItemDownCommand { get; }

        /// <summary>
        /// Command pro posunutí položky dolu
        /// </summary>
        public ICommand MoveItemUpCommand { get; }

        /// <summary>
        /// Command pro smazání položky
        /// </summary>
        public ICommand RemoveItemCommand { get; }

        /// <summary>
        /// Command pro nastavní stránky jako hlavní
        /// </summary>
        public ICommand SetAsMainCommand { get; }

        /// <summary>
        /// Titulek
        /// </summary>
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
        
        /// <summary>
        /// Tloušťka fontu pro titulek
        /// </summary>
        public string TitleFontWeight => IsMain ? "Bold" : "Normal";

        /// <summary>
        /// Obsažené ViewModely elemetů
        /// </summary>
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

        /// <summary>
        /// WebPage
        /// </summary>
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

        /// <summary>
        /// Vytvoří ViewModel z WebPage
        /// </summary>
        /// <param name="webPage">WebPage</param>
        /// <returns>WebPage ViewModel</returns>
        public static WebPageViewModel FromWebPage(WebPage webPage)
        {
            return new WebPageViewModel()
            {
                Title = webPage.Title,
                IsMain = webPage.IsMain,
                WebElementViewModels = new ObservableCollection<WebElementViewModel>(webPage.Select(webElement => WebElementViewModel.FromWebElement(webElement)))
            };
        }

        /// <summary>
        /// Přidá nadpis
        /// </summary>
        public void AddHeader() => WebElementViewModels.Add(new WebHeaderViewModel() { Level = WebHeaderViewModel.HighestLevel });

        /// <summary>
        /// Přidá obrázek
        /// </summary>
        public void AddImage() => WebElementViewModels.Add(new WebImageViewModel());

        /// <summary>
        /// Přidá odrážkový seznam
        /// </summary>
        public void AddList() => WebElementViewModels.Add(new WebListViewModel() { IsOrdered = false });

        /// <summary>
        /// Přidá odstavec
        /// </summary>
        public void AddParagraph() => WebElementViewModels.Add(new WebParagraphViewModel());

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is WebElementViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is WebElementViewModel webElementViewModel)
            {
                WebElementViewModels.Remove(webElementViewModel);
                WebElementViewModels.Insert(dropInfo.InsertIndex < WebElementViewModels.Count ? dropInfo.InsertIndex : WebElementViewModels.Count, webElementViewModel);
            }
        }

        /// <summary>
        /// Posune položku dolů
        /// </summary>
        /// <param name="webElementViewModel">Položka k posunutí</param>
        public void MoveItemDown(object webElementViewModel) => MoveElement(webElementViewModel as WebElementViewModel, true);

        /// <summary>
        /// Posune položku nahoru
        /// </summary>
        /// <param name="webElementViewModel">Položka k posunutí</param>
        public void MoveItemUp(object webElementViewModel) => MoveElement(webElementViewModel as WebElementViewModel, false);

        /// <summary>
        /// Odstraní položku
        /// </summary>
        /// <param name="webElementViewModel">Položka k odstanění</param>
        public void RemoveItem(object webElementViewModel) => WebElementViewModels.Remove(webElementViewModel as WebElementViewModel);

        /// <summary>
        /// Nastaví sebe jako hlavní
        /// </summary>
        /// <param name="webSiteViewModel">ViewModel webu</param>
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
    }
}
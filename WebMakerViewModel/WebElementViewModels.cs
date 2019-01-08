using System.Collections.ObjectModel;
using System.Linq;
using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// ViewModel třídy WebHeader
    /// </summary>
    public class WebHeaderViewModel : WebElementViewModel<string>
    {
        /// <summary>
        /// Nejvyšší možný level nadpisu
        /// </summary>
        public const int HighestLevel = 1;
        /// <summary>
        /// Nejnižšý možny level nadpisu
        /// </summary>
        public const int LowestLevel = 6;
        private int _level;
        /// <summary>
        /// Level nadpisu
        /// </summary>
        public int Level
        {
            get
            {
                if (_level < HighestLevel)
                {
                    return HighestLevel;
                }
                else if (_level > LowestLevel)
                {
                    return LowestLevel;
                }
                else
                {
                    return _level;
                }
            }
            set
            {
                if (value != _level)
                {
                    _level = value;
                    RaiseNotifyChanged();
                }
            }
        }
        /// <summary>
        /// WebElement
        /// </summary>
        public override WebElement WebElement => new WebHeader(Level) { Content = Content };
    }
    /// <summary>
    /// ViewModel pro WebImage
    /// </summary>
    public class WebImageViewModel : WebElementViewModel<string>
    {
        /// <summary>
        /// WebElement
        /// </summary>
        public override WebElement WebElement => new WebImage() { Content = Content };
    }
    /// <summary>
    /// ViewModel pro WebList
    /// </summary>
    public class WebListViewModel : WebElementViewModel<string>
    {
        private bool _isOrdered;
        /// <summary>
        /// Zda je očíslovaný
        /// </summary>
        public bool IsOrdered
        {
            get => _isOrdered;
            set
            {
                if (value != _isOrdered)
                {
                    _isOrdered = value;
                    RaiseNotifyChanged();
                }
            }
        }
        /// <summary>
        /// WebElement
        /// </summary>
        public override WebElement WebElement => new WebList(IsOrdered) { Content = Content?.Split('\n').ToList() };
    }
    /// <summary>
    /// ViewModel pro WebParagraph
    /// </summary>
    public class WebParagraphViewModel : WebElementViewModel<string>
    {
        /// <summary>
        /// WebElement
        /// </summary>
        public override WebElement WebElement => new WebParagraph() { Content = Content };
    }
}
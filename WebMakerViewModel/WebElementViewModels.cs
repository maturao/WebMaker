using System.Collections.ObjectModel;
using System.Linq;
using WebMaker.Web.Elements;
using WebMaker.Web.General;

namespace WebMaker.ViewModel
{
    public class WebHeaderViewModel : WebElementViewModel<string>
    {
        public const int HighestLevel = 1;
        public const int LowestLevel = 6;
        private int _level;

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

        public override WebElement WebElement => new WebHeader(Level) { Content = Content };
    }

    public class WebImageViewModel : WebElementViewModel<string>
    {
        public override WebElement WebElement => new WebImage() { Content = Content };
    }

    public class WebListViewModel : WebElementViewModel<string>
    {
        private bool _isOrdered;

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

        public override WebElement WebElement => new WebList(IsOrdered) { Content = Content?.Split('\n').ToList() };
    }

    public class WebParagraphViewModel : WebElementViewModel<string>
    {
        public override WebElement WebElement => new WebParagraph() { Content = Content };
    }
}
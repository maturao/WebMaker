using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMaker.Html.Style;
using WebMaker.Web.General;


namespace WebMaker.ViewModel
{
    public class BasicWebStyleViewModel : BaseViewModel
    {
        private int _tabsBorderRadius = 0;
        private int _tabBorder = 10;
        private object _backgroundColor =System.Windows.Media.Color.FromRgb(231, 231, 231);
        private object _foregroundColor = System.Windows.Media.Color.FromRgb(104, 154, 248);

        public int TabsBorderRadius
        {
            get => _tabsBorderRadius;
            set
            {
                if (value != _tabsBorderRadius)
                {
                    _tabsBorderRadius = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public int TabBorder
        {
            get => _tabBorder;
            set
            {
                if (value != _tabBorder)
                {
                    _tabBorder = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public object BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor)
                {
                    _backgroundColor = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public object ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                if (value != _foregroundColor)
                {
                    _foregroundColor = value;
                    RaiseNotifyChanged();
                }
            }
        }

        public BasicWebStyle BasicWebStyle
        {
            get
            {
                StyleColor backgroundStyleColor;
                StyleColor foregroundStyleColor;
                var type = BackgroundColor.GetType();
                if (!(BackgroundColor is null))
                {
                    dynamic backgroundColor = BackgroundColor;
                    backgroundStyleColor = new StyleColor(backgroundColor.R, backgroundColor.G, backgroundColor.B);
                }
                else
                {
                    backgroundStyleColor = BasicWebStyle.LightGray;
                }

                if (!(ForegroundColor is null))
                {
                    dynamic foregroundColor = ForegroundColor;
                    foregroundStyleColor = new StyleColor(foregroundColor.R, foregroundColor.G, foregroundColor.B);
                }
                else
                {
                    foregroundStyleColor = BasicWebStyle.BasicColor;
                }

                return new BasicWebStyle(
                    backgroundStyleColor,
                    foregroundStyleColor,
                    TabsBorderRadius,
                    TabBorder);
            }
        }

    }
}

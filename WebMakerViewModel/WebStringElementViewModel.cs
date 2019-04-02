using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMaker.ViewModel
{
    public abstract class WebStringElementViewModel : WebElementViewModel<string>
    {
        public WebStringElementViewModel()
        {
            Content = string.Empty;
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using CefSharp.Wpf;
using CefSharp;

namespace WebMaker.View
{
    public class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
                typeof(BrowserBehavior),
                new PropertyMetadata(OnHtmlChanged));

        public static readonly DependencyProperty RequestHandlerProperty = DependencyProperty.RegisterAttached(
               "RequestHandler",
               typeof(IRequestHandler),
               typeof(BrowserBehavior),
               new PropertyMetadata(OnRequestHandlerChanged));

        [AttachedPropertyBrowsableForType(typeof(ChromiumWebBrowser))]
        public static IRequestHandler GetRequestHandler(ChromiumWebBrowser d)
        {
            return (IRequestHandler)d.GetValue(RequestHandlerProperty);
        }

        public static void SetRequestHandler(ChromiumWebBrowser d, IRequestHandler value)
        {
            d.SetValue(RequestHandlerProperty, value);
        }

        private static void OnRequestHandlerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is ChromiumWebBrowser webBrowser && e.NewValue is IRequestHandler requestHandler)
            {
                webBrowser.RequestHandler = requestHandler;
            }
        }


        [AttachedPropertyBrowsableForType(typeof(ChromiumWebBrowser))]
        public static string GetHtml(ChromiumWebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(ChromiumWebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is ChromiumWebBrowser webBrowser)
                webBrowser.LoadHtml(e.NewValue as string ?? "&nbsp;", "http://rendering/");
        }
    }
}
using WebMaker.Web.General;

namespace WebMaker.Server
{
    /// <summary>
    /// Rozhraní poskytující web pro webový server
    /// </summary>
    public interface IWebSiteProvider
    {
        /// <summary>
        /// Web
        /// </summary>
        WebSite WebSite { get; }
    }
}
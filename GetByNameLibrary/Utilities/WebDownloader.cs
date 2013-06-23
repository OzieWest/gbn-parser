using System;
using System.Net;
using System.Text;

namespace GetByNameLibrary.Utilities
{
    public interface IWebDownloader
    {
        String GetPage(String path);
    }

    public class WebDownloader : IWebDownloader
    {
        public String GetPage(String path)
        {
            using (WebClient webClient = new WebClient())
            {
                if (!(path.Contains("steam") || path.Contains("roxen")))
                    webClient.Encoding = Encoding.UTF8;

                return webClient.DownloadString(path);
            }
        }
    }
}

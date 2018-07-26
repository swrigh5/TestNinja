using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string installername);
    }

    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string installername)
        {
            var client = new System.Net.WebClient();
            client.DownloadFile(url, installername);
        }
    }
}

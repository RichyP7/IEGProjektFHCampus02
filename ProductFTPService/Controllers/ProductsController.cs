using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;


namespace Products.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        private static readonly string ftpUrl = "ftp://test.rebex.net/readme.txt";
        // private static readonly string ressource = "/site/wwwroot/Ressources/products.txt";

        [HttpGet]

        public IEnumerable<string> Get()
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            // FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl + ressource);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("demo", "password");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<string> products = new List<string>();

            string line = reader.ReadLine();
            while (line != null)
            {
                products.Add(line);
                line = reader.ReadLine();
            }

            reader.Close();
            reader.Dispose();
            response.Close();

            return products.ToArray();
        }

    }
}

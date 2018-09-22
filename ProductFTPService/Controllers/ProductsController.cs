using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private string user;
        private string password;
        private string ftpUrl;

        public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger)
        {
            user = configuration.GetConnectionString("user");
            password = configuration.GetConnectionString("password");
            ftpUrl = configuration.GetConnectionString("ftpUrl");
            _logger = logger;
        }

        [HttpGet]

        public IEnumerable<string> Get()
        {

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
 
            request.Credentials = new NetworkCredential(user, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<string> products = new List<string>();

            try
            { 
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    _logger.LogInformation(line);
                    products.Add(line);
                }
            } catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            } finally
            {
                reader.Close();
                reader.Dispose();
                response.Close();
            }

            return products.ToArray();
        }

    }
}

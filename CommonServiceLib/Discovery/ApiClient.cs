using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using Polly;
//using Polly.Retry;

namespace CommonServiceLib.Discovery
{
    public class ApiClient
    {
        public readonly Dictionary<string, List<Uri>> serverUrls;
        private readonly IConfigurationRoot _configuration;
        public readonly HttpClient client;
       // private RetryPolicy _serverRetryPolicy;
        private int _currentConfigIndex;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient()
        {
            _logger = new LoggerFactory().CreateLogger<ApiClient>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            serverUrls = new Dictionary<string, List<Uri>>();
            Initialize();
        }

        public ApiClient(IConfigurationRoot configuration, ILogger<ApiClient> logger)
        {
            _logger = logger;
            _configuration = configuration;

            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            serverUrls = new Dictionary<string, List<Uri>>();
            Initialize();
        }

        public void Initialize()
        {
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(ConsulConfig.Address);
            });

            _logger.LogInformation("Discovering Services from Consul.");

            //var services = consulClient.Agent.Services();
            var services = consulClient.Agent.Services().Result.Response;

            foreach (var service in services)
            {
                if (service.Value.ID != "consul")
                {
                    var serviceUri = new Uri($"{service.Value.Address}:{service.Value.Port}");

                    if (serverUrls.TryGetValue(service.Value.Service, out List<Uri> temp))
                    {
                        temp.Add(serviceUri);
                    }
                    else
                    {
                        List<Uri> urlList = new List<Uri>();
                        urlList.Add(serviceUri);
                        serverUrls.Add(service.Value.Service, urlList);
                    }
                }
            }

            /*  _logger.LogInformation($"{serverUrls.Count} endpoints found.");
              var retries = serverUrls.Count * 2 - 1;
              _logger.LogInformation($"Retry count set to {retries}");

              _serverRetryPolicy = Policy.Handle<HttpRequestException>()
                 .RetryAsync(retries, (exception, retryCount) =>
                 {
                     ChooseNextServer(retryCount);
                 });
                 */
        }

        public String GetLoadBalancedUrl(String serviceId) {
            return this.serverUrls[serviceId].First().ToString();
        }

      /*  private void ChooseNextServer(int retryCount)
        {
            if (retryCount % 2 == 0)
            {
                _logger.LogWarning("Trying next server... \n");
                _currentConfigIndex++;

                if (_currentConfigIndex > _serverUrls.Count - 1)
                    _currentConfigIndex = 0;
            }
        } */

       /* public virtual Task<IEnumerable<Student>> GetStudents()
        {
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                var serverUrl = _serverUrls[_currentConfigIndex];
                var requestPath = $"{serverUrl}api/students";

                _logger.LogInformation($"Making request to {requestPath}");
                var response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<IEnumerable<Student>>(content);
            });
        }

        public virtual Task<IEnumerable<Course>> GetCourses()
        {
            return _serverRetryPolicy.ExecuteAsync(async () =>
            {
                var serverUrl = _serverUrls[_currentConfigIndex];
                var requestPath = $"{serverUrl}api/courses";

                _logger.LogInformation($"Making request to {serverUrl}");
                var response = await _apiClient.GetAsync(requestPath).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
            });
        }*/
    }
}

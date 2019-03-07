//using System;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using ScrapySharp.Network;
//using Stats.Common.Dto;
//using Stats.Fetcher.Modules;

//namespace Stats.Fetcher.Library.Clients
//{
//    public class Browser
//    {
//        private readonly ILogger<Job> logger;
//        private readonly IOptions<AppConfig> appConfig;
//        private readonly ScrapingBrowser browser = new ScrapingBrowser();


//        public Browser(ILogger<Job> logger, IOptions<AppConfig> appConfig)
//        {
//            this.logger = logger;
//            this.appConfig = appConfig;

//            browser.AutoDownloadPagesResources = false;
//        }

//        public async Task<T> GetObject<T>(string url)
//        {
//            string result = await browser.DownloadStringAsync(new Uri(url));
//            logger.LogTrace($"Downloaded {System.Text.Encoding.Unicode.GetByteCount(result)} bytes from {url}");
//            return JsonConvert.DeserializeObject<T>(result);
//        }

//        public async Task<T> GetObject<T>(BaseDto query)
//        {
//            string url = $"{appConfig.Value.ApiUrl}{query.Destination}";
//            string data = JsonConvert.SerializeObject(query);
//            browser.Headers.Add("Content-Type", "application/json");
//            browser.Headers.Add("Accept", "application/json");
//            //browser.Headers.Add("Media-Type", "application/json");
//            try
//            {
//                var response = await browser.ExecuteRequestAsync(new Uri(url), HttpVerb.Get, data, contentType:"application/json");
                
//                using (Stream stream = response.GetResponseStream())
//                {
//                    if (stream == null)
//                        return default(T);

//                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
//                    string responseString = reader.ReadToEnd();
                    
//                    logger.LogTrace($"Downloaded {System.Text.Encoding.Unicode.GetByteCount(responseString)} bytes from {url}");

//                    return JsonConvert.DeserializeObject<T>(responseString);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex.Message);
//            }

//            return default(T);
//        }

//        public async Task PostData(BaseDto dto)
//        {
//            string url = $"{appConfig.Value.ApiUrl}{dto.Source}";
//            var data = JsonConvert.SerializeObject(dto);
//            await browser.ExecuteRequestAsync(new Uri(url), HttpVerb.Post, data);
//        }
//    }
//}

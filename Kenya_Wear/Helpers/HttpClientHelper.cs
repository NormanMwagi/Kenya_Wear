using Newtonsoft.Json.Linq;
using System.Net;
using NLog;
namespace Kenya_Wear.Helpers
{
    public static class HttpClientHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public class HttpHandler
        {
            public string HttpClientPost(string url, JObject request_data)
            {
                string result = null;

                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url)!;
                    httpRequest.Method = "post";
                    httpRequest.ContentType = "application/json";
                    using (var dataStream = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        dataStream.Write(request_data);
                        dataStream.Flush();
                        dataStream.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("ERROR", "HttpClientPost | Exception ->" + ex.Message);
                }

                return result;
            }
        }
    }
}
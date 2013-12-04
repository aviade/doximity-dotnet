using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doximity.Api
{
    public class DoximityDataAccess
    {
        private const string BaseRoot = "https://www.doximity.com";

        private readonly HttpClient client;

        public DoximityDataAccess()
        {
            client = new HttpClient { BaseAddress = new Uri(BaseRoot) };
        }

        public static string ResolveAuthUrl(string clientId, string redirectUri, string scope)
        {
            string url =
                string.Format(
                    "{0}/oauth/authorize?client_id={1}&redirect_uri={2}&response_type=code&scope={3}",
                    BaseRoot, clientId, redirectUri, scope);

            return url;
        }

        public string ExchangeCodeWithAccessToken(string code, string redirectUri, string clientId, string clientSecret)
        {
            var collection = new Dictionary<string, string>();
            collection.Add("grant_type", "authorization_code");
            collection.Add("code", code);
            collection.Add("redirect_uri", redirectUri);
            collection.Add("client_id", clientId);
            collection.Add("client_secret", clientSecret);

            Task<HttpResponseMessage> task = client.PostAsync("/oauth/token", new FormUrlEncodedContent(collection));
            HttpResponseMessage responseMessage = task.Result;
            string content = responseMessage.Content.ReadAsStringAsync().Result;

            Dictionary<string, string> results = ParseContent(content);
            string token = results["access_token"];
            return token;
        }

        public string GetColleagues(string accessToken)
        {
            Task<HttpResponseMessage> task = client.GetAsync("api/v1/colleagues?access_token=" + accessToken);
            HttpResponseMessage responseMessage = task.Result;
            string ret = responseMessage.Content.ReadAsStringAsync().Result;
            return ret;
        }

        public string GetCurrentUserProfile(string accessToken)
        {
            Task<HttpResponseMessage> task = client.GetAsync("api/v1/users/current?access_token=" + accessToken);
            HttpResponseMessage responseMessage = task.Result;
            string ret =  responseMessage.Content.ReadAsStringAsync().Result;
            return ret;
        }

        private Dictionary<string, string> ParseContent(string content)
        {
            var ret = new Dictionary<string, string>();
            string[] results = content.Split(',');
            foreach (var result in results)
            {
                string[] keyValuePair = result.Split(':');
                if (keyValuePair.Length != 2)
                {
                    throw new Exception(string.Format(
                        "Can't parse response, expected 2 elements at {0}, found {1} ",
                        result, keyValuePair.Length));
                }
                string key = keyValuePair[0].Replace("{", string.Empty).Replace(@"""", string.Empty);
                string value = keyValuePair[1].Replace(@"""", string.Empty);
                ret.Add(key, value);
            }

            return ret;
        }


    }
}

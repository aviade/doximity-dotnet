using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Doximity.WebApp.Models;

namespace Doximity.WebApp.Controllers
{
    public class EntryController : Controller
    {
        public ActionResult Auth(string state, string code)
        {
            string accessToken = ExchangeCodeWithAccessToken(code);

            string userProfile = GetCurrentUserProfile(accessToken);
            string colleagues = GetColleagues(accessToken);
            return View(new AuthViewModel(userProfile, colleagues));
        }

        private string GetColleagues(string accessToken)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.doximity.com")
            };
            Task<HttpResponseMessage> task = httpClient.GetAsync("api/v1/colleagues?access_token=" + accessToken);
            task.Wait();
            var result = task.Result;
            result.EnsureSuccessStatusCode();

            string ret = result.Content.ReadAsStringAsync().Result;
            return ret;
        }

        private string GetCurrentUserProfile(string accessToken)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.doximity.com")
            };
            Task<HttpResponseMessage> task = httpClient.GetAsync("api/v1/users/current?access_token=" + accessToken);
            task.Wait();
            var result = task.Result;
            result.EnsureSuccessStatusCode();

            string ret = result.Content.ReadAsStringAsync().Result;
            return ret;
        }

        private string ExchangeCodeWithAccessToken(string code)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.doximity.com")
            };

            var collection = new Dictionary<string, string>();
            collection.Add("grant_type", "authorization_code");
            collection.Add("code", code);
            collection.Add("redirect_uri", Configuration.Instance.RedirectUri);
            collection.Add("client_id", Configuration.Instance.ClientId);
            collection.Add("client_secret", Configuration.Instance.ClientSecret);

            Task<HttpResponseMessage> task = httpClient.PostAsync("/oauth/token", new FormUrlEncodedContent(collection));
            // Wait default 90 sec
            task.Wait();

            HttpResponseMessage responseMessage = task.Result;
            responseMessage.EnsureSuccessStatusCode();
            string content = responseMessage.Content.ReadAsStringAsync().Result;

            Dictionary<string, string> results = ParseContent(content);
            string token = results["access_token"];
            return token;
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

        public ActionResult Ping()
        {
            return View("Auth", new AuthViewModel("aviad", "ezra"));
        }

        public ActionResult Index()
        {
            return View("Auth", new AuthViewModel("aviad", "ezra"));
        }

    }

    public class AuthViewModel
    {
        private readonly string userProfile;
        private readonly string colleagues;

        public AuthViewModel(string userProfile, string colleagues)
        {
            this.userProfile = userProfile;
            this.colleagues = colleagues;
        }

        public string UserProfile
        {
            get { return userProfile; }
        }

        public string Colleagues
        {
            get { return colleagues; }
        }
    }
}

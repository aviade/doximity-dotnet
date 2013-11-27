using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doximity.Api
{
    public class DoximityDataAccess
    {
        private readonly HttpClient client;

        public DoximityDataAccess()
        {
            client = new HttpClient { BaseAddress = new Uri("https://www.doximity.com") };
        }

        public void Authenticate(string appId, Uri redirectUri)
        {
            Task<HttpResponseMessage> task = client.GetAsync(string.Format("/oauth/authorize?client_id={0} " +
                                                        "&response_type=code" +
                                                        "&redirect_uri={1}&" +
                                                        "scope=basic%" +
                                                        "20colleagues" +
                                                        "&type=verify&" +
                                                        "state={2}", appId, redirectUri.AbsoluteUri, Guid.NewGuid()));
            task.Wait();
            HttpResponseMessage responseMessage = task.Result;
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}

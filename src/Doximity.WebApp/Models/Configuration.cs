using System.Configuration;

namespace Doximity.WebApp.Models
{
    public class Configuration
    {
        private readonly static Configuration instance = new Configuration();

        private readonly string redirectUri;
        private readonly string clientId;
        private readonly string clientSecret;

        private Configuration()
        {
            redirectUri = ConfigurationManager.AppSettings["RedirectUrl"];
            clientId = ConfigurationManager.AppSettings["ClientId"];
            clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        }

        public string RedirectUri
        {
            get { return redirectUri; }
        }

        public string ClientId
        {
            get { return clientId; }
        }

        public string ClientSecret
        {
            get { return clientSecret; }
        }

        public static Configuration Instance
        {
            get { return instance; }
        }
    }
}
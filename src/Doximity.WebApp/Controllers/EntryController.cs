using System.Threading.Tasks;
using System.Web.Mvc;
using Doximity.Api;
using Doximity.WebApp.Models;

namespace Doximity.WebApp.Controllers
{
    public class EntryController : Controller
    {
        private readonly DoximityDataAccess dataAccess;

        public EntryController()
        {
            dataAccess = new DoximityDataAccess();
        }

        public ActionResult Auth(string state, string code)
        {
            string token = dataAccess.ExchangeCodeWithAccessToken(code, 
                Configuration.Instance.RedirectUri, Configuration.Instance.ClientId, Configuration.Instance.ClientSecret);
            string userProfile = dataAccess.GetCurrentUserProfile(token);
            string colleagues = dataAccess.GetColleagues(token);

            return View(new AuthViewModel(userProfile, colleagues));
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

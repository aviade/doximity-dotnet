using System.Web.Mvc;

namespace Doximity.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            const string clientId = "e5512e621ef26a5b702c6953c5154c6dd4bf5341b40a87c661a35415418d2039";
            const string redirectUrl = "https%3A%2F%2Fdoximity.azurewebsites.net%2Fapi%2Fauth";
            string url = string.Format("https://www.doximity.com/oauth/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=email", clientId, redirectUrl);
            return View(new IndexViewModel(url));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Doximity, the most powerful medical directory and communication tool in the world, is now available in Windwows Store!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
    }

    public class IndexViewModel
    {
        private readonly string url;

        public IndexViewModel(string url)
        {
            this.url = url;
        }

        public string Url
        {
            get { return url; }
        }
    }
}
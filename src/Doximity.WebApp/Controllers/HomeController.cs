using System.Web.Mvc;
using Doximity.Api;
using Doximity.WebApp.Models;

namespace Doximity.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = DoximityDataAccess.ResolveAuthUrl(Configuration.Instance.ClientId,
                Configuration.Instance.RedirectUri, "basic%20colleagues");
            
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
using System.Web.Mvc;
using Doximity.WebApp.Models;

namespace Doximity.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = string.Format("https://www.doximity.com/oauth/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=basic%20colleagues", 
                Configuration.Instance.ClientId, Configuration.Instance.RedirectUri);
            
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
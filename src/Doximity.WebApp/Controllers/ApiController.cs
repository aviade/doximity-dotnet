using System.Web.Http;
using System.Web.Mvc;

namespace Doximity.WebApp.Controllers
{
    public class ApiController : Controller
    {
        public ActionResult Auth(string state, string code)
        {
            return View(new AuthViewModel(state, code));

        }
    }

    public class AuthViewModel
    {
        private readonly string state;
        private readonly string code;

        public AuthViewModel(string state, string code)
        {
            this.state = state;
            this.code = code;
        }

        public string State
        {
            get { return state; }
        }

        public string Code
        {
            get { return code; }
        }
    }
}

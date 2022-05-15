using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        public IActionResult Index()
        {
            return Ok("Hello World");
        }
    }
}
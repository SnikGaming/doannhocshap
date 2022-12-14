using BaiTapNho.models;
using BaiTapNho.Models;
using MessagePack;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapNho.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AspContext _context;
        private readonly IHttpContextAccessor _http;


        public HomeController(AspContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }
        public IActionResult Index()
        {
            if (_http.HttpContext.Session.GetString(SessionKey.userKey) == null ||
                _http.HttpContext.Session.GetString(SessionKey.roleKey) != SessionKey.adminVal)
            {
                return new RedirectToRouteResult(new RouteValueDictionary(
                new {area="",  action = "Index", controller = "Home" }));


            } 
            return View();
        }
    }
}

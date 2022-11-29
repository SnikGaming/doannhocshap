using BaiTapNho.models;
using BaiTapNho.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaiTapNho.Controllers
{
    public class HomeController : Controller
    {
        private readonly AspContext _context;
        private readonly IHttpContextAccessor _http;


        public HomeController(AspContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (_http.HttpContext.Session.GetString("us") != null) {
                if (
                _http.HttpContext.Session.GetString(SessionKey.roleKey) == SessionKey.adminVal)
                {

                    return new RedirectToRouteResult(new RouteValueDictionary(
                     new { area = "Admin", action = "Index", controller = "Home" }));
                }
                else if (
                    _http.HttpContext.Session.GetString(SessionKey.roleKey) == SessionKey.editVal)
                {

                    return new RedirectToRouteResult(new RouteValueDictionary(
                     new { area = "Editor", action = "Index", controller = "Home" }));
                }
            }
            
            Member _member = new Member();

            return View(_member);
        }
        [HttpPost]
        public IActionResult Index(Member member)
        {
            var ad = _context.Members.Where(m => m.Username ==
           member.Username && m.Password == member.Password && m.Status == 1 && m.Role == 1).FirstOrDefault();

            var ed = _context.Members.Where(m => m.Username ==
           member.Username && m.Password == member.Password && m.Status == 1 && m.Role == 2).FirstOrDefault();
            //Ad min hay ad dang nhap deu se luu lai trong session
            if (ad != null || ed != null)
            {
                _http.HttpContext.Session.SetString(SessionKey.userKey, member.Username);
            }
            if (ad != null && ed == null)

            {
                //neu la add min thi session add !=nul
                //ViewBag.Message = "Success full login";
                _http.HttpContext.Session.SetString(SessionKey.roleKey, SessionKey.adminVal);
                return new RedirectToRouteResult(new RouteValueDictionary(
               new { area = "Admin", action = "Index", controller = "Home" }));

            }
            else if (ad == null && ed != null)
            {
                //neu la add min thi session editor !=nul
                _http.HttpContext.Session.SetString(SessionKey.roleKey,SessionKey.editVal);
                return new RedirectToRouteResult(new RouteValueDictionary(
            new { area = "Editor", action = "Index", controller = "Home" }));
            }

            else

            {

                ViewBag.Message = "Invalid login detail.";

            }

            return View(member);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
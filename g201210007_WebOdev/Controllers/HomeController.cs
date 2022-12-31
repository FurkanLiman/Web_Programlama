using g201210007_WebOdev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security;
using System.Security.Claims;

namespace g201210007_WebOdev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        { //TempData ile Controllerlar arası user aktarımı yapıldı.
            
            var UserCache = User.Claims.ToList(); //Autherision işlemi sırasında kaydettiğimiz kişi bilgilerini tekrar kullanıyoruz bknz:AccountController.Login(e,p)->if içindeki var claim 
            var UserEmail = UserCache[0].Value.ToString();
            var UserName = UserCache[1].Value.ToString();
            Account UserC = new Account(UserEmail, "***");
            UserC.Name = UserName;
            if (UserCache == null)// kişi giriş yapmadıysa logine yolladık
            {   
                return RedirectToAction("Login", "Account");
            }
            else
            { //Eğer daha önce biri giriş yaptıysa cacheden anamenüye gönderdik

                TempData["UserData"] = JsonConvert.SerializeObject(UserC);
                CoffeeSql getCoffee = new CoffeeSql();
                List<Coffee> coffees = getCoffee.CoffeeRead();
                //TempData["coffeeList"] = coffees;
                
                return View(coffees);
            }
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
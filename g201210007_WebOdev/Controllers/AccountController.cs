using g201210007_WebOdev.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;


namespace g201210007_WebOdev.Controllers
{
    public class AccountController : Controller
    {

        [HttpPost]
        public IActionResult Login(String Email, String Password)
        {//Başlangıç adresimiz Account/Login
           //Login .cshtml'dan Email ve Password verisini alıp Model üzerinden Sql kontrolü sağlayacak.
            Account User = new Account(Email, Password);
            
            if (User.UserControl(User))
            {//Başarılı giriş sonucu ana menüye göndermekte
                TempData["UserData"] = JsonConvert.SerializeObject(User);
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult SignIn(String Email, String Password)
        {
            Account User = new Account(Email,Password);

            if (User.UserWriteSql(User))
            {//kişi kayıdı başarılıysa logine giriş yapmaya yollandı
                return RedirectToAction("Login");
            }
            else
            {//kişi kaydında sıkıntı varsa tekrar kayıt sekmesine yollandı
                return View();
            }
        }

        public IActionResult SignIn()
        {
            //sign In işlemi yaptır
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Verify()
        {
            return View();  
        }
    }
}

using g201210007_WebOdev.Models;
using Microsoft.AspNetCore.Mvc;

namespace g201210007_WebOdev.Controllers
{
    public class ProfileController : Controller
    {
        
        public IActionResult Index()
        {
            var UserCache = User.Claims.ToList();
            var UserEmail = UserCache[0].Value.ToString();
            var UserName = UserCache[1].Value.ToString();
            var UserAuthority = UserCache[2].Value.ToString();
            UserSql CheckSql= new UserSql();
            Account UserN = new Account(UserEmail,"*");
            UserN.Authority = UserAuthority;
            UserN.Name= UserName;
            UserN = CheckSql.UserInfo(UserEmail);
            if (UserN.Authority == "1") { 
               
                return araAction();
                //return RedirectToAction("AdminPanel", "Profile");
            }
            else
            {
                CoffeeSql getCoffee = new CoffeeSql();
                List<Coffee> coffees = getCoffee.CoffeeRead();
                return View(coffees);
            }
        }
        public IActionResult araAction()
        {
           
            ProfileController takesAdmin = new ProfileController();
            CoffeeSql getCoffee = new CoffeeSql();
            List<Coffee> coffees = getCoffee.CoffeeRead();



            return takesAdmin.AdminPanel(coffees);
        }
        [HttpPost]
        public IActionResult AdminPanel(string id)
        {
            CoffeeSql getCoffee = new CoffeeSql();
            getCoffee.DeleteCoffee(id);
            return araAction();

        }
        private IActionResult AdminPanel(List<Coffee> coffees)
        {
            
            return View("AdminPanel",coffees);

        
        }
        [HttpPost]
        public IActionResult AdminCoffeeAdd(string name, string brand, string taste, string image)
        {
            CoffeeSql addCoffee = new CoffeeSql();
            addCoffee.AddCoffee(name, brand, taste, image);

            return RedirectToAction("araAction");
        }
        [HttpPost]
        public IActionResult deleteComment(string id, string comId) 
        {

            CoffeeSql delComment= new CoffeeSql();
            delComment.deleteCommentSql(id, comId);
            return RedirectToAction("Index", "Profile"); 
        }

    }
}

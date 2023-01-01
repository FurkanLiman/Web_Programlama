using g201210007_WebOdev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace g201210007_WebOdev.Controllers
{
    public class CoffeeController : Controller
    {
        // GET: CoffeeController
        [HttpPost]
        public ActionResult Index(IFormCollection coffeId)
        {
            
            int Id = int.Parse(coffeId["id"]);
            CoffeeSql getCoffeeInfo = new CoffeeSql();
            Coffee coffee = getCoffeeInfo.CoffeeReadInfo(Id);
            
            return RedirectToAction("CoffeeDetails", "Coffee", coffee);
        }
        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }
        public IActionResult Products()
        {
            CoffeeSql getCoffee = new CoffeeSql();
            List<Coffee> coffees = getCoffee.CoffeeRead();
            return View(coffees);
        }

        [HttpPost]
        public ActionResult CoffeeDetails(string comment,int id)
        {
            var UserCache = User.Claims.ToList();
            var UserEmail = UserCache[0].Value.ToString();
            var UserComment = UserCache[1].Value.ToString();
            UserComment += ": " + comment;
            Coffee coffee = new Coffee();
            CoffeeSql commentSender= new CoffeeSql();
            coffee = commentSender.MakeComment(UserComment, id);
            return RedirectToAction("CoffeeDetails","Coffee", coffee);
        }
        [HttpGet]
        public ActionResult CoffeeDetails(Coffee coffee)
        {
            return View(coffee);
        }

        // GET: CoffeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoffeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoffeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoffeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoffeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoffeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

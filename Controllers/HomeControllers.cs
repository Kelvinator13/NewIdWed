using IdWedNu.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdWedNu.Controllers
{
    public class HomeControllers : Controller
    {
        ApplicationDbContext _context;
        public HomeControllers(ApplicationDbContext context)
        {
            _context = context;
        }


        //post
        public async Task<IActionResult> Index()
        {


            return View();
        }



        public IActionResult RedirectoToFacebook()
        {
            Models.Services.FacebookDataRequest facebookDataRequest = new Models.Services.FacebookDataRequest(_context);
            facebookDataRequest.postman();

            return View("Index");




            return View();

        }



        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public void MatchRestaurants()
        //{
        //    string customerOneChoice;
        //    string customerTwoChoice;
        //    string matchedChoice;

        //    if (customerOneChoice == customerTwoChoice)
        //    {
        //        return matchedChoice;
        //    }
        //}
    }
}

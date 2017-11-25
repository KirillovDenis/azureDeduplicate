using azureDeduplicate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using NHunspell;

namespace azureDeduplicate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deduplicate(string FirstString, string SecondString)
        {

            String tmp;
            PairForDeduplication tmp2 = new PairForDeduplication(FirstString, SecondString);
            bool isDup = tmp2.IsDuplicate();
            if (isDup)
            {
                tmp = "Дубли";
            }
            else
            {
                tmp = "Не Дубли";
            }
            return PartialView((object)tmp);
        }

        [HttpPost]
        public ActionResult SpellCheck(string inputString)
        {
            bool ans;

            using (Hunspell hspell = new Hunspell(System.Web.HttpContext.Current.Server.MapPath("~/Assets/Dictionaries/ru_RU.aff"), System.Web.HttpContext.Current.Server.MapPath("~/Assets/Dictionaries/ru_RU.dic")))
            {
                ans = hspell.Spell(inputString);
            }
            if (ans)
            {
                return PartialView((object)"Правильно");
            }
            else
            {
                return PartialView((object)"Неправильно");
            }
        }
    }
}
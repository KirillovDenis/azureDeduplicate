using azureDeduplicate.Models;
using System;
using System.Web.Mvc;

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
                tmp = "Duplicate";
            }
            else
            {
                tmp = "No duplicate";
            }
            return PartialView((object)tmp);
        }

        [HttpPost]
        public ActionResult SpellCheck(string inputString)
        {
            if (this.HttpContext.Request.Form.AllKeys.GetValue(0).ToString() == "add")
            {
                RussianDictionary.Instance.AddWord(inputString);
                return PartialView((object)"The word has added");
            }
            else
            {
                if (RussianDictionary.Instance.IsRigthSpelling(inputString))
                {
                    return PartialView((object)"Right");
                }
                else
                {
                    return PartialView((object)"Wrong");
                }
            }
        }
    }
}
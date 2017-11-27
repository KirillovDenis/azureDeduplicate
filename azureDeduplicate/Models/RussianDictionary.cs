using NHunspell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace azureDeduplicate.Models
{
    public class RussianDictionary
    {
        private readonly Hunspell _hunspell;
        private RussianDictionary()
        {
            _hunspell = new Hunspell(
                            HttpContext.Current.Server.MapPath("~/Assets/Dictionaries/ru_RU.aff"),
                            HttpContext.Current.Server.MapPath("~/Assets/Dictionaries/ru_RU.dic"));
        }

        private static RussianDictionary _instance;
        public static RussianDictionary Instance
        {
            get { return _instance ?? (_instance = new RussianDictionary()); }
        }


        public bool IsRigthSpelling(string word)
        {
            return _hunspell.Spell(word);
        }

        public void AddWord(string word)
        {
            if (!_hunspell.Spell(word))
            {
                _hunspell.Add(word);
            }
        }
    }
}
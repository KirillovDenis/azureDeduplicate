using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using azureDeduplicate;
using azureDeduplicate.Models;
using System.Text.RegularExpressions;

namespace azureDeduplicate.Tests.Models
{
    [TestClass]
    public class PairForDeduplicationTest
    {
        [TestMethod]
        public void TestUnification()
        {
            string str1 = @",с\т'р@а_н#н!а~я/ %с:т;р=о<к>а, |о[ч]е$н&ь^ ?с+т(р)а-нная";
            string str2 = "cтранная строка очень странная";

            PairForDeduplication pair = new PairForDeduplication(str1, str2);

            Assert.IsTrue(pair.IsDuplicate());
        }

    }
}

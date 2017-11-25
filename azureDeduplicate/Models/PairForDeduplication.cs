using System;
using System.Collections.Generic;
using DuoVia.FuzzyStrings;
using System.Text.RegularExpressions;

namespace azureDeduplicate.Models
{
    public class PairForDeduplication
    {
        private string firstString;
        private string unifFirstString;
        private string secondString;
        private string unifSecondString;
        public string FirstString { get { return firstString; } }
        public string SecondString { get { return secondString; } }


        public PairForDeduplication(string str1, string str2)
        {
            firstString = str1;
            unifFirstString = str1.ToLower();
            secondString = str2;
            unifSecondString = str2.ToLower();

            Unificate();
        }

        public bool IsDuplicate()
        {
            int len1 = unifFirstString.Length;
            int len2 = unifSecondString.Length;
            double avLen = (len1 + len2) / 2.0;

            int levenDist = LevenshteinDistanceExtensions.LevenshteinDistance(unifSecondString, unifFirstString);
            double dicecoef = DiceCoefficientExtensions.DiceCoefficient(unifFirstString, unifSecondString);
            Tuple<string, double> subs = LongestCommonSubsequenceExtensions.LongestCommonSubsequence(unifFirstString, unifSecondString);
            string metFirst = DoubleMetaphoneExtensions.ToDoubleMetaphone(unifFirstString);
            string metSecond = DoubleMetaphoneExtensions.ToDoubleMetaphone(unifSecondString);
            int metLevDist = LevenshteinDistanceExtensions.LevenshteinDistance(metFirst, metSecond);

            if (levenDist / avLen < 0.2 && dicecoef > 0.8)
            {
                return true;
            }
            else if (dicecoef > 0.9)
            {
                return true;
            }
            else if (subs.Item2 > 0.75)
            {
                return true;
            }
            else if (metLevDist == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Unificate()
        {
            string pattern = @"(?x) (-й\b)| (-ая\b)| (-ый\b)| (-е\b)| (-я\b)| (-ое\b)| (-го\b) | [,\\'@_#!~/%:;=<>|\[\]\$&\^\?\+\(\)]";
            string pattern2 = @"(?x) (-) | (\s{2,})";
            string pattern3 = @"\s*-\s*";

            unifFirstString = Regex.Replace(unifFirstString, pattern3, "-");
            unifSecondString = Regex.Replace(unifSecondString, pattern3, "-");
            unifFirstString = Regex.Replace(unifFirstString, pattern, "");
            unifSecondString = Regex.Replace(unifSecondString, pattern, "");
            unifFirstString = Regex.Replace(unifFirstString, pattern2, " ");
            unifSecondString = Regex.Replace(unifSecondString, pattern2, " ");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZOO_Accounting.Helpers
{
    class NumbersOnlyRegexChecker:IRegexChecker
    {
        public bool Check(string text)
        {
            return Regex.IsMatch(text, "^[0-9]+$");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZOO_Accounting.Helpers
{
    class TenCharRegexChecker:IRegexChecker
    {
        public bool Check(string text)
        {
            return Regex.IsMatch(text, @"^[A-Z0-9]{10,10}$");
        }
    }
}

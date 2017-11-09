using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.Helpers
{
    class NonEmptyRegexChecker:IRegexChecker
    {
        public bool Check(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}

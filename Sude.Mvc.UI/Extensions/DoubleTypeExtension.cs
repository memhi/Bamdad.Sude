using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sude.Mvc.UI
{
    public static class DoubleExtension
    {

        public static string ToStringDigit(this double value)
        {
            return value.ToString("###");
        }


    }
}

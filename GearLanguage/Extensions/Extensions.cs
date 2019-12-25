using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GearLanguage.Extensions
{
    static class Extensions
    {
        public static string RemoveQuotes(this string token)
        {
            return new string(
                token.ToCharArray()
                .Where(c => c != '"')
                .ToArray()
           );
        }
    }
}

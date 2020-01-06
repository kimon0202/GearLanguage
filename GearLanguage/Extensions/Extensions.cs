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

        public static string RemoveColon(this string token)
        {
            string value = token;
            string toReturn = "";

            for(int i = 0; i < value.Length; i++)
            {
                if(i == 0)
                {
                    if (value[i] != ':') toReturn += value[i];
                }else
                {
                    toReturn += value[i];
                }
            }

            return toReturn.Trim();
        }
    }
}

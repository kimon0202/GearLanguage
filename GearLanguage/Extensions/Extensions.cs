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

        public static List<char> ReplaceText(this List<char> targetText, char[] stringToRemove, char[] stringToAdd)
        {
            for(int i = 0;i < targetText.Count;i++)
            {
                for(int j = 0;j < stringToRemove.Length;j++)
                {
                    if (targetText[i + j] != stringToRemove[j])
                        break;
                    else if (j == stringToRemove.Length - 1)
                    {
                        for (int k = 0; k < stringToRemove.Length; k++)
                        {
                            targetText.RemoveAt(i + j - k);
                        }
                        if (targetText.Count == 0)
                        {
                            for (int k = 0; k < stringToAdd.Length; k++)
                            {
                                targetText.Add(stringToAdd[k]);
                            }
                        }
                        else
                        {
                            for (int k = 0; k < stringToAdd.Length; k++)
                            {
                                targetText.Insert(i + j - k, stringToAdd[k]);
                            }
                        }
                    }
                }
            }

            return targetText;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System;
using GearLanguage.Extensions;

namespace GearLanguage.Lang
{
    class Lexer
    {
        private string code;
        private List<string> tokens;

        private char[] codeSplitChars;

        public Lexer(string code)
        {
            this.code = code;
            tokens = new List<string>();

            codeSplitChars = new char[]
            {
                '\n',
                ':'
            };
        }

        public string[] TokenizeTest()
        {
            string token = "";
            List<string> splitCode = new List<string>();

            bool stringInit = false;

            for(int i = 0; i < code.Length; i++)
            {
                if (code[i] == '"') stringInit = !stringInit;

                if(code[i] == ':' && !stringInit)
                {
                    if (token != "")
                    {
                        string value = token.RemoveColon();
                        value = value.Trim();
                        splitCode.Add(value);
                    }

                    token = "";
                }

                if(code[i] == '\n')
                {
                    if(token != "")
                    {
                        string value = token.RemoveColon();
                        value = value.Trim();
                        splitCode.Add(value);
                    }

                    token = "";
                }

                token += code[i];
            }

            if (token != "")
            {
                string value = token.RemoveColon();
                value = value.Trim();
                splitCode.Add(value);
            }

            return splitCode.ToArray();
        }

        public string[] Tokenize(string[] _split)
        {
            string token = "";
            //string[] splitCode = code.Split(codeSplitChars);
            string[] splitCode = _split;

            foreach(string split in splitCode)
            {
                token = "";
                string value = split;
                value = value.Trim();

                foreach(char c in split)
                {
                    if(c == '(' || c == ';')
                    {
                        tokens.Add(token);
                        token = "";
                    }

                    token += c;
                }

                token = token.Trim();
                tokens.Add(token);
            }

            string[] returnArray = tokens.ToArray();
            return ClearTokens(returnArray);
        }

        private string[] ClearTokens(string[] _tokens)
        {
            List<string> tmpList = new List<string>();

            foreach(string _token in _tokens)
            {
                string tmpToken = _token.Trim();
                string token = new string(
                    tmpToken.ToCharArray()
                    .Where(c => c != ';')
                    .ToArray()
                );

                if (token != "")
                    tmpList.Add(token);
            }

            return tmpList.ToArray();
        }
    }
}

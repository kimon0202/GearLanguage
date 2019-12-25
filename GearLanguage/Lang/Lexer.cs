using System.Collections.Generic;
using System.Linq;

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

            codeSplitChars = new char[2]
            {
                ':',
                '\n',
            };
        }

        public string[] Tokenize()
        {
            string token = "";
            string[] splitCode = code.Split(codeSplitChars);

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

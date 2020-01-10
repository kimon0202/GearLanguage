using System.Collections.Generic;
using System.Linq;
using System;
using GearLanguage.Extensions;

/// Todo:
/// remove second layer of foreach in tokenize
/// in order to improve lexing performance

namespace GearLanguage.Lang
{
    /// <summary>
    /// Handles spliting large chuncks of gear code
    /// into smaller tokens that will be passed to
    /// the parser later on the process
    /// </summary>
    class Lexer
    {
        /// <summary>
        /// Variable that holds the actual code that was in the file
        /// given in the command line.
        /// </summary>
        private readonly string code;

        /// <summary>
        /// Varibale that holds the tokens created from the gear code
        /// specified on the "code" variable.
        /// </summary>
        private List<string> tokens;

        /// <summary>
        /// Constructor: Initialize tokens list and saves the file content
        /// in "code variable"
        /// </summary>
        /// <param name="code">Content from the file given in the command line</param>
        public Lexer(string code)
        {
            this.code = code;
            tokens = new List<string>();
        }

        /// <summary>
        /// Breaks up gear file content in different lines
        /// </summary>
        /// <returns>Array of lines from gear file</returns>
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

        /// <summary>
        /// Splits lines into tokens
        /// </summary>
        /// <param name="_split">Lines of gear code</param>
        /// <returns>Tokens</returns>
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

        /// <summary>
        /// Cleans tokens by removing unnecessary chars, such as ';'.
        /// Also removes empty tokens from the content that will be
        /// given to the parser.
        /// </summary>
        /// <param name="_tokens">Array of tokens to clean</param>
        /// <returns>Array of cleaned tokens</returns>
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

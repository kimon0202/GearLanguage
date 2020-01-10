using System;
using System.Collections.Generic;
using System.Text;
using DynamicExpresso;
using GearLanguage.Base_Classes;
using GearLanguage.Extensions;

namespace GearLanguage.Lang
{
    class ExpressionParser
    {
        private List<ExpressionNode> nodes;

        public ExpressionParser()
        {
            nodes = new List<ExpressionNode>();
        }

        public ExpressionNode[] GetNodes()
        {
            return nodes.ToArray();
        }

        public string[] Parse(string expression)
        {
            string token = "";
            List<string> tokens = new List<string>();

            bool stringStarted = false;

            foreach(char c in expression)
            {
                if(c == '"')
                {
                    stringStarted = !stringStarted;
                }

                if(Char.IsWhiteSpace(c) && !stringStarted)
                {
                    tokens.Add(token);
                    token = "";
                }

                token += c;
            }

            tokens.Add(token);

            for(int i = 0; i < tokens.Count; i++)
            {
                tokens[i] = tokens[i].Trim();
            }

            return tokens.ToArray();
        }
    }
}

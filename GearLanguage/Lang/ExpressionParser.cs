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
        private Tree tree;
        private DynamicExpresso.Interpreter evaluator;

        private List<string> symbolsList;
        private List<ExpressionNode> nodes;
        private Dictionary<string, ExpressionNode> expressionNodes;

        private string[] symbols = new string[]
        {
            "+",
            "-",
            "*",
            "/",
            "(", ")",
            "[", "]",
            "{", "}"
        };

        public ExpressionParser(Tree tree)
        {
            this.tree = tree;
            evaluator = new DynamicExpresso.Interpreter();

            symbolsList = new List<string>(symbols);
            nodes = new List<ExpressionNode>();

            expressionNodes = new Dictionary<string, ExpressionNode>();
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

            /**foreach(string tok in tokens)
            {
                double number;
                ExpressionType type = ExpressionType.String;

                if (tok.Contains("\"")) type = ExpressionType.String;
                else if (symbolsList.Contains(tok.Trim())) type = ExpressionType.Operator;
                else if (tree.VarExists(tok.Trim())) type = ExpressionType.Variable;
                else if (double.TryParse(tok.Trim(), out number)) type = ExpressionType.Number;

                ExpressionNode node = new ExpressionNode(type, tok.Trim());
                nodes.Add(node);
                Console.WriteLine(node.GetExpressionType() + ": " + node.GetValue());
            }*/

            for(int i = 0; i < tokens.Count; i++)
            {
                tokens[i] = tokens[i].Trim();
            }

            return tokens.ToArray();
        }
    }
}

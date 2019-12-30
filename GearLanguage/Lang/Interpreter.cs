using System;
using System.Collections.Generic;
using System.Text;
using GearLanguage.Base_Classes;
using GearLanguage.Extensions;
using DynamicExpresso;

namespace GearLanguage.Lang
{
    class Interpreter
    {
        private Tree tree;
        private char[] addSymbol;

        private ExpressionParser expressionParser;
        private DynamicExpresso.Interpreter evaluator;

        public Interpreter(Tree tree)
        {
            this.tree = tree;
            addSymbol = new char[] { '+' };

            expressionParser = new ExpressionParser(tree);
            evaluator = new DynamicExpresso.Interpreter();
        }

        public void Run()
        {
            foreach(MethodNode metNode in tree.GetMethods())
            {
                foreach(Node node in metNode.GetNodes())
                {
                    if(node.GetName() == "print")
                    {
                        //HandleStringPrint(node.GetValue());
                        string[] tokens = expressionParser.Parse(node.GetValue());
                        string value = BuildString(tokens);
                        HandlePrint(value);
                    }

                    if(tree.VarExists(node.GetName()))
                    {
                        tree.SetVar(node.GetName(), node.GetValue());
                    }
                }
            }
        }

        private string BuildString(string[] tokens)
        {
            StringBuilder builder = new StringBuilder();

            foreach(string token in tokens)
            {
                string value = token;

                if(tree.VarExists(token))
                {
                    value = HandleVarCalling(token);
                }

                builder.Append(value);
            }

            return builder.ToString();
        }

        public void HandleStringPrint(string value)
        {
            string[] splitValue;
            StringBuilder builder = new StringBuilder();

            if (value.Contains("+"))
            {
                splitValue = value.Split(addSymbol);

                for (int i = 0; i < splitValue.Length; i++)
                {
                    splitValue[i] = splitValue[i].Trim();

                    if (tree.VarExists(splitValue[i]))
                    {
                        string newValue = HandleVarCalling(splitValue[i]) + " ";
                        newValue = newValue.RemoveQuotes();

                        builder.Append(newValue);
                    }
                    else
                    {
                        if (splitValue[i].Contains("\""))
                        {
                            string newValue = splitValue[i].RemoveQuotes();
                            builder.Append(newValue);
                        }
                    }
                }
            }
            else
            {
                if (tree.VarExists(value))
                {
                    string newValue = HandleVarCalling(value);
                    newValue = newValue.RemoveQuotes();

                    builder.Append(newValue);
                }
                else
                {
                    if (value.Contains("\""))
                    {
                        value = value.RemoveQuotes();
                        builder.Append(value);
                    }
                }
            }

            HandlePrint(builder.ToString());
        }

        private void HandlePrint(string value)
        {
            var toPrint = evaluator.Eval(value);
            Console.WriteLine(toPrint.ToString());
        }

        private string HandleVarCalling(string varName)
        {
            ///Change later
            ///Evaluate var
            ///before return

            return tree.GetVar(varName).GetValue();
        }
    }
}

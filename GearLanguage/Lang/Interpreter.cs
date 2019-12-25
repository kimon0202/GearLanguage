using System;
using System.Collections.Generic;
using System.Text;
using GearLanguage.Base_Classes;
using GearLanguage.Extensions;

namespace GearLanguage.Lang
{
    class Interpreter
    {
        private Tree tree;
        private char[] addSymbol;

        public Interpreter(Tree tree)
        {
            this.tree = tree;
            addSymbol = new char[] { '+' };
        }

        public void Run()
        {
            foreach(MethodNode metNode in tree.GetMethods())
            {
                foreach(Node node in metNode.GetNodes())
                {
                    if(node.GetName() == "print")
                    {
                        HandleStringPrint(node.GetValue());
                    }

                    if(tree.VarExists(node.GetName()))
                    {
                        tree.SetVar(node.GetName(), node.GetValue());
                    }
                }
            }
        }

        public void HandleStringPrint(string value)
        {
            string[] splitValue;
            StringBuilder builder = new StringBuilder();

            if(value.Contains("+"))
            {
                splitValue = value.Split(addSymbol);

                for(int i = 0; i < splitValue.Length; i++)
                {
                    splitValue[i] = splitValue[i].Trim();

                    if(tree.VarExists(splitValue[i]))
                    {
                        string newValue = HandleVarCalling(splitValue[i]) + " ";
                        newValue = newValue.RemoveQuotes();

                        builder.Append(newValue);
                    }else
                    {
                        if(splitValue[i].Contains("\""))
                        {
                            string newValue = splitValue[i].RemoveQuotes();
                            builder.Append(newValue);
                        }
                    }
                }
            }else
            {
                if(tree.VarExists(value))
                {
                    string newValue = HandleVarCalling(value);
                    newValue = newValue.RemoveQuotes();

                    builder.Append(newValue);
                }else 
                {
                    if(value.Contains("\""))
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
            Console.WriteLine(value);
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

﻿using System;
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
                        HandleVarSet(node);
                    }
                }
            }
        }
        public void HandleVarSet(Node node)
        {
            string[] splitValue;
            StringBuilder builder = new StringBuilder();

            if (node.GetValue().Contains("+") && !node.GetValue().Contains("++"))
            {
                splitValue = node.GetValue().Split(addSymbol);

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
                if (tree.VarExists(node.GetValue()))
                {
                    string newValue = HandleVarCalling(node.GetValue());
                    newValue = newValue.RemoveQuotes();

                    builder.Append(newValue);
                }
                else
                {
                    if (node.GetValue().Contains("\""))
                    { 
                        builder.Append(node.GetValue().RemoveQuotes());
                    } else
                    {
                        builder.Append(node.GetValue());
                    }
                }
            }
            tree.SetVar(node.GetName(), builder.ToString());
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

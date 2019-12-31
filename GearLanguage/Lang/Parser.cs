using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GearLanguage.Base_Classes;

namespace GearLanguage.Lang
{
    class Parser
    {
        private string[] tokens;
        private Tree tree;

        private Dictionary<string, int?> methods, funcs, vars;
        private char[] varsDeclarationChars;

        public Parser(string[] tokens)
        {
            this.tokens = tokens;
            tree = new Tree();

            methods = new Dictionary<string, int?>();
            vars = new Dictionary<string, int?>();
            funcs = new Dictionary<string, int?>();

            varsDeclarationChars = new char[] { '=' };
        }

        public Tree CreateTree()
        {
            bool appendToMethod = false;
            bool appendToFunc = false;

            string methodId = "";
            string funcId = "";
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "var")
                {
                    string name = "";
                    string value = "";

                    string[] tmp = tokens[i + 1].Split(varsDeclarationChars);
                    name = tmp[0].Trim();
                    value = ClearTokens(tmp[1]).Trim();

                    VariableNode node = new VariableNode(name, value, VariableType.GENERIC);
                    int? id = tree.AddToVars(node);

                    vars.Add(name, id);
                }
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Contains("#"))
                {
                    //nothing to do beacuse it is a comment
                }
                else if (tokens[i].Contains("method"))
                {
                    appendToMethod = true;
                    methodId = tokens[i].Split()[1].Trim();

                    MethodNode node = new MethodNode(methodId);
                    int? id = tree.AddToMethods(node);

                    methods.Add(methodId, id);
                }
                else if (tokens[i] == "end")
                {
                    if (appendToMethod)
                    {
                        appendToMethod = false;
                        methodId = "";
                    }

                    if (appendToFunc)
                    {
                        appendToFunc = false;
                        funcId = "";
                    }
                }
                else if (tokens[i] == "print")
                {
                    string valueToPrint = "";

                    if (tokens[i + 1].Contains("(\"") || tokens[i + 1].Contains("("))
                        valueToPrint = ClearTokens(tokens[i + 1]);

                    Node node = new Node(tokens[i], valueToPrint);

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node);
                    }else if(appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node);
                    }
                }
                else if(tokens[i].Contains("=") && tokens[i - 1] != "var" && !tokens[i].Contains("=="))
                {
                    string name = "";
                    string value = "";

                    string[] tmp = tokens[i].Split(varsDeclarationChars);
                    name = new string(tmp[0].ToCharArray().Where(c => c != '+').ToArray()).Trim();
                    value = ClearTokens(tmp[1]).Trim();

                    VariableNode var = tree.GetVar(name);
                    
                    if (tokens[i].Contains("+=") && (var.GetVarType() == VariableType.GENERIC || var.GetVarType() == VariableType.STRING))
                        value = var.GetValue() + ClearTokens(tmp[1]).Trim();

                    var = new VariableNode(name, value, VariableType.GENERIC);

                    if(appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(var.ToNode());
                    }else if(appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(var.ToNode());
                    }
                }
                else if(tokens[i].Contains("++"))
                {
                    string name = "";

                    string[] tmp = tokens[i].Split('+');
                    name = tmp[0].Trim();
                    var value = int.Parse(tree.GetVar(name).GetValue()) + 1;

                    VariableNode node = new VariableNode(name, value.ToString(), VariableType.GENERIC);

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node.ToNode());
                    }
                    else if (appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node.ToNode());
                    }
                }
                else if(tokens[i].Contains("func"))
                {
                    string funcName = tokens[i].Split()[1].Trim();
                    string paramsToken = tokens[i + 1];

                    appendToFunc = true;
                    funcId = funcName;

                    FunctionNode funcNode = new FunctionNode(funcName, paramsToken);
                    int? id = tree.AddToFuncs(funcNode);

                    funcs.Add(funcId, id);
                }
                else if(tree.FuncExists(tokens[i]))
                {
                    string paramsToken = tokens[i + 1];

                    int id = (int)funcs[tokens[i]];
                    foreach(Node node in tree.GetFunction(id).GetNodes())
                    {
                        if (appendToMethod)
                        {
                            int _id = (int)methods[methodId];
                            tree.GetMethod(_id).AddNode(node);
                        }else if(appendToFunc)
                        {
                            int _id = (int)funcs[tokens[i]];
                            tree.GetFunction(_id).AddNode(node);
                        }
                    }
                }
            }

            return tree;
        }

        private string ClearTokens(string token)
        {
            return new string(
                token.ToCharArray()
                .Where(c => c != '(')
                .Where(c => c != ')')
                .ToArray()
            );
        }
    }
}

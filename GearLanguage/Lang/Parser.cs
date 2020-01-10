using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GearLanguage.Base_Classes;

/// Todo:
/// Update the varsDeclarationChars to reflect all possible declaration symbols
/// Maybe its necessary to change the array type from char[] to string[] in order
/// to save more complex declaration symbols such as *= and /=

namespace GearLanguage.Lang
{
    /// <summary>
    /// Handles parsing tokens to a abstract node tree
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Tokens created by the lexer Tokenize function
        /// </summary>
        private string[] tokens;

        /// <summary>
        /// Abstract node tree - Contains a list of variables, methods and functions
        /// </summary>
        private Tree tree;

        /// <summary>
        /// Dictionaries that saves the subscribed method | function | variable id based on its name
        /// and position in the abstract node tree
        /// </summary>
        private Dictionary<string, int?> methods, funcs, vars;

        /// <summary>
        /// Array of chars that contains all variable declaration char combinations
        /// </summary>
        private char[] varsDeclarationChars;

        /// <summary>
        /// Constructor: Initialize ANT, ID Dictionaries, varsDeclarationSymbols
        /// and save the tokens from the lexer in a variable
        /// </summary>
        /// <param name="tokens">Tokens created by the Lexer</param>
        public Parser(string[] tokens)
        {
            this.tokens = tokens;
            tree = new Tree();

            methods = new Dictionary<string, int?>();
            vars = new Dictionary<string, int?>();
            funcs = new Dictionary<string, int?>();

            varsDeclarationChars = new char[] { '=' };
        }

        /// <summary>
        /// Creates the ANT based on the tokens array order
        /// </summary>
        /// <returns>New instance of ANT</returns>
        public Tree CreateTree()
        {
            ///Control variables to check current nesting state
            bool appendToMethod = false; ///method nesting
            bool appendToFunc = false; ///function nesting

            string methodId = ""; ///current method name
            string funcId = ""; ///current function name

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Contains("#")) ///# declares a comment in gear code
                {
                    ///nothing to do beacuse it is a comment
                }
                else if (tokens[i].Contains("method")) ///method keyword identifies a new method
                {
                    appendToMethod = true; ///set nesting state to method nesting
                    methodId = tokens[i].Split()[1].Trim(); ///gets the method name from the current token

                    MethodNode node = new MethodNode(methodId); ///Creates a new Abstract Method Node
                    int? id = tree.AddToMethods(node); ///Sets the method id based on mthos position on ANT

                    methods.Add(methodId, id); ///Adds method to dictinary for faster uses on the future
                }
                else if (tokens[i] == "end") ///resets nesting state
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
                else if (tokens[i] == "print") ///outputs a string on the console
                {
                    string valueToPrint = "";

                    if (tokens[i + 1].Contains("(\"") || tokens[i + 1].Contains("("))
                        valueToPrint = ClearTokens(tokens[i + 1]);

                    ///Creates a new abstract node that holds the command(print) and the value to print
                    Node node = new Node(tokens[i], valueToPrint);

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node);
                    }
                    else if (appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node);
                    }
                }
                else if (tokens[i] == "var") ///decalres a new variable
                {
                    string name = "";
                    string value = "";

                    string[] tmp = tokens[i + 1].Split(varsDeclarationChars);
                    name = tmp[0].Trim(); ///gets var name from next token
                    value = ClearTokens(tmp[1]).Trim(); ///gets var value from next token

                    ///Creates a new abstract node that holds the var name, value and type(not in use yet)
                    VariableNode node = new VariableNode(name, value, VariableType.GENERIC);
                    int? id = tree.AddToVars(node);

                    vars.Add(name, id);
                }
                ///Checks if variable is being changed by a logic operator
                else if (tokens[i].Contains("=") && tokens[i - 1] != "var" && (!tokens[i].Contains("==") || !tokens[i].Contains("!=") || !tokens[i].Contains(">=") || !tokens[i].Contains("<=")))
                {
                    string name = "";
                    string value = "";
                    string[] carryAction = new string[2] { null, null };

                    string[] tmp = tokens[i].Split(varsDeclarationChars);
                    name = new string(tmp[0].ToCharArray()
                        .Where(c => c != '+')
                        .Where(c => c != '-')
                        .Where(c => c != '*')
                        .Where(c => c != '/')
                        .ToArray()
                    ).Trim();
                    value = ClearTokens(tmp[1]).Trim();

                    if (value.Contains("input"))
                    {
                        string valueToPrint = "";

                        string[] tm = tokens[i + 1].Split(")");

                        tm[0] += ")";

                        if (tokens[i + 1].Contains(@"(""") || tokens[i + 1].Contains("(")) valueToPrint = tm[0];

                        carryAction[1] = valueToPrint;
                        value += tm[1];
                    }

                    VariableNode var = new VariableNode(name, value, VariableType.GENERIC);

                    if (tokens[i].Contains("+=")) carryAction[0] = "+=";
                    else if (tokens[i].Contains("-=")) carryAction[0] = "-=";
                    else if (tokens[i].Contains("*=")) carryAction[0] = "*=";
                    else if (tokens[i].Contains("/=")) carryAction[0] = "/=";
                    else carryAction[0] = null;

                    Node node = new Node(var.GetName(), var.GetValue(), carryAction);

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node);
                    }
                    else if (appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node);
                    }
                }
                else if (tokens[i].Contains("++"))
                {
                    string name = "";

                    string[] tmp = tokens[i].Split('+');
                    name = tmp[0].Trim();
                    var value = "";

                    VariableNode varNode = new VariableNode(name, value.ToString(), VariableType.GENERIC);
                    Node node = new Node(varNode.GetName(), varNode.GetValue(), new string[1] { "++" });

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node);
                    }
                    else if (appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node);
                    }
                }
                else if (tokens[i].Contains("--"))
                {
                    string name = "";

                    string[] tmp = tokens[i].Split('-');
                    name = tmp[0].Trim();
                    var value = "";

                    VariableNode varNode = new VariableNode(name, value.ToString(), VariableType.GENERIC);
                    Node node = new Node(varNode.GetName(), varNode.GetValue(), new string[1] { "--" });

                    if (appendToMethod)
                    {
                        int id = (int)methods[methodId];
                        tree.GetMethod(id).AddNode(node);
                    }
                    else if (appendToFunc)
                    {
                        int id = (int)funcs[funcId];
                        tree.GetFunction(id).AddNode(node);
                    }
                }
                else if (tokens[i].Contains("func")) ///declares a new function
                {
                    string funcName = tokens[i].Split()[1].Trim();
                    string paramsToken = tokens[i + 1];

                    appendToFunc = true;
                    funcId = funcName;

                    FunctionNode funcNode = new FunctionNode(funcName, paramsToken);
                    int? id = tree.AddToFuncs(funcNode);

                    funcs.Add(funcId, id);
                }
                else if (tree.FuncExists(tokens[i])) ///calls the function from its numeric id saved on the dict
                {
                    string paramsToken = tokens[i + 1];

                    int id = (int)funcs[tokens[i]];
                    foreach (Node node in tree.GetFunction(id).GetNodes())
                    {
                        if (appendToMethod)
                        {
                            int _id = (int)methods[methodId];
                            tree.GetMethod(_id).AddNode(node);
                        }
                        else if (appendToFunc)
                        {
                            int _id = (int)funcs[tokens[i]];
                            tree.GetFunction(_id).AddNode(node);
                        }
                    }
                }
            }

            return tree;
        }

        /// <summary>
        /// Clear tokens by removing '(' and ')' chars
        /// </summary>
        /// <param name="token">Token to clean</param>
        /// <returns>Cleaned string</returns>
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

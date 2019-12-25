using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    class Tree
    {
        private List<MethodNode> methods;
        private List<FunctionNode> funcs;
        private List<VariableNode> vars;

        public Tree()
        {
            methods = new List<MethodNode>();
            funcs = new List<FunctionNode>();
            vars = new List<VariableNode>();
        }

        public MethodNode[] GetMethods()
        {
            return methods.ToArray();
        }

        public MethodNode GetMethod(int methodId)
        {
            return methods[methodId];
        }

        public FunctionNode[] GetFunctions()
        {
            return funcs.ToArray();
        }

        public FunctionNode GetFunction(int funcId)
        {
            return funcs[funcId];
        }

        public bool FuncExists(string funcName)
        {
            foreach (FunctionNode func in funcs)
            {
                if (func.GetName() == funcName)
                    return true;
            }

            return false;
        }

        public VariableNode[] GetVars()
        {
            return vars.ToArray();
        }

        public VariableNode GetVar(int varId)
        {
            return vars[varId];
        }

        public VariableNode GetVar(string varName)
        {
            foreach(VariableNode var in vars)
            {
                if (var.GetName() == varName)
                    return var;
            }

            return null;
        }

        public bool VarExists(string varName)
        {
            foreach(VariableNode var in vars)
            {
                if (var.GetName() == varName)
                    return true;
            }

            return false;
        }

        public void SetVar(string varName, string value)
        {
            foreach (VariableNode var in vars)
            {
                if (var.GetName() == varName)
                    var.SetValue(value);
            }
        }

        public int? AddToMethods(MethodNode node)
        {
            methods.Add(node);

            for(int i = 0; i < methods.Count; i++)
            {
                if (methods[i] == node) return i;
            }

            return null;
        }

        public int? AddToFuncs(FunctionNode node)
        {
            funcs.Add(node);

            for (int i = 0; i < funcs.Count; i++)
            {
                if (funcs[i] == node) return i;
            }

            return null;
        }

        public int? AddToVars(VariableNode node)
        {
            vars.Add(node);

            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i] == node) return i;
            }

            return null;
        }
    }
}

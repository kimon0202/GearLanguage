using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    class FunctionNode
    {
        private string name;
        private string paramsToken;
        private List<Node> nodes;

        private int? registryID;

        public FunctionNode(string name, string paramsToken)
        {
            this.name = name;
            this.paramsToken = paramsToken;

            nodes = new List<Node>();
        }

        public FunctionNode(string name)
        {
            this.name = name;
            nodes = new List<Node>();
        }

        public void SetRegistryID(int? ID)
        {
            registryID = ID;
        }

        public void SetRegistryID(int ID)
        {
            registryID = ID;
        }

        public int? GetRegistryID()
        {
            return registryID;
        }

        public void SetParamsToken(string paramsToken)
        {
            this.paramsToken = paramsToken;
        }

        public string GetParams()
        {
            return paramsToken;
        }

        public string GetName()
        {
            return name;
        }

        public Node[] GetNodes()
        {
            return nodes.ToArray();
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
        }
    }
}

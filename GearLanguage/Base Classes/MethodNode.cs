using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    class MethodNode
    {
        private string name;
        private List<Node> nodes;

        public MethodNode(string name)
        {
            this.name = name;
            nodes = new List<Node>();
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

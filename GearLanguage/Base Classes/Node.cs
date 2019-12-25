using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    class Node
    {
        private string name;
        private string value;

        public Node(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public Node(string name)
        {
            this.name = name;
            value = "";
        }

        public string GetName()
        {
            return name;
        }

        public string GetValue()
        {
            return value;
        }
    }
}

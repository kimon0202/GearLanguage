using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    public enum VariableType
    {
        INT,
        FLOAT,
        STRING,
        BOOL,
        GENERIC
    }

    class VariableNode
    {
        private string name;
        private string value;
        private VariableType type;
         

        public VariableNode(string name, string value, VariableType type)
        {
            this.name = name;
            this.value = value;
            this.type = type;
        }

        public VariableNode(string name, VariableType type)
        {
            this.name = name;
            this.type = type;
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

        public void SetValue(string value)
        {
            this.value = value;
        }

        public VariableType GetVarType()
        {
            return type;
        }

        public Node ToNode()
        {
            return new Node(name, value);
        }
    }
}

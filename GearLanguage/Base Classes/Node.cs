using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    class Node
    {
        private string name;
        private string value;
        private string? carryAction;

        public Node(string name, string value, string? carryAction)
        {
            this.name = name;
            this.value = value;
            this.carryAction = carryAction;
        }

        public Node(string name, string value)
        {
            this.name = name;
            this.value = value;
            carryAction = null;
        }

        public Node(string name)
        {
            this.name = name;
            value = "";
            carryAction = null;
        }

        public string GetName()
        {
            return name;
        }

        public string GetValue()
        {
            return value;
        }

        public string GetCarryAction()
        {
            return carryAction;
        }
    }
}

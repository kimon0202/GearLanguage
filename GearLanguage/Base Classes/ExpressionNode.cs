using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Base_Classes
{
    public enum ExpressionType
    {
        Number,
        String,
        Variable,
        Operator
    }

    class ExpressionNode
    {
        private ExpressionType type;
        private string value;

        public ExpressionNode(ExpressionType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public string GetValue()
        {
            return value;
        }

        public ExpressionType GetExpressionType()
        {
            return type;
        }
    }
}

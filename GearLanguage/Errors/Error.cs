using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Errors
{
    class Error
    {
        private string name;
        private string body;
        private string id;
        private string type;

        public Error(string name, string body, string id, string type)
        {
            this.name = name;
            this.body = body;
            this.id = id;
            this.type = type;
        }

        public string GetName()
        {
            return name;
        }

        public string GetBody()
        {
            return body;
        }

        public string GetId()
        {
            return id;
        }

        public string GetErrorType()
        {
            return type;
        }
    }
}

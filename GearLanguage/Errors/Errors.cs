using System;
using System.Collections.Generic;
using System.Text;

namespace GearLanguage.Errors
{
    class ErrorsList
    {
        public static class CLI
        {
            public static Error fileNotFound = new Error(
                name: "File Not Found",
                body: "No file with the provided name was found",
                id: "0000",
                type: "CLI"
            );

            public static Error fileNameNotProvided = new Error(
                name: "File name was blank",
                body: "No file name was provided",
                id: "0001",
                type: "CLI"
            );
        }

        public static class Runtime
        {
            public static Error variableDoesNotExist = new Error(
                name: "Variable does not exist",
                body: "The used variable does not exist",
                id: "0000",
                type: "Runtime"
            );
        }
    }
}

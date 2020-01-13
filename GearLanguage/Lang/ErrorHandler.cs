using System;
using System.Collections.Generic;
using System.Text;
using GearLanguage.Utils;
using GearLanguage.Errors;

namespace GearLanguage.Lang
{
    /// <summary>
    /// Handles catching errors and outputing the error code in the console
    /// </summary>
    class ErrorHandler
    {
        private ConsoleColor defaultErrorColor = Colors.error;

        public ErrorHandler() { }

        public void LogError(string error)
        {
            Console.ForegroundColor = defaultErrorColor;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public void LogError(Error error)
        {
            InitializeColors();
            Console.WriteLine(error.GetErrorType() + " " + error.GetId() + ": " + error.GetName());
            Console.WriteLine(error.GetBody());
            ResetColors();
        }

        private void InitializeColors()
        {
            Console.ForegroundColor = defaultErrorColor;
        }

        private void ResetColors()
        {
            Console.ResetColor();
        }
    }
}

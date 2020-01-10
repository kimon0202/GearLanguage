using System;
using System.Collections.Generic;
using System.Text;
using GearLanguage.Utils;

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
    }
}

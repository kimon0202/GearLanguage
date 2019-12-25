using System.Text;
using GearLanguage.Lang;
using GearLanguage.Base_Classes;
using System.IO;
using System;

namespace GearLanguage
{
    class Program
    {
        static string fileName = "varSet.gear";
        //cahnge this to reflect your pc
        static string testFilesPath = "C:\\Users\\gusta\\Desktop\\Workspace\\GearLanguage\\Examples";

        static Lexer lexer;
        static Parser parser;
        static Interpreter interpreter;

        static Tree tree;
        static string[] tokens;

        static void Main(string[] args)
        {
            string data = File.ReadAllText(testFilesPath + "/" + fileName, Encoding.UTF8);
            //Console.WriteLine(data);

            lexer = new Lexer(data);
            tokens = lexer.Tokenize();

            parser = new Parser(tokens);
            tree = parser.CreateTree();

            interpreter = new Interpreter(tree);
            interpreter.Run();
        }
    }
}

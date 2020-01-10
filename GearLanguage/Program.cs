﻿using System.Text;
using GearLanguage.Lang;
using GearLanguage.Base_Classes;
using System.IO;
using System;

//C:\\Users\\gusta\\Desktop\\Workspace\\GearLanguage\\Examples

namespace GearLanguage
{
    class Program
    {
        //static string fileName = "input.gear";
        //cahnge this to reflect your pc
        //static string testFilesPath = "C:\\Users\\gusta\\Desktop\\Workspace\\GearLanguage\\Examples";
        //static string testFilesPath = @"E:\modl\GearLanguage\Examples";

        static Lexer lexer;
        static Parser parser;
        static Interpreter interpreter;
        static ErrorHandler errorHandler;

        static Tree tree;
        static string[] _tokens;
        static string[] tokens;
        static string data = "";

        static void Main(string[] args)
        {
            errorHandler = new ErrorHandler();
            //string data = File.ReadAllText(testFilesPath + "/" + fileName, Encoding.UTF8);
            //Console.WriteLine(data);
            //string file = args[0];
            //string data = File.ReadAllText(file, Encoding.UTF8);

            //string pathEnv = Environment.GetEnvironmentVariable("Path");
            if(args.Length == 0)
            {
                errorHandler.LogError("Error: Bad command -> File name can't be null!");
                return;
            }

            string curPath = Environment.CurrentDirectory;
            string fileName = args[0];

            if(fileName.Contains(":\\"))
            {
                data = File.ReadAllText(fileName, Encoding.UTF8);
            }else
            {
                string[] filesInCurDir = Directory.GetFiles(curPath);
                foreach (string file in filesInCurDir)
                {
                    if (file.Contains(fileName))
                    {
                        data = File.ReadAllText(file, Encoding.UTF8);
                        break;
                    }
                }
            }

            if (data != null)
            {
                lexer = new Lexer(data);
                _tokens = lexer.TokenizeTest();
                tokens = lexer.Tokenize(_tokens);

                /*foreach(string token in tokens)
                {
                    Console.WriteLine(token);
                }*/

                parser = new Parser(tokens);
                tree = parser.CreateTree();

                interpreter = new Interpreter(tree);
                interpreter.Run();
            }
            else
            {
                errorHandler.LogError("Error: File not found -> " + fileName + " don't exist in " + curPath);
                return;
            }
        }
    }
}

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ANTLR_COOL_Program;
using System.IO;
using COOL_Compiler.SymbolTableCreation;

namespace COOL_Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            AntlrInputStream inputStream = new AntlrInputStream(Console.In);
            COOLLexer lexer = new COOLLexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            COOLParser parser = new COOLParser(tokenStream);
            IParseTree parseTree = parser.program();

            ParseTreeWalker walker = new ParseTreeWalker();
            COOLAnalyseListener coolListener = new COOLAnalyseListener();
            //symbol tables building phase
            coolListener.phase = COOLAnalyseListener.Phase.Building;
            Console.WriteLine("Symbol Table Building Phase:============================================");
            walker.Walk(coolListener, parseTree);
            //symbol tables checking phase
            coolListener.phase = COOLAnalyseListener.Phase.Checking;
            Console.WriteLine("Symbol Table Checking Phase:============================================");
            walker.Walk(coolListener, parseTree);

            Console.ReadLine();
        }
    }
}

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ANTLR_COOL_Program;
using System.IO;
using COOL_Compiling.SymbolTableCreation;

namespace COOL_Compiling
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
            COOLCompileListener coolListener = new COOLCompileListener();
            //symbol tables building phase
            coolListener.phase = COOLCompileListener.Phase.Building;
            Console.WriteLine("Symbol Table Building Phase:============================================");
            walker.Walk(coolListener, parseTree);
            //symbol tables checking phase
            coolListener.phase = COOLCompileListener.Phase.Checking;
            Console.WriteLine("Symbol Table Checking Phase:============================================");
            walker.Walk(coolListener, parseTree);

            Console.ReadLine();
        }
    }
}

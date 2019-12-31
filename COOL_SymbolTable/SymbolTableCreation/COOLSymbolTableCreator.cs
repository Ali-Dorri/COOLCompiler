using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace COOL_Compiler.SymbolTableCreation
{
    public class COOLSymbolTableCreator : COOLBaseListener
    {
        
        public Stack<SymbolTable> depthTables;
        COOLAnalyseListener listener;

        public override void EnterAdd([NotNull] COOLParser.AddContext context)
        {
            if(listener.phase == COOLAnalyseListener.Phase.Building)
            {

            }
            else
            {

            }
        }
    }
}

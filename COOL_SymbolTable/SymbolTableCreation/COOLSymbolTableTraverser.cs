using System;
using System.Collections.Generic;
using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;

namespace COOL_Compiler.SymbolTableCreation
{
    class COOLSymbolTableTraverser : COOLBaseListener
    {
        COOLAnalyseListener listener;
        Stack<int> traverserChildsCounts = new Stack<int>();

        public override void EnterMethod([NotNull] COOLParser.MethodContext context)
        {
            //Method is scope rule so enter new scope
            EnterScope();

            //context.

        }

        void EnterScope()
        {
            listener.symbolTableCreator.depthTables.Push(listener.symbolTableCreator.depthTables[traverserChildsCounts.Peek()]);

            //update parent table traverser childs count
            int newCount = traverserChildsCounts.Pop() + 1;
            traverserChildsCounts.Push(newCount);

            //add child table's traversed childs count
            traverserChildsCounts.Push(0);
        }

        void ExitScope()
        {

        }
    }
}

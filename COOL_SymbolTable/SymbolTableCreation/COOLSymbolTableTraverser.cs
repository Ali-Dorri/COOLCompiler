using System;
using System.Collections.Generic;
using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        private class COOLSymbolTableTraverser : COOLCompileAspectListener
        {
            CustomStack<int> traverserChildsCounts = new CustomStack<int>();

            public COOLSymbolTableTraverser(COOLCompileListener listener) : base(listener) { }

            public override void EnterMethod([NotNull] COOLParser.MethodContext context)
            {
                CheckEnterScope();
            }

            public override void ExitMethod([NotNull] COOLParser.MethodContext context)
            {
                CheckExitScope();
            }

            void CheckEnterScope()
            {
                if (listener.isScopeRule)
                {
                    listener.depthTables.Push(listener.depthTables.Peek().childs[traverserChildsCounts.Peek()]);
                    //update parent table traverser childs count
                    traverserChildsCounts[traverserChildsCounts.Count - 1] = traverserChildsCounts.Peek() + 1;
                    //add child table's traversed childs count
                    traverserChildsCounts.Push(0);
                }
            }

            void CheckExitScope()
            {
                if (listener.isScopeRule)
                {
                    listener.depthTables.Pop();
                    traverserChildsCounts.Pop();
                }
            }
        }
    }
}

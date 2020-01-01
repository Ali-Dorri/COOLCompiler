using System;
using System.Collections.Generic;
using ANTLR_COOL_Program;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        /// <summary>
        /// Push and pop proper child symbolTables if entering or exiting a new Scope
        /// </summary>
        private class COOLSymbolTableTraverser : COOLCompileAspectListener
        {
            CustomStack<int> traverserChildsCounts = new CustomStack<int>();

            public COOLSymbolTableTraverser(COOLCompileListener listener) : base(listener) { }

            //TODO add check enter and exit scope for all rule methods

            public override void EnterMethod([NotNull] COOLParser.MethodContext context)
            {
                CheckEnterScope(context);
            }

            public override void ExitMethod([NotNull] COOLParser.MethodContext context)
            {
                CheckExitScope(context);
            }

            void CheckEnterScope(ParserRuleContext context)
            {
                if (IsScopeRule(context))
                {
                    listener.depthTables.Push(listener.depthTables.Peek().childs[traverserChildsCounts.Peek()]);
                    //update parent table traverser childs count
                    traverserChildsCounts[traverserChildsCounts.Count - 1] = traverserChildsCounts.Peek() + 1;
                    //add child table's traversed childs count
                    traverserChildsCounts.Push(0);
                }
            }

            void CheckExitScope(ParserRuleContext context)
            {
                if (IsScopeRule(context))
                {
                    listener.depthTables.Pop();
                    traverserChildsCounts.Pop();
                }
            }
        }
    }
}

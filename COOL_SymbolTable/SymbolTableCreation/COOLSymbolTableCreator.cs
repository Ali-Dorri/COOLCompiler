using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        /// <summary>
        /// Create symboltable and check redefining same symbols
        /// </summary>
        private class COOLSymbolTableCreator : COOLCompileAspectListener
        {
            //TODO add and remove symbol tables for all scope rule methods
            //TODO add symbols to symbolTable in stack's top in all rule methods

            public COOLSymbolTableCreator(COOLCompileListener listener) : base(listener) { }

            public void CheckSymbolTablesErrors()
            {
                
            }

            public override void EnterProgram([NotNull] COOLParser.ProgramContext context)
            {
                base.EnterProgram(context);

                listener.rootSymbolTable = new SymbolTable(context.ToString());
                listener.depthTables.Push(listener.rootSymbolTable);

                //context.programBlocks()
            }

            public override void EnterMethod([NotNull] COOLParser.MethodContext context)
            {
                base.EnterMethod(context);
                if (IsScopeRule(context))
                {
                    SymbolTable parent = listener.depthTables.Peek();
                    SymbolTable child = new SymbolTable(context.ToString());
                    listener.depthTables.Push(child);
                    parent.childs.Add(child);
                }

                //add current rule symbols

            }


        }
    }
}

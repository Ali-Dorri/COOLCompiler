using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        private class COOLSymbolTableCreator : COOLCompileAspectListener
        {
            public COOLSymbolTableCreator(COOLCompileListener listener) : base(listener) { }

            public override void EnterMethod([NotNull] COOLParser.MethodContext context)
            {
                base.EnterMethod(context);
                if (listener.isScopeRule)
                {
                    SymbolTable parent = listener.depthTables.Peek();
                    SymbolTable child = new SymbolTable("sdfs", 0);
                    listener.depthTables.Push(child);
                    parent.childs.Add(child);
                }
            }
        }
    }
}

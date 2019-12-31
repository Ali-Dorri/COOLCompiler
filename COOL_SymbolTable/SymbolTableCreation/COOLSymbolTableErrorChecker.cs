using ANTLR_COOL_Program;
using System;
using System.Collections.Generic;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        private class COOLSymbolTableErrorChecker : COOLCompileAspectListener
        {
            public COOLSymbolTableErrorChecker(COOLCompileListener listener) : base(listener) { }
        }
    }
}

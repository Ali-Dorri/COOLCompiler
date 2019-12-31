using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        private class COOLCompileAspectListener : COOLBaseListener
        {
            protected COOLCompileListener listener;

            public COOLCompileAspectListener(COOLCompileListener listener)
            {
                this.listener = listener;
            }
        }
    }
}

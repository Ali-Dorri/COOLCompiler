using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener
    {
        private class COOLSemanticErrorChecker : COOLCompileAspectListener
        {
            public COOLSemanticErrorChecker(COOLCompileListener listener) : base(listener) { }
        }
    }
}

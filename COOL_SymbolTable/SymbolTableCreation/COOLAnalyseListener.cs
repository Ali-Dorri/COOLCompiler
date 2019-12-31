using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace COOL_Compiler.SymbolTableCreation
{
    public class COOLAnalyseListener : COOLBaseListener
    {
        public enum Phase { Building, Checking }

        public Phase phase = Phase.Building;
        public COOLSymbolTableCreator symbolTableCreator;
        //symbol table traverser
        //symbol table error checker
        //semantic error checker
        //protected SymbolTable CurrentScopeTable => symbolTableCreator.depthTables.Peek();   //all COOLListener here use this to access the current symbolTable

        public override void EnterMethod([NotNull] COOLParser.MethodContext context)
        {
            base.EnterMethod(context);
            if(phase == Phase.Building)
            {
                symbolTableCreator.EnterMethod(context);    //also check redefining same symbols
            }
            else
            {
                //symbolTableTraverser.EnterMethod(context);    //push the proper child symbolTable if entering a new Scope
                //symbolTableErrorChecker.EnterMethod(context); //remove extra symbol in child tables(if exists in parent) and throws error if using undefined symbol
                //semanticErrorChecker.EnterMethod(context);    //check semantic errors
            }
        }
    }
}

using ANTLR_COOL_Program;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener : COOLBaseListener
    {
        public enum Phase { Building, Checking }

        public Phase phase = Phase.Building;
        CustomStack<SymbolTable> depthTables = new CustomStack<SymbolTable>();
        bool isScopeRule = false;
        COOLSymbolTableCreator symbolTableCreator;
        COOLSymbolTableTraverser symbolTableTraverser;
        COOLSymbolTableErrorChecker symbolTableErrorChecker;
        COOLSemanticErrorChecker semanticErrorChecker;

        public COOLCompileListener()
        {
            symbolTableCreator = new COOLSymbolTableCreator(this);
            symbolTableTraverser = new COOLSymbolTableTraverser(this);
            symbolTableErrorChecker = new COOLSymbolTableErrorChecker(this);
            semanticErrorChecker = new COOLSemanticErrorChecker(this);
        }

        /// <summary>
        /// Current scope symbol table
        /// </summary>
        SymbolTable ScopeSymbolTable
        {
            get
            {
                return depthTables.Peek();
            }
        }

        SymbolTable GetSymbolTableParent(int parentLevel)
        {
            return depthTables[depthTables.Count - 1 - parentLevel];
        }

        bool SymbolTableParentExists(int parentLevel)
        {
            return depthTables.Count - 1 - parentLevel >= 0;
        }

        public override void EnterProgram([NotNull] COOLParser.ProgramContext context)
        {
            base.EnterProgram(context);
            depthTables.Clear();
        }

        public override void EnterMethod([NotNull] COOLParser.MethodContext context)
        {
            base.EnterMethod(context);
            isScopeRule = true;     //all methods should define if it is scopre rule (set isScopeRule to true or false)
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterMethod(context);    //also check redefining same symbols
            }
            else
            {
                symbolTableTraverser.EnterMethod(context);    //push the proper child symbolTable if entering a new Scope
                symbolTableErrorChecker.EnterMethod(context); //remove extra symbol in child tables(if exists in parent) and throws error if using undefined symbol
                semanticErrorChecker.EnterMethod(context);    //check semantic errors
            }
        }
    }
}

using ANTLR_COOL_Program;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using static ANTLR_COOL_Program.COOLParser;

namespace COOL_Compiling.SymbolTableCreation
{
    public partial class COOLCompileListener : COOLBaseListener
    {
        //TODO add all method rules
        public enum Phase { Building, Checking }

        public bool logProcess = false;
        public Phase phase = Phase.Building;
        CustomStack<SymbolTable> depthTables = new CustomStack<SymbolTable>();
        SymbolTable rootSymbolTable;
        static HashSet<Type> scopeRules;
        COOLSymbolTableCreator symbolTableCreator;
        COOLSymbolTableTraverser symbolTableTraverser;
        COOLSemanticErrorChecker semanticErrorChecker;

        static COOLCompileListener()
        {
            scopeRules = new HashSet<Type>();
            //TODO: add scope rules
            scopeRules.Add(typeof(ProgramContext));
        }

        public COOLCompileListener()
        {
            symbolTableCreator = new COOLSymbolTableCreator(this);
            symbolTableTraverser = new COOLSymbolTableTraverser(this);
            semanticErrorChecker = new COOLSemanticErrorChecker(this);
        }

        /// <summary>
        /// Current scope symbol table
        /// </summary>
        SymbolTable CurrentSymbolTable
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

        public static bool IsScopeRule<T>() where T : ParserRuleContext
        {
            return scopeRules.Contains(typeof(T));
        }

        public static bool IsScopeRule(ParserRuleContext context)
        {
            return scopeRules.Contains(context.GetType());
        }

        void Log(string message)
        {
            if (logProcess)
            {
                Console.WriteLine(message);
            }
        }

        public override void EnterProgram([NotNull] COOLParser.ProgramContext context)
        {
            base.EnterProgram(context);
            depthTables.Clear();
            Log("enter Program");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterProgram(context);
            }
            else
            {
                depthTables.Push(rootSymbolTable);
                symbolTableCreator.CheckSymbolTablesErrors();   //travers symbol tables hierarchy and check errors
                symbolTableTraverser.EnterProgram(context);
                semanticErrorChecker.EnterProgram(context);
            }
        }

        public override void ExitProgram([NotNull] COOLParser.ProgramContext context)
        {
            base.ExitProgram(context);
            Log("exit Program");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitProgram(context);
            }
            else
            {
                symbolTableTraverser.ExitProgram(context);
                semanticErrorChecker.ExitProgram(context);
            }
        }

        public override void EnterClasses(ClassesContext context)
        {
            base.EnterClasses(context);
            Log("enter Classes");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterClasses(context);
            }
            else
            {
                symbolTableTraverser.EnterClasses(context);
                semanticErrorChecker.EnterClasses(context);
            }
        }

        public override void ExitClasses(ClassesContext context)
        {
            base.ExitClasses(context);
            Log("exit Classes");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitClasses(context);
            }
            else
            {
                symbolTableTraverser.ExitClasses(context);
                semanticErrorChecker.ExitClasses(context);
            }
        }

        public override void EnterMethod([NotNull] COOLParser.MethodContext context)
        {
            base.EnterMethod(context);
            Log("enter Method");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterMethod(context);
            }
            else
            {
                symbolTableTraverser.EnterMethod(context);
                semanticErrorChecker.EnterMethod(context);
            }
        }

        public override void ExitMethod([NotNull] MethodContext context)
        {
            base.ExitMethod(context);
            Log("exit Method");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitMethod(context);
            }
            else
            {
                symbolTableTraverser.ExitMethod(context);
                semanticErrorChecker.ExitMethod(context);
            }
        }

        public override void EnterEof([NotNull] EofContext context)
        {
            base.EnterEof(context);
            Log("enter EOF");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterEof(context);
            }
            else
            {
                symbolTableTraverser.EnterEof(context);
                semanticErrorChecker.EnterEof(context);
            }
        }

        public override void ExitEof([NotNull] EofContext context)
        {
            base.ExitEof(context);
            Log("exit EOF");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitEof(context);
            }
            else
            {
                symbolTableTraverser.ExitEof(context);
                semanticErrorChecker.ExitEof(context);
            }
        }

        public override void EnterClassDefine([NotNull] ClassDefineContext context)
        {
            base.EnterClassDefine(context);
            Log("EnterClassDefine");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterClassDefine(context);
            }
            else
            {
                symbolTableTraverser.EnterClassDefine(context);
                semanticErrorChecker.EnterClassDefine(context);
            }
        }

        public override void ExitClassDefine([NotNull] ClassDefineContext context)
        {
            base.ExitClassDefine(context);
            Log("ExitClassDefine");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitClassDefine(context);
            }
            else
            {
                symbolTableTraverser.ExitClassDefine(context);
                semanticErrorChecker.ExitClassDefine(context);
            }
        }

        public override void EnterProperty([NotNull] PropertyContext context)
        {
            base.EnterProperty(context);
            Log("EnterProperty");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterProperty(context);
            }
            else
            {
                symbolTableTraverser.EnterProperty(context);
                semanticErrorChecker.EnterProperty(context);
            }
        }

        public override void ExitProperty([NotNull] PropertyContext context)
        {
            base.ExitProperty(context);
            Log("ExitProperty");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitProperty(context);
            }
            else
            {
                symbolTableTraverser.ExitProperty(context);
                semanticErrorChecker.ExitProperty(context);
            }
        }

        public override void EnterFormal([NotNull] FormalContext context)
        {
            base.EnterFormal(context);
            Log("EnterFormal");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterFormal(context);
            }
            else
            {
                symbolTableTraverser.EnterFormal(context);
                semanticErrorChecker.EnterFormal(context);
            }
        }

        public override void ExitFormal([NotNull] FormalContext context)
        {
            base.ExitFormal(context);
            Log("ExitFormal");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitFormal(context);
            }
            else
            {
                symbolTableTraverser.ExitFormal(context);
                semanticErrorChecker.ExitFormal(context);
            }
        }

        public override void EnterLetIn([NotNull] LetInContext context)
        {
            base.EnterLetIn(context);
            Log("EnterLetIn");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterLetIn(context);
            }
            else
            {
                symbolTableTraverser.EnterLetIn(context);
                semanticErrorChecker.EnterLetIn(context);
            }
        }

        public override void ExitLetIn([NotNull] LetInContext context)
        {
            base.ExitLetIn(context);
            Log("ExitLetIn");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitLetIn(context);
            }
            else
            {
                symbolTableTraverser.ExitLetIn(context);
                semanticErrorChecker.ExitLetIn(context);
            }
        }

        public override void EnterMinus([NotNull] MinusContext context)
        {
            base.EnterMinus(context);
            Log("EnterMinus");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterMinus(context);
            }
            else
            {
                symbolTableTraverser.EnterMinus(context);
                semanticErrorChecker.EnterMinus(context);
            }
        }

        public override void ExitMinus([NotNull] MinusContext context)
        {
            base.ExitMinus(context);
            Log("ExitMinus");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitMinus(context);
            }
            else
            {
                symbolTableTraverser.ExitMinus(context);
                semanticErrorChecker.ExitMinus(context);
            }
        }

        public override void EnterString([NotNull] StringContext context)
        {
            base.EnterString(context);
            Log("EnterString");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterString(context);
            }
            else
            {
                symbolTableTraverser.EnterString(context);
                semanticErrorChecker.EnterString(context);
            }
        }

        public override void ExitString([NotNull] StringContext context)
        {
            base.ExitString(context);
            Log("ExitString");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitString(context);
            }
            else
            {
                symbolTableTraverser.ExitString(context);
                semanticErrorChecker.ExitString(context);
            }
        }

        public override void EnterIsvoid([NotNull] IsvoidContext context)
        {
            base.EnterIsvoid(context);
            Log("EnterIsvoid");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterIsvoid(context);
            }
            else
            {
                symbolTableTraverser.EnterIsvoid(context);
                semanticErrorChecker.EnterIsvoid(context);
            }
        }

        public override void ExitIsvoid([NotNull] IsvoidContext context)
        {
            base.ExitIsvoid(context);
            Log("ExitIsvoid");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitIsvoid(context);
            }
            else
            {
                symbolTableTraverser.ExitIsvoid(context);
                semanticErrorChecker.ExitIsvoid(context);
            }
        }

        public override void EnterWhile([NotNull] WhileContext context)
        {
            base.EnterWhile(context);
            Log("EnterWhile");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterWhile(context);
            }
            else
            {
                symbolTableTraverser.EnterWhile(context);
                semanticErrorChecker.EnterWhile(context);
            }
        }

        public override void ExitWhile([NotNull] WhileContext context)
        {
            base.ExitWhile(context);
            Log("ExitWhile");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitWhile(context);
            }
            else
            {
                symbolTableTraverser.ExitWhile(context);
                semanticErrorChecker.ExitWhile(context);
            }
        }

        public override void EnterDivision([NotNull] DivisionContext context)
        {
            base.EnterDivision(context);
            Log("EnterDivision");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterDivision(context);
            }
            else
            {
                symbolTableTraverser.EnterDivision(context);
                semanticErrorChecker.EnterDivision(context);
            }
        }

        public override void ExitDivision([NotNull] DivisionContext context)
        {
            base.ExitDivision(context);
            Log("ExitDivision");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitDivision(context);
            }
            else
            {
                symbolTableTraverser.ExitDivision(context);
                semanticErrorChecker.ExitDivision(context);
            }
        }

        public override void EnterNegative([NotNull] NegativeContext context)
        {
            base.EnterNegative(context);
            Log("EnterNegative");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterNegative(context);
            }
            else
            {
                symbolTableTraverser.EnterNegative(context);
                semanticErrorChecker.EnterNegative(context);
            }
        }

        public override void ExitNegative([NotNull] NegativeContext context)
        {
            base.ExitNegative(context);
            Log("ExitNegative");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitNegative(context);
            }
            else
            {
                symbolTableTraverser.ExitNegative(context);
                semanticErrorChecker.ExitNegative(context);
            }
        }

        public override void EnterBoolNot([NotNull] BoolNotContext context)
        {
            base.EnterBoolNot(context);
            Log("EnterBoolNot");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterBoolNot(context);
            }
            else
            {
                symbolTableTraverser.EnterBoolNot(context);
                semanticErrorChecker.EnterBoolNot(context);
            }
        }

        public override void ExitBoolNot([NotNull] BoolNotContext context)
        {
            base.ExitBoolNot(context);
            Log("ExitBoolNot");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitBoolNot(context);
            }
            else
            {
                symbolTableTraverser.ExitBoolNot(context);
                semanticErrorChecker.ExitBoolNot(context);
            }
        }

        public override void EnterLessThan([NotNull] LessThanContext context)
        {
            base.EnterLessThan(context);
            Log("EnterLessThan");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterLessThan(context);
            }
            else
            {
                symbolTableTraverser.EnterLessThan(context);
                semanticErrorChecker.EnterLessThan(context);
            }
        }

        public override void ExitLessThan([NotNull] LessThanContext context)
        {
            base.ExitLessThan(context);
            Log("ExitLessThan");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitLessThan(context);
            }
            else
            {
                symbolTableTraverser.ExitLessThan(context);
                semanticErrorChecker.ExitLessThan(context);
            }
        }

        public override void EnterBlock([NotNull] BlockContext context)
        {
            base.EnterBlock(context);
            Log("EnterBlock");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterBlock(context);
            }
            else
            {
                symbolTableTraverser.EnterBlock(context);
                semanticErrorChecker.EnterBlock(context);
            }
        }

        public override void ExitBlock([NotNull] BlockContext context)
        {
            base.ExitBlock(context);
            Log("ExitBlock");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitBlock(context);
            }
            else
            {
                symbolTableTraverser.ExitBlock(context);
                semanticErrorChecker.ExitBlock(context);
            }
        }

        public override void EnterId([NotNull] IdContext context)
        {
            base.EnterId(context);
            Log("EnterId");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterId(context);
            }
            else
            {
                symbolTableTraverser.EnterId(context);
                semanticErrorChecker.EnterId(context);
            }
        }

        public override void ExitId([NotNull] IdContext context)
        {
            base.ExitId(context);
            Log("ExitId");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitId(context);
            }
            else
            {
                symbolTableTraverser.ExitId(context);
                semanticErrorChecker.ExitId(context);
            }
        }

        public override void EnterMultiply([NotNull] MultiplyContext context)
        {
            base.EnterMultiply(context);
            Log("EnterMultiply");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterMultiply(context);
            }
            else
            {
                symbolTableTraverser.EnterMultiply(context);
                semanticErrorChecker.EnterMultiply(context);
            }
        }

        public override void ExitMultiply([NotNull] MultiplyContext context)
        {
            base.ExitMultiply(context);
            Log("ExitMultiply");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitMultiply(context);
            }
            else
            {
                symbolTableTraverser.ExitMultiply(context);
                semanticErrorChecker.ExitMultiply(context);
            }
        }

        public override void EnterIf([NotNull] IfContext context)
        {
            base.EnterIf(context);
            Log("EnterIf");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterIf(context);
            }
            else
            {
                symbolTableTraverser.EnterIf(context);
                semanticErrorChecker.EnterIf(context);
            }
        }

        public override void ExitIf([NotNull] IfContext context)
        {
            base.ExitIf(context);
            Log("ExitIf");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitIf(context);
            }
            else
            {
                symbolTableTraverser.ExitIf(context);
                semanticErrorChecker.ExitIf(context);
            }
        }

        public override void EnterCase([NotNull] CaseContext context)
        {
            base.EnterCase(context);
            Log("EnterCase");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterCase(context);
            }
            else
            {
                symbolTableTraverser.EnterCase(context);
                semanticErrorChecker.EnterCase(context);
            }
        }

        public override void ExitCase([NotNull] CaseContext context)
        {
            base.ExitCase(context);
            Log("ExitCase");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitCase(context);
            }
            else
            {
                symbolTableTraverser.ExitCase(context);
                semanticErrorChecker.ExitCase(context);
            }
        }

        public override void EnterOwnMethodCall([NotNull] OwnMethodCallContext context)
        {
            base.EnterOwnMethodCall(context);
            Log("EnterOwnMethodCall");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterOwnMethodCall(context);
            }
            else
            {
                symbolTableTraverser.EnterOwnMethodCall(context);
                semanticErrorChecker.EnterOwnMethodCall(context);
            }
        }

        public override void ExitOwnMethodCall([NotNull] OwnMethodCallContext context)
        {
            base.ExitOwnMethodCall(context);
            Log("ExitOwnMethodCall");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitOwnMethodCall(context);
            }
            else
            {
                symbolTableTraverser.ExitOwnMethodCall(context);
                semanticErrorChecker.ExitOwnMethodCall(context);
            }
        }

        public override void EnterAdd([NotNull] AddContext context)
        {
            base.EnterAdd(context);
            Log("EnterAdd");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterAdd(context);
            }
            else
            {
                symbolTableTraverser.EnterAdd(context);
                semanticErrorChecker.EnterAdd(context);
            }
        }

        public override void ExitAdd([NotNull] AddContext context)
        {
            base.ExitAdd(context);
            Log("ExitAdd");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitAdd(context);
            }
            else
            {
                symbolTableTraverser.ExitAdd(context);
                semanticErrorChecker.ExitAdd(context);
            }
        }

        public override void EnterNew([NotNull] NewContext context)
        {
            base.EnterNew(context);
            Log("EnterNew");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterNew(context);
            }
            else
            {
                symbolTableTraverser.EnterNew(context);
                semanticErrorChecker.EnterNew(context);
            }
        }

        public override void ExitNew([NotNull] NewContext context)
        {
            base.ExitNew(context);
            Log("ExitNew");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitNew(context);
            }
            else
            {
                symbolTableTraverser.ExitNew(context);
                semanticErrorChecker.ExitNew(context);
            }
        }

        public override void EnterParentheses([NotNull] ParenthesesContext context)
        {
            base.EnterParentheses(context);
            Log("EnterParentheses");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterParentheses(context);
            }
            else
            {
                symbolTableTraverser.EnterParentheses(context);
                semanticErrorChecker.EnterParentheses(context);
            }
        }

        public override void ExitParentheses([NotNull] ParenthesesContext context)
        {
            base.ExitParentheses(context);
            Log("ExitParentheses");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitParentheses(context);
            }
            else
            {
                symbolTableTraverser.ExitParentheses(context);
                semanticErrorChecker.ExitParentheses(context);
            }
        }

        public override void EnterAssignment([NotNull] AssignmentContext context)
        {
            base.EnterAssignment(context);
            Log("EnterAssignment");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterAssignment(context);
            }
            else
            {
                symbolTableTraverser.EnterAssignment(context);
                semanticErrorChecker.EnterAssignment(context);
            }
        }

        public override void ExitAssignment([NotNull] AssignmentContext context)
        {
            base.ExitAssignment(context);
            Log("ExitAssignment");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitAssignment(context);
            }
            else
            {
                symbolTableTraverser.ExitAssignment(context);
                semanticErrorChecker.ExitAssignment(context);
            }
        }

        public override void EnterFalse([NotNull] FalseContext context)
        {
            base.EnterFalse(context);
            Log("EnterFalse");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterFalse(context);
            }
            else
            {
                symbolTableTraverser.EnterFalse(context);
                semanticErrorChecker.EnterFalse(context);
            }
        }

        public override void ExitFalse([NotNull] FalseContext context)
        {
            base.ExitFalse(context);
            Log("ExitFalse");
            if (phase == Phase.Building)
            {
                symbolTableCreator.ExitFalse(context);
            }
            else
            {
                symbolTableTraverser.ExitFalse(context);
                semanticErrorChecker.ExitFalse(context);
            }
        }

        public override void EnterInt([NotNull] IntContext context)
        {
            base.EnterInt(context);
            Log("EnterInt");
            if (phase == Phase.Building)
            {
                symbolTableCreator.EnterInt(context);
            }
            else
            {
                symbolTableTraverser.EnterInt(context);
                semanticErrorChecker.EnterInt(context);
            }
        }

        public override void ExitInt([NotNull] IntContext context)
        {
            base.ExitInt(context);
        }

        public override void EnterEqual([NotNull] EqualContext context)
        {
            base.EnterEqual(context);
        }

        public override void ExitEqual([NotNull] EqualContext context)
        {
            base.ExitEqual(context);
        }

        public override void EnterTrue([NotNull] TrueContext context)
        {
            base.EnterTrue(context);
        }

        public override void ExitTrue([NotNull] TrueContext context)
        {
            base.ExitTrue(context);
        }

        public override void EnterLessEqual([NotNull] LessEqualContext context)
        {
            base.EnterLessEqual(context);
        }

        public override void ExitLessEqual([NotNull] LessEqualContext context)
        {
            base.ExitLessEqual(context);
        }

        public override void EnterMethodCall([NotNull] MethodCallContext context)
        {
            base.EnterMethodCall(context);
        }

        public override void ExitMethodCall([NotNull] MethodCallContext context)
        {
            base.ExitMethodCall(context);
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);
        }

        public override void VisitTerminal([NotNull] ITerminalNode node)
        {
            base.VisitTerminal(node);
        }

        public override void VisitErrorNode([NotNull] IErrorNode node)
        {
            base.VisitErrorNode(node);
        }
    }
}

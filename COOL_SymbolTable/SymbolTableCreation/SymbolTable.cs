using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOL_Compiling.SymbolTableCreation
{
    public class SymbolTable
    {
        public readonly string id;
        public readonly HashSet<Symbol> entries = new HashSet<Symbol>();
        public readonly List<SymbolTable> childs = new List<SymbolTable>();

        public SymbolTable(string id)
        {
            this.id = id;
        }
    }

    public enum SymbolKind { Class, Method, Property, Variable }
    public enum SymbolType { Int, Float, String, Bool, Void, CustomType }

    public abstract class Symbol
    {
        public readonly string id;
        public abstract SymbolKind Kind { get; }
        /// <summary>
        /// Is it defined in it's symbol table's corresponding scope?
        /// </summary>
        public bool isDefined = false;
        /// <summary>
        /// Is it used in it's symbol table's corresponding scope?
        /// </summary>
        public bool isUsed = false;

        public Symbol(string id)
        {
            this.id = id;
        }
    }

    public class ClassSymbol : Symbol
    {
        public override SymbolKind Kind => SymbolKind.Class;
        public ClassSymbol baseClass = null;

        public ClassSymbol(string id, ClassSymbol baseClass) : base(id)
        {
            this.baseClass = baseClass;
        }
    }

    public class VariableSymbol : Symbol
    {
        public readonly SymbolType type;
        public readonly string customTypeID;
        public override SymbolKind Kind => SymbolKind.Variable;
        //Properties: nothing for now

        public VariableSymbol(string id, SymbolType type) : base(id)
        {
            this.type = type;
        }

        public VariableSymbol(string id, string customTypeID) : base(id)
        {
            type = SymbolType.CustomType;
            this.customTypeID = customTypeID;
        }
    }

    public class MethodSymbol : Symbol
    {
        public override SymbolKind Kind => SymbolKind.Method;
        public readonly SymbolType returnType;
        public readonly SymbolType[] parameterTypes;
        //Properties: nothing for now

        public MethodSymbol(string id, SymbolType returnType, SymbolType[] parameterTypes) : base(id)
        {
            this.returnType = returnType;
            this.parameterTypes = parameterTypes;
        }
    }

    public class PropertySymbol : Symbol
    {
        public override SymbolKind Kind => SymbolKind.Property;
        public readonly SymbolType returnType;
        public readonly string customTypeID;
        //Properties: nothing for now

        public PropertySymbol(string id, SymbolType returnType) : base(id)
        {
            this.returnType = returnType;
        }

        public PropertySymbol(string id, string customTypeID) : base(id)
        {
            returnType = SymbolType.CustomType;
            this.customTypeID = customTypeID;
        }
    }
}

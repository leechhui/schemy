// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Schemy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Scheme symbol
    /// </summary>
    /// <remarks>
    /// Symbols are interned so that symbols with the same name are actually of the same symbol object instance.
    /// </remarks>
    public class Symbol : IEquatable<Symbol>
    {
        private static readonly IDictionary<string, Symbol> table = new Dictionary<string, Symbol>();
        public static readonly IReadOnlyDictionary<string, Symbol> QuotesMap = new Dictionary<string, Symbol>()
            {
                { "'", Symbol.QUOTE },
                { "`", Symbol.QUASIQUOTE},
                { ",", Symbol.UNQUOTE},
                { ",@", Symbol.UNQUOTE_SPLICING},
            };

        public readonly string name;
        public readonly bool isKeyword;
        // public readonly bool isInterned;

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="sym">The symbol</param>
        /// <remarks>
        /// This is private and the users should call <see cref="FromString"/> to instantiate a symbol object.
        /// </remarks>
        private Symbol(string sym)
        {
            this.name = sym;
            this.isKeyword = false;
        }

        private Symbol(string sym, bool isKeyword)
        {
            this.name = sym;
            this.isKeyword = isKeyword;
        }

        public string AsString
        {
            get { return this.name; }
        }

        public bool IsInterned
        {
            get { return table.ContainsKey(this.name); }
        }

        /// <summary>
        /// Returns the interned symbol
        /// </summary>
        /// <param name="sym">The symbol name</param>
        /// <returns>the symbol instance</returns>
        public static Symbol FromString(string sym)
        {
            if (table.TryGetValue(sym, out Symbol resultSymbol))
            {
                return resultSymbol;
            }else{
                var newSymbol = new Symbol(sym);
                table[sym] = newSymbol;
                return newSymbol;
            }
            // Symbol res;
            // if (!table.TryGetValue(sym, out res))
            // {
            //     table[sym] = new Symbol(sym);
            // }

            // return table[sym];
        }

        #region wellknown symbols
        // TODO: Optimization, use readonly instead of properties
        public static Symbol IF { get { return Symbol.FromString("if"); } }
        public static Symbol QUOTE { get { return Symbol.FromString("quote"); } }
        public static Symbol SET { get { return Symbol.FromString("set!"); } }
        public static Symbol DEFINE { get { return Symbol.FromString("define"); } }
        public static Symbol LAMBDA { get { return Symbol.FromString("lambda"); } }
        public static Symbol BEGIN { get { return Symbol.FromString("begin"); } }
        public static Symbol DEFINE_MACRO { get { return Symbol.FromString("define-macro"); } }
        public static Symbol QUASIQUOTE { get { return Symbol.FromString("quasiquote"); } }
        public static Symbol UNQUOTE { get { return Symbol.FromString("unquote"); } }
        public static Symbol UNQUOTE_SPLICING { get { return Symbol.FromString("unquote-splicing"); } }
        public static Symbol EOF { get { return Symbol.FromString("#<eof-object>"); } }
        public static Symbol APPEND { get { return Symbol.FromString("append"); } }
        public static Symbol CONS { get { return Symbol.FromString("cons"); } }
        #endregion wellknown symbols

        #region object implementations
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            /*
            if (obj is Symbol)
            {
                return object.Equals(this.symbol, ((Symbol)obj).symbol);
            }
            else
            {
                return false;
            }
           */
            var tmp = obj as Symbol;
            if (!object.ReferenceEquals(null, tmp))
            {
                return string.Equals(this.name, tmp.name);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            // return string.Format("'{0}", this.symName);
            return "'" + this.name;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        public bool Equals(Symbol other)
        {
            // return ((object)this).Equals(other);
            return string.Equals(this.name, other.name);
        }
        #endregion object implementations

        public static List<string> AllSymbols()
        {
            return new List<string>(table.Keys);
        }
    }
}

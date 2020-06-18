using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

// Good essay: http://kflu.github.io/2018/04/15/2018-04-15-implement-scheme/

namespace cscheme
{
    public class Interpreter
    {
        public readonly Environment env;
        public readonly Dictionary<SchSym, Procedure> MacroTable;
        public readonly Dictionary<string, SchSym> SymbolTable;

        public Interpreter()
        {
            env = new Environment(new Dictionary<SchSym, object>(), null);
            MacroTable = new Dictionary<SchSym, Procedure>(); 
            SymbolTable = new Dictionary<string, SchSym>() {
                {SchSym.IF.Name, SchSym.IF},
                {SchSym.QUOTE.Name, SchSym.QUOTE},
                {SchSym.SET.Name, SchSym.SET},
                {SchSym.DEFINE.Name, SchSym.DEFINE},
                {SchSym.LAMBDA.Name, SchSym.LAMBDA},
                {SchSym.BEGIN.Name, SchSym.BEGIN},
                {SchSym.DEFINE_MACRO.Name, SchSym.DEFINE_MACRO},
                {SchSym.QUASIQUOTE.Name, SchSym.QUASIQUOTE},
                {SchSym.UNQUOTE.Name, SchSym.UNQUOTE},
                {SchSym.UNQUOTE_SPLICING.Name, SchSym.UNQUOTE_SPLICING},
                // {SchSym.EOF.Name, SchSym.EOF}, // uninterned; can't be read
                {SchSym.APPEND.Name, SchSym.APPEND},
                {SchSym.CONS.Name, SchSym.CONS},
            };
        }

        /// <summary>
        ///   Generate a symbol.
        /// </summary>
        public SchSym GenSym(string name)
        {
            // if (SymbolTable.TryGetValue(name, out SchSym result))
            // {
            //     return result;
            // }
            // var newSym = new SchSym(name);
            // SymbolTable.Add(name, newSym);
            // return newSym;
            return new SchSym(name);
        }

        public SchSym Intern(string name)
        {
            if (SymbolTable.TryGetValue(name, out SchSym result))
            {
                return result;
            }
            var newSym = new SchSym(name);
            SymbolTable.Add(name, newSym);
            return newSym;
        }

        /// <summary>
        ///   Load text program.
        /// </summary>
        public object Load(string program)
        {
            using(TextReader reader = new StringReader(program))
            {
                return Load(reader);
            }
        }

        /// <summary>
        ///   Load text program from TextReader.
        /// </summary>
        public object Load(TextReader reader)
        {
            return Parse(new InPort(reader));
        }

        public object Repl()
        {
            return null;
        }

        /// <summary>
        ///   Evaluate an expression in an environment.
        /// </summary>
        object Eval(object x)
        {
            return null;
        }

        /// <summary>
        ///   Parse a program: read and expand/error-check it.
        /// </summary>
        object Parse(InPort inport)
        {
            return null;
        }

        /// <summary>
        ///   Walk tree of x, making optimizations/fixes, and signaling SyntaxError.
        /// </summary>
        object Expand()
        {
            return null;
        }

        /// <summary>
        ///   Read a Scheme expression from an input port.
        /// </summary>
        object Read(InPort inport)
        {
            return null;
        }

        /// <summary>
        ///   Read the next character from an input port.
        /// </summary>
        object ReadChar(InPort inport)
        {
            return null;
        }

        /// <summary>
        ///   Numbers become numbers; #t and #f are booleans; "..." string; otherwise Symbol.
        /// </summary>
        private object ParseAtom(string token)
        {
            if (token == "#t")
            {
                // Do we really need boolean?
                // return new SchBool(true);
                return new SchInt(1);
            }
            else if (token == "#f")
            {
                // return new SchBool(false);
                return new SchInt(0);
            }
            else if (token[0] == '"')
            {
                return token.Substring(1, token.Length - 2);
            }
            else if (SchNum.TryParse(token, out SchObj num))
            {
                return num;
            }
            else
            {
                return Intern(token); // a symbol
            }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;

namespace cscheme
{
    public class Interpreter
    {
        private readonly Environment env;
        private readonly Dictionary<SchSym, Procedure> macros;

        public Interpreter()
        {
            env = new Environment(new Dictionary<SchSym, object>(), null);
        }

        public object Load(string program)
        {
            using(TextReader reader = new StringReader(program))
            {
                return Load(reader);
            }
        }

        public object Load(TextReader reader)
        {
            return null;
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
    }
}

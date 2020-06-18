using System;
using System.Collections.Generic;

namespace cscheme
{
    /// <summary>
    /// Tracks the state of an interpreter or a procedure. It supports lexical scoping.
    /// </summary>
    public class Environment
    {
        /// <summary>
        ///   Never be null
        /// </summary>
        public readonly Dictionary<SchSym, object> env;

        /// <summary>
        /// The enclosing environment. For top level env, this is null.
        /// </summary>
        public readonly Environment outer;

        public Environment(Dictionary<SchSym, object> env, Environment outer)
        {
            if (null == env)
            {
                this.env = new Dictionary<SchSym, object>();
            }else{
                this.env = env;
            }
            this.outer = outer;
        }
    }
}

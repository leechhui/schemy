using System;
using System.Collections.Generic;

namespace cscheme
{
    /// <summary>
    /// Represents a procedure value in Scheme
    /// </summary>
    interface ICallable
    {
        /// <summary>
        /// Invokes this procedure
        /// </summary>
        /// <param name="args">The arguments. These are the `cdr` of the s-expression for the procedure invocation.</param>
        /// <returns>the result of the procedure invocation</returns>
        object Call(List<object> args);
    }

    /// <summary>
    /// A procedure implemented in Scheme
    /// </summary>
    /// <seealso cref="Schemy.ICallable" />
    public class Procedure : ICallable
    {
        private readonly object expr; // Expression
        private readonly object body;
        private readonly cscheme.Environment env;

        // public Procedure(Union<Symbol, List<Symbol>> parameters, object body, Environment env)
        // {
        //     this.parameters = parameters;
        //     this.body = body;
        //     this.env = env;
        // }

        public object Body
        {
            get { return this.body; }
        }

        public object Expr
        {
            get => expr;
        }

        public cscheme.Environment Env
        {
            get => env;
        }

        /// <summary>
        /// Invokes this procedure
        /// </summary>
        /// <remarks>
        /// Implementation note: under normal function invocation scenarios, this method is not used. Instead,
        /// a tail call optimization is used in the interpreter evaluation phase that runs Scheme functions.
        /// 
        /// This method is useful however, in macro expansions, and any other occasions where the tail call optimization
        /// is not (yet) implemented.
        /// 
        /// <see cref="Interpreter.EvaluateExpression(object, Environment)"/>
        /// </remarks>
        public object Call(List<object> args)
        {
            // NOTE: This is not needed for regular function invoke after the tail call optimization.
            // a (non-native) procedure is now optimized into evaluating the body under the environment
            // formed by the (params, args). So the `Call` method will never be used.
            // return Interpreter.EvaluateExpression(this.body, Environment.FromVariablesAndValues(this.parameters, args, this.env));
            return null;
        }

        /// <summary>
        /// Prints the implementation of the function.
        /// </summary>
        public override string ToString()
        {
            // var form = new List<object> { Symbol.LAMBDA, this.parameters.Use(sym => (object)sym, syms => syms.Cast<object>().ToList()), this.body };
            // return Utils.PrintExpr(form);
            return "";
        }
    }
}

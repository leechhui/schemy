// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Schemy
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {

        delegate object Function(object input);

        public static void Main(string[] args)
        {

            Interpreter.CreateSymbolTableDelegate extension = _ => new Dictionary<Symbol, object>()
            {
                { Symbol.FromString("say-hi"), NativeProcedure.Create<Function>(() => name => $"Hello {name}!") },
                { Symbol.FromString("truncate-string"), NativeProcedure.Create<int, Function>(len => input => ((string)input).Substring(0, len)) },
                { Symbol.FromString("dump"), NativeProcedure.Create<object>(
                    () => Symbol.AllSymbols()
                    ) }
            };

            if (args.Length > 0 && File.Exists(args[0]))
            {
                // evaluate input file's content
                var file = args[0];
                var interpreter = new Interpreter(new[] {extension}, new ReadOnlyFileSystemAccessor());

                using (TextReader reader = new StreamReader(file))
                {
                    object res = interpreter.Evaluate(reader);
                    Console.WriteLine(Utils.PrintExpr(res));
                }
            }
            else
            {
                // starts the REPL
                var interpreter = new Interpreter(new[] {extension}, new ReadOnlyFileSystemAccessor());
                var headers = new[]
                {
                    "-----------------------------------------------",
                    "| Schemy - Scheme as a Configuration Language |",
                    "| Press Ctrl-C to exit                        |",
                    "-----------------------------------------------",
                };

                interpreter.REPL(Console.In, Console.Out, "Schemy> ", headers);
            }
        }
    }
}

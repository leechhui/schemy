using System;
using Schemy;

namespace memory_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var intMem = new Allocator<int>(64);
        }
    }
}

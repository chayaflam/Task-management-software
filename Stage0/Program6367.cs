using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome6367();
            welcome9589();
            Console.ReadKey();
        }

        static partial void welcome9589();

        private static void welcome6367()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}


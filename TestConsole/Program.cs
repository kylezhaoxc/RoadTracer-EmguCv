using System;
using RoadTracer;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Operator op = new Operator();

            try { op.FindMiddleByPath("D:\\4.jpg"); }
            catch (TextException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }
}

using System;
using RoadTracer;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Operator op = new Operator(640,480,280);

            try { op.FindMiddleByPath("D:\\5.jpg"); }
            catch (TextException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }
}

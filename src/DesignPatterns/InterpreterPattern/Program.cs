using System;

namespace InterpreterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Interpreter Pattern!");

            // ParserTest();

            Solution.InterpreterTests.Test();
        }

        private static void ParserTest()
        {
            // 2 3 + 5 *
            string expression = "2 3 + 5 *";

            var parser = new Parser();

            int result = parser.Evaluate(expression);

            Console.WriteLine($"{expression} = {result}");

        }
    }

    #region Model

    public class Parser
    {
        public int Evaluate(string s)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}

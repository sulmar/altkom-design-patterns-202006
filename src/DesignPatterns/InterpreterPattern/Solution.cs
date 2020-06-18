using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterPattern.Solution
{
    public class InterpreterTests
    {
        public static void Test()
        {
            // 2 3 + 5 *
            string expression = "2 3 + 5 *";

            var parser = new Parser();

            int result = parser.Evaluate(expression);

            Console.WriteLine($"{expression} = {result}");
        }
    }

    // Abstract Expression
    public interface IExpression
    {
        void Interpret(Context context);
    }

    // Context
    public class Context : Stack<int>
    {

    }

    // TerminalExpression 
    public class NumberExpression : IExpression
    {
        private int number;

        public NumberExpression(int number)
        {
            this.number = number;
        }

        public void Interpret(Context context)
        {
            context.Push(number);
        }
    }

    // TerminalExpression 
    public class PlusExpression : IExpression
    {
        public void Interpret(Context context)
        {
            context.Push(context.Pop() + context.Pop());
        }
    }

    public class MinusExpression : IExpression
    {
        public void Interpret(Context context)
        {
            context.Push(-context.Pop() + context.Pop());
        }
    }

    // TerminalExpression 
    public class MultiplyExpression : IExpression
    {
        public void Interpret(Context context)
        {
            context.Push(context.Pop() * context.Pop());
        }
    }

    public static class ExpressionFactory
    {
        static public IExpression Create(string token)
        {
            switch (token)
            {
                case "+": return new PlusExpression();
                case "-": return new MinusExpression();
                case "*": return new MultiplyExpression();

                default: return new NumberExpression(int.Parse(token));
            }
        }
    }

    public class Parser
    {
        private IEnumerable<IExpression> Parse(string stringExpression, char separator = ' ')
        {
            ICollection<IExpression> expressionTree = new List<IExpression>();

            var tokens = stringExpression.Split(separator);

            foreach (var token in tokens)
            {
                var expression = ExpressionFactory.Create(token);
                expressionTree.Add(expression);
            }

            return expressionTree;
        }

        public int Evaluate(string stringExpression)
        {
            var expressionTree = Parse(stringExpression);

            Context context = new Context();

            foreach (var expression in expressionTree)
            {
                expression.Interpret(context);
            }

            return context.Pop();
        }
    }

}

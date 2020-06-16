using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CommandPattern
{
    public class CommandTest
    {
        public static void Test()
        {
            // CanExecuteTest();

            ICollection<ICommand> commands = new Collection<ICommand>();
            commands.Add(new PrintCommand(content: "Hello World!"));
            commands.Add(new SendCommand(from: "53453443", to: "555888000", content: "Hello World!"));
            commands.Add(new SendCommand(from: "53453534", to: "555888000", content: "Hello World!"));
            commands.Add(new SendCommand(from: "67545345", to: "555888000", content: "Hello World!"));

            foreach (ICommand command in commands)
            {
                if (command.CanExecute())
                {
                    command.Execute();
                }
            }

        }

        private static void CanExecuteTest()
        {
            ICommand command = new SendCommand(from: "", to: "555888000", content: "Hello World!");

            if (command.CanExecute())
            {
                command.Execute();
            }

            command = new PrintCommand(content: "Hello World!");

            if (command.CanExecute())
            {
                command.Execute();
            }
        }
    }

    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }

    public interface IPriorityCommand : ICommand
    {
        byte Priority { get; }
        bool IsEnabled { get; set; }
    }


    public class SendCommand : ICommand
    {
        public SendCommand(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public string Content { get; private set; }

        public bool CanExecute()
        {
            return !(string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Content));
        }

        public void Execute()
        {
            Console.WriteLine($"Send message from <{From}> to <{To}> {Content}");
        }
    }

    public class PrintCommand : ICommand
    {
        public PrintCommand(string content)
        {
            Content = content;
        }

        public string Content { get; private set; }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(Content);
        }

        public void Execute()
        {
            Console.WriteLine($"Print message {Content}");
        }
    }
}

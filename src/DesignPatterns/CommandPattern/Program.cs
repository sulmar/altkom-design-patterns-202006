using System;

namespace CommandPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Command Pattern!");

            CommandTest.Test();

            Message message = new Message(from: "", to: "555888000", content: "Hello World!");

            if (message.CanPrint())
            {
                message.Print();
            }

            if (message.CanPrint())
            {
                message.Send();
            }    
        }
    }

    #region Models

    public class Message
    {
        public Message(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
     
        public void Send()
        {
            Console.WriteLine($"Send message from <{From}> to <{To}> {Content}");
        }

        public bool CanSend()
        {
            return !(string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Content));
        }

        public void Print()
        {
            Console.WriteLine($"Print message {Content}");
        }

        public bool CanPrint()
        {
            return !string.IsNullOrEmpty(Content);
        }



    }

    #endregion
}

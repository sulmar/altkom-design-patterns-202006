using System;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Singleton Pattern!");

            LoggerTest();
        }

        private static void LoggerTest()
        {
            MessageService messageService = new MessageService();
            PrintService printService = new PrintService();
            messageService.Send("Hello World!");
            printService.Print("Hello World!", 3);

            if (ReferenceEquals(messageService.logger, printService.logger))
            {
                Console.WriteLine("The same instances");
            }
            else
            {
                Console.WriteLine("Different instances");
            }
        }
    }

    public class FileLogger : Logger
    {

    }

    public class Logger
    {
        protected Logger()
        {

        }

        private static Logger instance;

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }

                return instance;
            }
        }

        public void LogInformation(string message)
        {
            Console.WriteLine($"Logging {message}");
        }
    }



    public class MessageService
    {
        public Logger logger;

        public MessageService()
        {
            // logger = new Logger();

            logger = Logger.Instance;
        }

        public void Send(string message)
        {
            logger.LogInformation($"Send {message}");
        }
    }

    public class PrintService
    {
        public Logger logger;

        public PrintService()
        {
            logger = Logger.Instance;
        }

        public void Print(string content, int copies)
        {
            for (int i = 1; i < copies+1; i++)
            {
                logger.LogInformation($"Print {i} copy of {content}");
            }
        }




    }
}

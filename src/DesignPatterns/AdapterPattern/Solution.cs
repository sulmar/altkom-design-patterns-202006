using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern
{
    public interface IRadioAdapterFactory
    {
        IRadio Create(string type);
    }

    public class RadioAdapterFactory : IRadioAdapterFactory
    {
        public IRadio Create(string type)
        {
            switch(type)
            {
                case "M": return new MotorolaRadioAdapter();
                case "H": return new HyteraRadioAdapter();

                default: throw new NotSupportedException(type);
            }
        }
    }

    public class RadioAdapterTest
    {
        public static void Test()
        {
            Console.WriteLine("Choose radio (M)otorola (H)ytera");
            string input = Console.ReadLine();

            IRadioAdapterFactory radioAdapterFactory = new RadioAdapterFactory();

            IRadio radio = radioAdapterFactory.Create(input);

            radio.Send(10, "Hello World!");
        }
    }

    public interface IRadio
    {
        void Send(byte channel, string message);
    }

    // Adapter
    public class MotorolaRadioAdapter : IRadio
    {
        // Adaptee
        private MotorolaRadio radio;

        public MotorolaRadioAdapter()
        {
            radio = new MotorolaRadio();
        }

        public void Send(byte channel, string message)
        {
            radio.PowerOn();
            radio.SelectChannel(channel);
            radio.Send(message);
            radio.PowerOff();
        }
    }

    public class HyteraRadioAdapter : IRadio
    {
        // Adaptee
        private HyteraRadio radio;

        public HyteraRadioAdapter()
        {
            radio = new HyteraRadio();
        }

        public void Send(byte channel, string message)
        {
            radio.Init();
            radio.SendMessage(channel, message);
            radio.Release();
        }
    }
}

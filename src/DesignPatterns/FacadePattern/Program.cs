using System;
using System.Threading;

namespace FacadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Facade Pattern!");

            ZeroDivideFloat();
            ZeroDivide();

            // WashMachineTest();


            FacadeWashMachineTest();

        }

        private static void ZeroDivide()
        {
            int x = 10;
            int y = 0;

            int result = x / y;
        }

        private static void ZeroDivideFloat()
        {
            float x = 10;
            float y = 0;

            float result = x / y;
        }

        private static void FacadeWashMachineTest()
        {
            IWashMachine washMachine = new WashMachine(new Heater(), new Engine(), new Pump());            
            
            washMachine.SetTemperature(30);
            washMachine.Start();

        }

        private static void WashMachineTest()
        {
            // Wybieram program (steruje czasem oraz cyklem) 
            // Ustawiam temperaturę
            // Ustawiam prędkość wirowania

            byte temperature = 40;
            int rotationSpeed = 1200;

            WashMachine washMachine = new WashMachine(new Heater(), new Engine(), new Pump());

            bool quick = true;

            if (quick)
            {
                // Włączenie blokady
                washMachine.Locked = true;
                Console.WriteLine("Włączenie blokady");

                // pobiera wodę
                washMachine.Pump.Direction = Direction.In;
                washMachine.Pump.Start();

                Thread.Sleep(TimeSpan.FromSeconds(1));

                washMachine.Pump.Stop();

                // podgrzewa wodę

                int maxTemp = 30;

                if (temperature> maxTemp)
                {
                    Console.WriteLine($"Błąd - nastawiona temperaturę większą od {maxTemp} st. C");
                    return;
                }

                washMachine.Heater.Temperature = temperature;
                washMachine.Heater.On();

                // obraca bęben 

                washMachine.Engine.RotationSpeed = 200;
                washMachine.Engine.RotateRight();

                Thread.Sleep(TimeSpan.FromSeconds(1));

                washMachine.Engine.RotateLeft();

                Thread.Sleep(TimeSpan.FromSeconds(1));

                // zakończenie cyklu prania

                washMachine.Engine.Stop();
                washMachine.Engine.RotationSpeed = 0;

                washMachine.Heater.Off();

                // wypompowanie wody

                washMachine.Pump.Direction = Direction.Out;
                washMachine.Pump.Start();

                washMachine.Pump.Direction = Direction.In;
                washMachine.Pump.Start();


                // Zwolnienie blokady
                washMachine.Locked = false;
                Console.WriteLine("Zwolnienie blokady");

            }
            else
            {
                // Włączenie blokady
                washMachine.Locked = true;
                Console.WriteLine("Włączenie blokady");

                // pobiera wodę
                washMachine.Pump.Direction = Direction.In;
                washMachine.Pump.Start();

                Thread.Sleep(TimeSpan.FromSeconds(10));

                washMachine.Pump.Stop();

                // podgrzewa wodę
                washMachine.Heater.Temperature = temperature;
                washMachine.Heater.On();

                // obraca bęben 

                washMachine.Engine.RotationSpeed = 100;
                washMachine.Engine.RotateRight();

                Thread.Sleep(TimeSpan.FromSeconds(5));

                washMachine.Engine.RotateLeft();

                Thread.Sleep(TimeSpan.FromSeconds(5));

                // zakończenie cyklu prania

                washMachine.Engine.Stop();
                washMachine.Engine.RotationSpeed = 0;

                washMachine.Heater.Off();

                // wypompowanie wody

                washMachine.Pump.Direction = Direction.Out;
                washMachine.Pump.Start();

                washMachine.Pump.Direction = Direction.In;
                washMachine.Pump.Start();

                // wirowanie
                washMachine.Engine.RotationSpeed = rotationSpeed;
                washMachine.Engine.RotateRight();

                Thread.Sleep(TimeSpan.FromSeconds(5));

                // płukanie

                washMachine.Pump.Direction = Direction.In;
                washMachine.Pump.Start();

                Thread.Sleep(TimeSpan.FromSeconds(10));

                washMachine.Pump.Stop();

                washMachine.Pump.Direction = Direction.Out;
                washMachine.Pump.Start();

                // Zwolnienie blokady
                washMachine.Locked = false;
                Console.WriteLine("Zwolnienie blokady");
            }


        }
    }

    // Wybieram program (steruje czasem oraz cyklem) 
    // Ustawiam temperaturę
    // Ustawiam prędkość wirowania


    // Facade
    public interface IWashMachine
    {
        void SetTemperature(byte temperature);
        void SetRotationSpeed(int rotationSpeed);
        void Start();        
    }

    public class WashMachine : IWashMachine
    {
        public WashMachine(Heater heater, Engine engine, Pump pump)
        {
            Heater = heater;
            Engine = engine;
            Pump = pump;
            Locked = false;
        }

        public Heater Heater { get; set; }
        public Engine Engine { get; set; }
        public Pump Pump { get; set; }


        private byte temperature;

        public bool Locked { get; set; }

        public void SetRotationSpeed(int rotationSpeed)
        {
            throw new NotImplementedException();
        }

        public void SetTemperature(byte temperature)
        {
            this.temperature = temperature;
        }

        public void Start()
        {
            // Włączenie blokady
            this.Locked = true;
            Console.WriteLine("Włączenie blokady");

            // pobiera wodę
            this.Pump.Direction = Direction.In;
            this.Pump.Start();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            this.Pump.Stop();

            // podgrzewa wodę

            int maxTemp = 30;

            if (temperature > maxTemp)
            {
                Console.WriteLine($"Błąd - nastawiona temperaturę większą od {maxTemp} st. C");
                return;
            }

            this.Heater.Temperature = temperature;
            this.Heater.On();

            // obraca bęben 

            this.Engine.RotationSpeed = 200;
            this.Engine.RotateRight();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            this.Engine.RotateLeft();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            // zakończenie cyklu prania

            this.Engine.Stop();
            this.Engine.RotationSpeed = 0;

            this.Heater.Off();

            // wypompowanie wody

            this.Pump.Direction = Direction.Out;
            this.Pump.Start();

            this.Pump.Direction = Direction.In;
            this.Pump.Start();


            // Zwolnienie blokady
            this.Locked = false;
            Console.WriteLine("Zwolnienie blokady");
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }

    public class Pump
    {
        public bool Enabled { get; private set; }

        public Direction Direction { get; set; }


        public void Start()
        {
            Enabled = true;
            Console.WriteLine($"Pump started {Direction}");
        }

        public void Stop()
        {
            Enabled = false;
            Console.WriteLine("Pump stopped");
        }
    }

    public enum Direction
    {
        In,
        Out
    }

    public class Engine
    {
        public bool Running { get; private set; }

        public int RotationSpeed { get; set; }

        public void RotateRight()
        {
            Running = true;
            Console.WriteLine($"Engine rotating {RotationSpeed} right ");
        }

        public void RotateLeft()
        {
            Running = true;
            Console.WriteLine($"Engine rotating {RotationSpeed} left");
        }

        public void Stop()
        {
            Running = false;
            Console.WriteLine("Engine stopped");
        }
    }

    public class Heater
    {
        private bool heating;

        public byte Temperature { get; set; }

        public void On()
        {
            heating = true;
            Console.WriteLine("Heater on");
        }

        public void Off()
        {
            heating = false;
            Console.WriteLine("Heater off");

        }


    }
}

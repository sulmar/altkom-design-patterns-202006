using System;
using System.Collections.Generic;
using System.Text;
using Stateless;

namespace StatePattern.Solution
{

    public class StateTest
    {
        public static void LampTest()
        {
            Lamp lamp = new Lamp();

            Console.WriteLine(lamp.Graph);


            Console.WriteLine(lamp.State);

            lamp.PowerOn();
            Console.WriteLine(lamp.State);

            lamp.PowerOn();
            Console.WriteLine(lamp.State);

            lamp.PowerOff();
            Console.WriteLine(lamp.State);

            lamp.PowerOff();
            Console.WriteLine(lamp.State);

            lamp.PowerOff();
            Console.WriteLine(lamp.State);
        }
    }

    // PMC> Intall-Package Stateless
    public class Lamp
    {
        public LampState State => machine.State;

        private StateMachine<LampState, LampTrigger> machine;

        public Lamp()
        {
            machine = new StateMachine<LampState, LampTrigger>(LampState.Off);

            machine.Configure(LampState.Off)
                .Permit(LampTrigger.SwitchOn, LampState.On)                
                .Ignore(LampTrigger.SwitchOff);

            machine.Configure(LampState.On)
                .OnEntry(() => Console.WriteLine("Pamiętaj o wyłączeniu światła"), "Powitanie")
                .OnEntry(() => Console.WriteLine("<xml>start</xml>"), "Sterowanie")
                .Permit(LampTrigger.SwitchOff, LampState.Off)
                .Permit(LampTrigger.SwitchOn, LampState.Blinking)
                .OnExit(() => Console.WriteLine("Dziekuje za wylaczenie swiatla", "Pożegnanie"));
                ;

            machine.Configure(LampState.Blinking)
                .Permit(LampTrigger.SwitchOn, LampState.On);
            
            
        }

        public void PowerOn() => machine.Fire(LampTrigger.SwitchOn);

        public void PowerOff() => machine.Fire(LampTrigger.SwitchOff);

        // Pobranie grafu
        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());

    }

    public enum LampState
    {
        On,
        Off,
        Blinking
    }

    public enum LampTrigger
    {
        SwitchOn,
        SwitchOff
    }
}

using System;
using Raspberry.IO.GeneralPurpose;
using GrowBoxShared;
using System.Threading;
using System.Collections.Generic;


namespace Test_DigitalMonoIO
{
    class Program
    {
        //static private DigitalIO _pin;
        static private GpioPinValue _pinValue;

        /*private void Timer_Tick(object sender, object e)
        {
            if (_pinValue == GpioPinValue.High) _pinValue = GpioPinValue.Low;
            else _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);
        }*/

        static void Main(string[] args)
        {
            DigitalConverterMCP3208 adc = new DigitalConverterMCP3208();
            
            DigitalIO pinIn1 = new DigitalIO(ConnectorPin.P1Pin38, GpioPinDriveMode.Input);
            DigitalIO pinOut1 = new DigitalIO(ConnectorPin.P1Pin37, GpioPinDriveMode.Output);
            DigitalIO pinIn2 = new DigitalIO(ConnectorPin.P1Pin40, GpioPinDriveMode.Input);
            DigitalIO pinOut2 = new DigitalIO(ConnectorPin.P1Pin35, GpioPinDriveMode.Output);

            // definizioni per test output digitali su shift register
            OutShiftRegister shift = new OutShiftRegister(16, 25, 24, 23);
            Device_OnOffShift irrigator = new Device_OnOffShift(shift, 8);    // ottavo pin dello shift register
            Device_OnOffShift humidifier = new Device_OnOffShift(shift, 2);   // secondo pin dello shift register

            while (true)
            {
                Console.CursorTop = 0;
                Console.Clear();
                
                // test two digital inputs
                Console.WriteLine("Bottone 1: Canale {0}, Valore {1} ",
                    pinIn1.connectorPin.ToString(), pinIn1.Read().ToString());
                Console.WriteLine("Bottone 2: Canale {0}, Valore {1} ",
                    pinIn2.connectorPin.ToString(), pinIn2.Read().ToString());

                // test two digital outputs
                Console.WriteLine("\r\nLED 1: Canale {0}, Valore: {1} ",
                    pinOut1.connectorPin.ToString(), pinIn1.Read().ToString());
                pinOut1.Write(pinIn1.Read());
                Console.WriteLine("LED 2: Canale {0}, Valore: {1} ",
                    pinOut2.connectorPin.ToString(), pinIn2.Read().ToString());
                pinOut2.Write(pinIn2.Read());

                //ActuateShiftRegister();

                Thread.Sleep(500);
            }
        }
        private static List<DigitalIO> RicercaPin()
        {
            // ricerca di tutti i pin possibili nella Raspberry che stiamo usando
            List<DigitalIO> pinTuttiIn = new List<DigitalIO>();

            foreach (ConnectorPin pin in Enum.GetValues(typeof(ConnectorPin)))
            {
                pinTuttiIn.Add(new DigitalIO(pin, GpioPinDriveMode.Input));
                Console.Write("{0} ", Enum.GetName(typeof(ConnectorPin), pin));                
            }

            return pinTuttiIn;
        }
    }
}
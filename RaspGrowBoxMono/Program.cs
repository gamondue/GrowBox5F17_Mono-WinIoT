# define DOTNET_MONO

using GrowBoxShared;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RaspGrowBoxMono
{
    class Program
    {
        private const int LED_PIN = 16;
        static private DigitalIO _pin;
        static private GpioPinValue _pinValue;
        //static private DispatcherTimer _timer;
        static private DigitalConverterMCP3208 adc;
        static private LightSensor lux;
        static private TemperatureSensor temp;
        static private SoilMoistureSensor moisture;
        static private CO2Sensor co2;
        static private RelativeHumiditySensor rh;
        static private OutShiftRegister shift;
        static private LCD1602Shift lcd;
        static private FileLogger fileLog;
        static private Device_OnOffShift irrigator;
        static private Device_OnOffShift humidifier;
        static private LightDevice lightDevice;

        static void Main(string[] args)
        {
            //datalog();
            test();
        }

        private static void datalog()
        {
            while(true)
            { 
                fileLog = new FileLogger("file.log");

                // Crea ed utilizza un ADC. Il codice del ADC utilizzerà un gestore del bus SPI
                adc = new DigitalConverterMCP3208();

                lux = new LightSensor(adc, 0, 3.3);
                temp = new TemperatureSensor(adc, 1, 3.3);
                moisture = new SoilMoistureSensor(adc, 2, 3.3);
                co2 = new CO2Sensor(adc, 3, 4.9);
                rh = new RelativeHumiditySensor(adc, 4, 5.0);

                shift = new OutShiftRegister(16, 25, 24, 23);

                lcd = new LCD1602Shift(shift, 12, 11, 10, 9, 13, 14);

                irrigator = new Device_OnOffShift(shift, 8);
                ////////irrigator.WriteLog += WriteFileLog;
                humidifier = new Device_OnOffShift(shift, 2);
                ////////humidifier.WriteLog += WriteFileLog;

                lightDevice = new LightDevice(shift, 3, 4, 5, 6, 7, 21, 20);

                lightDevice.PercentualeApertura(50);
                //irrigator.TimeOn(3000);

                //_timer = new DispatcherTimer();
                //_timer.Interval = TimeSpan.FromMilliseconds(1000);
                //_timer.Tick += Timer_Tick;
                //if (_pin != null)
                //{
                //    _timer.Start();
                //}
            }
        }
        private void WriteFileLog(string Line)
        {
            fileLog.WriteLogger(Line);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (_pinValue == GpioPinValue.High) _pinValue = GpioPinValue.Low;
            else _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);

            //textLux.Text = lux.Read().ToString("F0");
            //textGradi.Text = temp.Read().ToString("F1");
            //textRH.Text = rh.Read().ToString("F0");
            //textPPM.Text = co2.Read().ToString("F0");
            //textUT.Text = moisture.Read().ToString("F0");
            //textVoltmetro.Text = adc.ReadVolt(5).ToString("F1");
        }

        private static void test()
        {   // per test Adc: definizione del convertitore analogico digitale
            DigitalConverterMCP3208 adc = new DigitalConverterMCP3208();

            // per test IO digitale: definizione di canali di IO digitale
            const int chanLed1 = 40;
            const int chanLed2 = 38;
            const int chanButton1 = 37;
            const int chanButton2 = 35;
            DigitalIO pinIn1 = new DigitalIO(chanButton1, GpioPinDriveMode.Input);
            DigitalIO pinIn2 = new DigitalIO(chanButton2, GpioPinDriveMode.Input);
            DigitalIO pinOut1 = new DigitalIO(chanLed1, GpioPinDriveMode.Output);
            DigitalIO pinOut2 = new DigitalIO(chanLed2, GpioPinDriveMode.Output);
            // si può anche fare così: 
            //DigitalIO pinIn1 = new DigitalIO(ConnectorPin.P1Pin37, GpioPinDriveMode.Input);
            //DigitalIO pinOut1 = new DigitalIO(ConnectorPin.P1Pin40, GpioPinDriveMode.Output);
            //DigitalIO pinIn2 = new DigitalIO(ConnectorPin.P1Pin35, GpioPinDriveMode.Input);
            //DigitalIO pinOut2 = new DigitalIO(ConnectorPin.P1Pin38, GpioPinDriveMode.Output);

            // definizioni per test output digitali su shift register
            OutShiftRegister shift = new OutShiftRegister(16, 25, 24, 23);
            Device_OnOffShift irrigator = new Device_OnOffShift(shift, 8);    // ottavo pin dello shift register
            Device_OnOffShift humidifier = new Device_OnOffShift(shift, 2);   // secondo pin dello shift register

            //TestAllDigitalInputs(pinTuttiIn);
            //return;

            while (true)
            {
                Console.CursorTop = 0;
                Console.Clear();

                ReadAllAdc();

                // test two digital inputs
                Console.WriteLine("Canale bottone 1: {0}, Canale bottone 2: {1} ",
                                    chanButton1, chanButton2);
                Console.WriteLine("Bottone 1: {0}, Bottone 2: {1} ",
                    pinIn1.Read().ToString(), pinIn2.Read().ToString());
                // test two digital outputs
                Console.WriteLine("\r\nCanale LED 1: {0}, Canale LED 2: {1} ",
                                    chanLed1, chanLed2);
                pinOut1.Write(pinIn1.Read());
                pinOut2.Write(pinIn2.Read());

                ActuateShiftRegister();

                Thread.Sleep(500);
            }
        }

        private static void ActuateShiftRegister()
        {
            shift[0] = true;
            shift[1] = true;
            shift.OutBits();
        }

        private static void ReadAllAdc()
        {
            // Test ADC
            Console.WriteLine("Letture ADC");
            for (int i = 0; i < 8; i++)
                Console.WriteLine("Canale:{0} Punti: {1}", i, adc.ReadValue(i));
            //Console.WriteLine("Punti: {0}\tVolt: {1}", adc.ReadValue(i), adc.ReadVolt(i));
            Console.WriteLine();
        }

        private static void TestAllDigitalInputs()
        {
            // Test all Digital Inputs
            List<DigitalIO> pinTuttiIn = RicercaPin();
            Console.WriteLine("\r\n\r\nAll pins");
            while (true)
            {
                foreach (DigitalIO input in pinTuttiIn)
                {
                    Console.Write("{0}:{1}|", Enum.GetName(typeof(ConnectorPin), input.connectorPin)
                        , input.Read());
                }
            }
        }

        private static List<DigitalIO> RicercaPin()
        {
            // ricerca di tutti i pin possibili nella Raspberry che stiamo usando
            List<DigitalIO> pinTuttiIn = new List<DigitalIO>();

            foreach (ConnectorPin pin in Enum.GetValues(typeof(ConnectorPin)))
            {
                try
                {
                    pinTuttiIn.Add(new DigitalIO(pin, GpioPinDriveMode.Input));
                    Console.Write("{0} ", Enum.GetName(typeof(ConnectorPin), pin));
                }
                catch
                { // se quel pin non c'è nel Raspberry che uso, dà errore
                  // non aggiungo niente alla lista dei canali
                }
            }
            return pinTuttiIn; 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GrowBoxShared;

namespace Test_Device_OnOffShift
{
    class Program
    {
        static void Main(string[] args)
        {
            Device_OnOffShift a = new Device_OnOffShift(new OutShiftRegister(16, 25, 24, 23), 2/* num bit*/);
            a.WriteLog += A_WriteLog;

            const int chanLed1 = 40;
            const int chanLed2 = 38;
            const int chanButton1 = 37;
            const int chanButton2 = 35;
            DigitalIO pinIn1 = new DigitalIO(chanButton1, GpioPinDriveMode.Input);
            DigitalIO pinIn2 = new DigitalIO(chanButton2, GpioPinDriveMode.Input);
            DigitalIO pinOut1 = new DigitalIO(chanLed1, GpioPinDriveMode.Output);
            DigitalIO pinOut2 = new DigitalIO(chanLed2, GpioPinDriveMode.Output);

            for (bool running = true; running;)
            {

                Console.WriteLine("Selezionare l'opzione desiderata tramite numero:");
                Console.WriteLine("\t1- On");
                Console.WriteLine("\t2.-Off");
                Console.WriteLine("\t3-On temporizzato");
                Console.WriteLine("\t4-Spegni e chiudi");
                string s = Console.ReadLine();
                if (s == "3")
                {
                    int time = 0;
                    bool ok;
                    do
                    {
                        Console.WriteLine("Inserire il tempo(ms)... ");
                        ok = int.TryParse(Console.ReadLine(), out time);
                        if (!ok)
                        {
                            Console.WriteLine("Errore nella stringa di input");
                        }
                    } while (!ok);
                    a.TimeOn(time);
                    pinOut1.Write(GpioPinValue.High);
                    Task.Delay(time);
                    pinOut1.Write(GpioPinValue.Low);
                }
                else if (s == "2")
                {
                    a.Off();
                    pinOut1.Write(GpioPinValue.Low);
                }
                else if (s == "1")
                {
                    a.On();
                    pinOut1.Write(GpioPinValue.High);
                }
                else if (s == "4")
                {
                    a.Off();
                    pinOut1.Write(GpioPinValue.Low);
                    running = false;
                }
            }
        }

        private static void A_WriteLog(string line)
        {
            FileLogger fl = new FileLogger("test_deviceOnOffShift.txt");
            fl.WriteLogger(line);
        }
    }
}

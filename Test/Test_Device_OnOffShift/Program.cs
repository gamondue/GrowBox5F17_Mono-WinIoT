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
            const int NUM_BITS = 16;
            const int DATA_PIN = 16;
            const int CLOCK_PIN = 16;
            const int LATCH_PIN = 16;

            const int ONOFFSHIFT_BIT = 16;

            OutShiftRegister reg = new OutShiftRegister(NUM_BITS, DATA_PIN, CLOCK_PIN, LATCH_PIN);
            Device_OnOffShift tester = new Device_OnOffShift(reg, ONOFFSHIFT_BIT);
            tester.On();
            Console.WriteLine("Tester on");
            Task.Delay(1000);
            tester.Off();
            Console.WriteLine("Tester off");
            Task.Delay(500);
            Console.WriteLine("Tester on for 2 seconds");
            tester.TimeOn(2000);
            Task.Delay(2000);
            Console.WriteLine("Test termined");
        }
    }
}

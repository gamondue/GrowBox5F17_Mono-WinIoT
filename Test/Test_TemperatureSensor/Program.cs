using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowBoxShared;
using System.Threading;

//Test_TemperatureSensor
namespace Test_TemperatureSensor
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitalConverterMCP3208 ADC = new DigitalConverterMCP3208();
            TemperatureSensor tmp = new TemperatureSensor(ADC, 3);

            while (true)
            {
                Console.WriteLine(tmp.Read());
                Thread.Sleep(500);
            }
            
        }
    }
}

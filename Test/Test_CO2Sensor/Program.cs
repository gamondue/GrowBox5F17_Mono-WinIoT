using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowBoxShared;

namespace Test_CO2Sensor
{
    class Program
    {
        //TODO assegnare il lavoro ad un gruppo !!!!!
        static void Main(string[] args)
        {
            CO2Sensor sensor = new CO2Sensor(new DigitalConverterMCP3208(), 3);

            while(true)
            {
                Console.WriteLine(sensor.Read());
            }
        }
    }
}

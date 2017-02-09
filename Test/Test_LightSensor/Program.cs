using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using GrowBoxShared;

namespace GrowBoxShared
{
    public class Program
    {
        static void Main(string[] args)
        {
            LightSensor l = new LightSensor(new DigitalConverterMCP3208(), 3);
            Console.WriteLine(l.Read());
            
        }
    }
}

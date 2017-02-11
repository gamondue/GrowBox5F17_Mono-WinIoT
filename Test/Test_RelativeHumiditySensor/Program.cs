using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowBoxShared;
using System.Threading;

//Test_RelativeHumiditySensor
namespace Test_RelativeHumiditySensor
{
    class Program
    {
        static void Main(string[] args)
        {
            RelativeHumiditySensor hum = new RelativeHumiditySensor(new DigitalConverterMCP3208(), 3, 5.0);

            while (true)
            {
                Console.WriteLine(hum.Read());
                Thread.Sleep(500);
            }
            
        }
    }
}

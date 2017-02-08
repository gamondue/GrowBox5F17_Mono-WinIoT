using GrowBoxShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_PWM
{
    class Program
    {
        static void Main(string[] args)
        {
            PwmDevice pwm = new PwmDevice();
            DigitalConverterMCP3208 adc = new GrowBoxShared.DigitalConverterMCP3208();
            TemperatureSensor t = new TemperatureSensor(adc, 3);
        }
    }
}

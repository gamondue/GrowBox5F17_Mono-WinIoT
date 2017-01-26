using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class TemperatureSensor
    {
        private int _ch;
        private double _voltSensore;
        private DigitalConverterMCP3208 _adc;

        public TemperatureSensor(DigitalConverterMCP3208 Adc, int ch, double voltSensore = 3.3)
        {
            _ch = ch;
            _voltSensore = voltSensore;
            _adc = Adc;
        }

        public double Read()
        {
            double a = -51.94713;
            double b = 0.304941;
            double c = 2.10E+10;
            double d = 152566.7;

            return (d + ((a - d) / (1 + Math.Pow((_adc.ReadVolt(_ch, _voltSensore) / c), b))));
        }

    }
}

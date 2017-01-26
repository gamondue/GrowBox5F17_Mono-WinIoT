using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRaspGrowBox
{
    class TemperatureSensor : DigitalConverterMCP3208
    {
        private int _ch;
        private double _voltSensore;

        public TemperatureSensor(int ch, double voltSensore = 3.3, SpiDeviceHandler sdh = null) : base(sdh)
        {
            _ch = ch;
            _voltSensore = voltSensore;
        }

        public double Read()
        {
            double a = -51.94713;
            double b = 0.304941;
            double c = 2.10E+10;
            double d = 152566.7;

            return (d + ((a - d) / (1 + Math.Pow((ReadVolt(_ch, _voltSensore) / c), b))));
        }

    }
}

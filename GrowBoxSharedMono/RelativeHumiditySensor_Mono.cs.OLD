using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class RelativeHumiditySensor
    {
        private int _ch;
        private double _voltSensore;
        private DigitalConverterMCP3208 _adc;

        public RelativeHumiditySensor(DigitalConverterMCP3208 Adc, int ch, double voltSensore = 3.3)
        {
            _ch = ch;
            _voltSensore = voltSensore;
            _adc = Adc;
        }



        public double Read()
        {

            const double A = 52.59517;
            const double B = 15.46814;
            const double C = 3.550823;
            const double D = 73.81925;

            double umiditaRelativa = D + ((A - D) / (1.0 + Math.Pow((_adc.ReadVolt(_ch, _voltSensore) / C), B)));

            if (umiditaRelativa > 100) umiditaRelativa = 100;
            if (umiditaRelativa < 0) umiditaRelativa = 0;

            return (umiditaRelativa);
        }
    }
}

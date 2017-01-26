using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class SoilMoistureSensor
    {
        private int _ch;
        private double _voltSensore;
        private DigitalConverterMCP3208 _adc;

        public SoilMoistureSensor(DigitalConverterMCP3208 Adc, int ch, double voltSensore = 3.3)
        {
            _ch = ch;
            _voltSensore = voltSensore;
            _adc = Adc;
        }

        public double Read()
        {
            const double MINVOLT = 2.25;   // Min volt allagato     
            const double MAXVOLT = 3.3;    // Max volt secco
            //Considera il sensore lineare ma è da verificare
            double umiditaTerreno = 100.0 - ((_adc.ReadVolt(_ch, _voltSensore) - MINVOLT) * (100.0 / (MAXVOLT - MINVOLT)));
            //Tronca eventuali imprecisioni del convertitore A/D
            if (umiditaTerreno > 100.0) umiditaTerreno = 100;
            if (umiditaTerreno < 0.0) umiditaTerreno = 0;

            return (umiditaTerreno);
        }

    }
}

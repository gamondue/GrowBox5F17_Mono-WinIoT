using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class CO2Sensor
    {
        private int _ch;
        private double _voltSensore;
        private DigitalConverterMCP3208 _adc;

        public CO2Sensor(DigitalConverterMCP3208 Adc, int ch, double voltSensore = 3.3)
        {
            _ch = ch;
            _voltSensore = voltSensore;
            _adc = Adc;
        }



        public double Read()
        {
            // http://www.robotshop.com/media/files/zip/documentation-sen0159.zip
            // http://www.dfrobot.com/wiki/index.php/CO2_Sensor_SKU:SEN0159

            //Formula presa dai link sul web
            const double ZERO_POINT_VOLTAGE = 0.324; //define the output of the sensor in volts when the concentration of CO2 is 400PPM
            const double REACTION_VOLTGAE = 0.020; //define the voltage drop of the sensor when move the sensor from air into 1000ppm CO2
            double[] CO2Curve = { 2.602, ZERO_POINT_VOLTAGE, (REACTION_VOLTGAE / (2.602 - 3.0)) };
            const double DC_GAIN = 8.5;   //define the DC gain of amplifier

            double volts = _adc.ReadVolt(_ch, _voltSensore);

            //if ((volts / DC_GAIN) >= ZERO_POINT_VOLTAGE)
            //{
            //   return -1;
            //} 
            //else

            return Math.Pow(10.0, ((volts / DC_GAIN) - CO2Curve[1]) / CO2Curve[2] + CO2Curve[0]);

        }






    }
}

using Raspberry.IO.Components.Converters.Mcp3208;
using Raspberry.IO.GeneralPurpose;
using System;

namespace GrowBoxShared
{
    public class DigitalConverterMCP3208:IDisposable 
    {
        private const int NUM_LETT = 10;// Numero di letture sulla porta analogica

        const ConnectorPin SPI_SCLK = ConnectorPin.P1Pin23;
        const ConnectorPin SPI_MISO = ConnectorPin.P1Pin21;
        const ConnectorPin SPI_MOSI = ConnectorPin.P1Pin19;
        const ConnectorPin SPI_CS = ConnectorPin.P1Pin24;

        GpioConnectionDriver gpioDriver = new GpioConnectionDriver();

        Mcp3208SpiConnection adcConnection;

        public static ConnectorPin SPI_SCLK1
        {
            get
            {
                return SPI_SCLK;
            }
        }

        public DigitalConverterMCP3208()
        {
            adcConnection = new Mcp3208SpiConnection(
                gpioDriver.Out(SPI_SCLK),
                gpioDriver.Out(SPI_CS),
                gpioDriver.In(SPI_MISO),
                gpioDriver.Out(SPI_MOSI)); 
        }

        public int ReadValue(int channel) 
        {
            //Console.WriteLine("Channel: {0}", ((Mcp3208Channel)(channel)).ToString());
            return (int)adcConnection.Read((Mcp3208Channel)channel).Value; 
        }

        public double ReadVolt(int ch = 0, double volt = 3.3)
        {
            double unita = 0.0;
            for (int i = 0; i < NUM_LETT; i++) unita = unita + ReadValue(ch);
            return ((unita / NUM_LETT) * (volt / 4096.0));
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            adcConnection.Close();
        }
    }
}

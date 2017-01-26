using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GrowBoxShared
{
    class DigitalConverterMCP3208
    {
        private SpiDeviceHandler _sdh;

        private const int NUM_LETT = 10;// Numero di letture sulla porta analogica

        public DigitalConverterMCP3208()
        {
            // Crea un gestore del bus SPI utilizzando valori di default per le sue impostazioni.
            SpiDeviceHandler _sdh = new SpiDeviceHandler();
            if (_sdh == null) throw new Exception("SpiDeviceHandler non inizializzato.");
        }

        /*   *************************************************   Codice per  MCP3008    *************************************************
        /// <summary>
        /// Legge la tensione in millivolt rilevata dall'ADC.
        /// </summary>
        /// <param name="channel">Canale dell'ADC, default = 0.</param>
        /// <returns>Restituisce la tensione rilevata in mV.</returns>
        public virtual ushort Read(int channel = 0)
        {
            // TODO: 0x80 | pare superfluo.
            byte ch = (byte)(0x80 | (8 + channel) << 4);

            // Codifica del comando per l'ADC MCP3008
            //      1° byte: bit 0 = 1, per "svegliare” l'ADC, altri bit = 0
            //      2° byte:
            //          bit 7 = 1 per utilizzare l'input singolo dai pin dei canali del chip
            //                  0 per utilizzare l'input differenziale da coppie pin di canali consecutivi del chip
            //          bit 6 5 4 identificano il canale di input del chip (8 canali)
            //          bit rimanenti = 0
            //      3° byte: 0x00, serve perché il comando deve avere lo stesso numero di byte di quelli poi ricevuti
            var transmitBuffer = new byte[3] { 1, ch, 0x00 };

            // Codifica della risposta
            //      1° byte: contenuto dell'MCU, non serve
            //      2° byte: i bit 1 e 0 contengono i due bit "più alti" dei 10 bit restituiti dall'ADC
            //      3° byte: 8 bit "bassi" dei 10 bit restituiti dall'ADC
            var receiveBuffer = new byte[3];

            _sdh.WriteReadFD(transmitBuffer, receiveBuffer);

            return (ushort)(((receiveBuffer[1] & 3) << 8) + receiveBuffer[2]);
        }
        */

        public int ReadValue(int ch)
        {
            int adcValue;

            byte[] readBuffer = new byte[3]; /* Buffer to hold read data*/
            byte[] writeBuffer = new byte[3] { 0x00, 0x00, 0x00 };

            writeBuffer[0] = 0x06;
            // Si può ottimizzare 
            switch (ch)
            {
                case 0:
                    writeBuffer[0] = 0x06;
                    writeBuffer[1] = 0x00;
                    break;
                case 1:
                    writeBuffer[0] = 0x06;
                    writeBuffer[1] = 0x40;
                    break;
                case 2:
                    writeBuffer[0] = 0x06;
                    writeBuffer[1] = 0x80;
                    break;
                case 3:
                    writeBuffer[0] = 0x06;
                    writeBuffer[1] = 0xc0;
                    break;
                case 4:
                    writeBuffer[0] = 0x07;
                    writeBuffer[1] = 0x00;
                    break;
                case 5:
                    writeBuffer[0] = 0x07;
                    writeBuffer[1] = 0x40;
                    break;
                case 6:
                    writeBuffer[0] = 0x07;
                    writeBuffer[1] = 0x80;
                    break;
                case 7:
                    writeBuffer[0] = 0x07;
                    writeBuffer[1] = 0xc0;
                    break;
                default:
                    throw new System.ArgumentException("Numero canale errato");

            }

            _sdh.WriteReadFD(writeBuffer, readBuffer); /* Read data from the ADC                           */
            adcValue = ConvertToInt(readBuffer);                /* Convert the returned bytes into an integer value */
            return (adcValue);
        }



        /* Convert the raw ADC bytes to an integer */
        private int ConvertToInt(byte[] data)
        {
            int result = 0;
            // Si può ottimizzare 
            result = data[1] & 0x0F;
            result <<= 8;
            result += data[2];

            return result;
        }

        public double ReadVolt(int ch = 0, double volt = 3.3)
        {
            double unita = 0.0;
            for (int i = 0; i < NUM_LETT; i++) unita = unita + ReadValue(ch);
            return ((unita / NUM_LETT) * (volt / 4096.0));
        }
    }
}

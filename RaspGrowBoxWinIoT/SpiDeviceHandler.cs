using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Spi;

namespace AppRaspGrowBox
{
    class SpiDeviceHandler
    {

        // Memorizza l'SpiDevice impostato per l'occorrenza della classe.
        private SpiDevice _spiDevice = null;

        public SpiDeviceHandler(int chipSelect = 0, int clock = 3600000,
                                SpiMode mode = SpiMode.Mode0, string controllerName = "SPI0")
        {
            // Anche se il metodo è dichiarato con async non è possibile invocarlo con await perché,
            //giustamente, non è possibile attendere che sia restituito qualcosa (che è quello che fa await)
            // in quanto non restituirà nulla.
            InitSPI(chipSelect, clock, mode, controllerName);
        }


        private async void InitSPI(int chipSelect, int clock, SpiMode mode, string controllerName)
        {
            try
            {

                //await Task.Delay(2000);

                var settings = new SpiConnectionSettings(chipSelect);
                settings.ClockFrequency = clock;   // 0.5MHz clock rate                                       
                settings.Mode = mode;      // The ADC expects idle-low clock polarity so we use Mode0  

                var controller = await SpiController.GetDefaultAsync();

                _spiDevice = controller.GetDevice(settings);
            }

            // If initialization fails, display the exception and stop running 
            catch (Exception ex)
            {
                throw new Exception("SPI Initialization Failed", ex);
            }
        }

        /*
        private async void InitSPI(int busIndex, int clock, SpiMode mode, string controllerName)
        {
            var spiSettings = new SpiConnectionSettings(busIndex);
            spiSettings.ClockFrequency = clock;
            spiSettings.Mode = mode;

            string spiQuery = SpiDevice.GetDeviceSelector(controllerName);

            // Usa await solo perché i metodi che, per convenzione, terminano con Async di norma restituiscono
            // Task<tipo>. Invocandoli in questo modo si ottiene direttamente il valore restituito di tale tipo,
            // anziché Task<tipo>.
            // L'invocazione in tal caso è bloccante per cui non si attiva alcun multithreading!
            var deviceInfo = await DeviceInformation.FindAllAsync(spiQuery);
            if (deviceInfo != null && deviceInfo.Count > 0)
                _spiDevice = null; // = await SpiDevice.FromIdAsync(deviceInfo[0].Id, spiSettings);
            //else
            //    throw new Exception("SPIDevice non inizializzato.");
        }
        */

        /// <summary>
        /// Trasmette e riceve in full duplex tramite i buffer indicati, in pratica un wrapper.
        /// </summary>
        /// <param name="w">Buffer di scrittura.</param>
        /// <param name="r">Buffer di lettura.</param>
        public void WriteReadFD(byte[] w, byte[] r)
        {

            _spiDevice.TransferFullDuplex(w, r); // Read data from the ADC                

        }

    }
}

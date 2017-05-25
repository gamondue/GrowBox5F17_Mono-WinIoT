using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Gpio;

namespace GrowBoxShared
{
    /// <summary>
     /// Pin di I/O digitale, programmabile in Input od in Output
     /// Realizza l'astrazione di un IO digitale per la piattaforma desiderata
     /// Versione per Win 10 for IoT
     /// </summary>
    class DigitalIO
    {
        private GpioPin _GpioPin;
        // commento inutile

        /// <summary>
        /// Costruttore che richiede il canale da usare e la direzione dell'IO
        /// </summary>
        /// <param name="NumConnectorPin">
        /// Numero del canale 
        /// E' il numero del piedino nel connettore Gpio del Raspberry Pi, NON quello 
        /// del SoC Broadcom
        /// </param>
        /// <param name="IsInput">
        /// Direzione dell'IO: se true è un Input, altrimenti è un output
        /// </param>
        public DigitalIO(int NumConnectorPin, GpioPinDriveMode DriveMode)
        {
            var gpio = GpioController.GetDefault();
            if (gpio == null) throw new Exception("GPIO Initialization Failed");

            _GpioPin = gpio.OpenPin(NumConnectorPin);
            _GpioPin.Write(GpioPinValue.Low); // sarebbe meglio evitare, chiedere a Lombardi
            _GpioPin.SetDriveMode(DriveMode);
        }
        public GpioPinValue Read()
        {
            return _GpioPin.Read(); 
        }

        public void Write(GpioPinValue value)
        {
            _GpioPin.Write(value);
        }

        public void Wait(bool waitForUp, System.TimeSpan Timeout)
        {
            throw new NotImplementedException("Wait: attesa per un livello di IO non utizzabile nella libreria WinIoT"); 
        }
    }
}

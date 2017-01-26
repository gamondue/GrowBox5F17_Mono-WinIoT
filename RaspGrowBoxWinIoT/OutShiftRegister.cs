using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Gpio;

namespace AppRaspGrowBox
{
    class OutShiftRegister
    {
        private bool[] bit; // Array contenente i bit per lo shift register
                            //private OutputPort dataPin;  // Pin connesso al DS del 74HC595 
                            //private OutputPort clockPin; // Pin connesso al SH_CP del 74HC595 
                            //private OutputPort latchPin; // Pin connesso al ST_CP del 74HC595

        private GpioPin _pinData;
        private GpioPin _pinClock;
        private GpioPin _pinLatch;

        /// <summary>
        /// Inizializzazione
        /// </summary>
        /// <param name="bits">Numero di bit: 8 X Numero di integrati 74HC595</param>
        /// <param name="dataPin">Porta linea dati DS Pin 14 del 74HC595</param>
        /// <param name="clockPin">Porta linea clock SH_CP Pin 11 del 74HC595</param>
        /// <param name="latchPin">Porta linea latch ST_CP Pin 12 del 74HC595</param>
        public OutShiftRegister(int numBits, int dataPin, int clockPin, int latchPin)
        {
            if (numBits <= 0) throw new IndexOutOfRangeException("ShiftRegister: numero di bit < 0");

            var gpio = GpioController.GetDefault();
            if (gpio == null) throw new Exception("GPIO Initialization Failed");

            _pinData = gpio.OpenPin(dataPin);
            _pinClock = gpio.OpenPin(clockPin);
            _pinLatch = gpio.OpenPin(latchPin);

            _pinData.Write(GpioPinValue.Low);
            _pinClock.Write(GpioPinValue.Low);
            _pinLatch.Write(GpioPinValue.Low);

            _pinData.SetDriveMode(GpioPinDriveMode.Output);
            _pinClock.SetDriveMode(GpioPinDriveMode.Output);
            _pinLatch.SetDriveMode(GpioPinDriveMode.Output);

            bit = new bool[numBits];

            //this.dataPin = new OutputPort((Cpu.Pin)dataPin, false);
            //this.clockPin = new OutputPort((Cpu.Pin)clockPin, false);
            //this.latchPin = new OutputPort((Cpu.Pin)latchPin, false);
            OutResetBits();
        }
        /// <summary>
        /// get/set un bit dell'array da inviare allo shift register.
        /// </summary>
        /// <param name="i">Numero di bit del get/set</param>
        /// <returns>Restituisce il valore di un bit</returns>
        public bool this[int i]
        {
            get
            {
                if ((i >= 0) && (i < bit.Length)) return (bit[i]);
                throw new IndexOutOfRangeException("ShiftRegister: Indice fuori range");
            }
            set
            {
                if ((i >= 0) && (i < bit.Length)) bit[i] = value;
                else throw new IndexOutOfRangeException("ShiftRegister: Indice fuori range");
            }
        }
        /// <summary>
        /// Memorizza false su tutti i bit, li pone in uscita
        /// </summary>
        public void OutResetBits()
        {
            for (int i = 0; i < bit.Length; i++)
                bit[i] = false;
            OutBits();
        }
        /// <summary>
        /// Memorizza true su tutti i bit,  li pone in uscita 
        /// </summary>
        public void OutSetBits()
        {
            for (int i = 0; i < bit.Length; i++)
                bit[i] = true;
            OutBits();
        }
        /// <summary>
        /// Invia i bit in uscita
        /// </summary>
        public void OutBits()
        {
            for (int i = (bit.Length - 1); i >= 0; i--)
            {
                if (bit[i]) _pinData.Write(GpioPinValue.High);
                else _pinData.Write(GpioPinValue.Low);
                //Wait();
                _pinClock.Write(GpioPinValue.High);
                //Wait();
                _pinClock.Write(GpioPinValue.Low);
                //Wait();
            }
            _pinLatch.Write(GpioPinValue.High);
            //Wait();
            _pinLatch.Write(GpioPinValue.Low);
        }
    }
}

﻿using System;

#if NETFX_CORE
using Windows.Devices.Gpio;
#endif

namespace GrowBoxShared
{
    class OutShiftRegister
    {
        private bool[] bit; // Array contenente i bit per lo shift register
                            //private OutputPort dataPin;  // Pin connesso al DS del 74HC595 
                            //private OutputPort clockPin; // Pin connesso al SH_CP del 74HC595 
                            //private OutputPort latchPin; // Pin connesso al ST_CP del 74HC595

        private DigitalIO _pinData;
        private DigitalIO _pinClock;
        private DigitalIO _pinLatch;

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

            _pinData = new DigitalIO(dataPin, GpioPinDriveMode.Output);
            _pinClock = new DigitalIO(clockPin, GpioPinDriveMode.Output);
            _pinLatch = new DigitalIO(latchPin, GpioPinDriveMode.Output);

            _pinData.Write(GpioPinValue.Low);
            _pinClock.Write(GpioPinValue.Low);
            _pinLatch.Write(GpioPinValue.Low);

            bit = new bool[numBits];

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

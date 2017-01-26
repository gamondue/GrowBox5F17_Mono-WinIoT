using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class Device_OnOffShift : Device
    {
        //private int _timeOn; // Tempo di on in millisecondi
        private OutShiftRegister _shift;
        private int _numBit;

        public Device_OnOffShift(OutShiftRegister shift, int numBit)
        {
            _shift = shift;
            _numBit = numBit;
            _shift[numBit] = false;
            _shift.OutBits();
        }

        /// <summary>
        /// Accende il dispositivo
        /// </summary>
        public void On()
        {
            InvokeWriteLog("ShiftRegister " + _numBit + " ON");
            _shift[_numBit] = true;
            _shift.OutBits();
        }
        /// <summary>
        /// Spegne il dispositivo
        /// </summary>
        public void Off()
        {
            InvokeWriteLog("ShiftRegister " + _numBit + " OFF");
            _shift[_numBit] = false;
            _shift.OutBits();
        }

        /// <summary>
        /// ON temporizzato
        /// </summary>
        /// <param name="millesecondi">Tempo di ON</param>
        public void TimeOn(int millesecondi)
        {
            _shift[_numBit] = true;
            InvokeWriteLog("ShiftRegister " + _numBit + " ON");
            _shift.OutBits();
            Task.Delay(millesecondi).Wait();
            _shift[_numBit] = false;
            InvokeWriteLog("ShiftRegister " + _numBit + " OFF");
            _shift.OutBits();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRaspGrowBox
{
    class Device
    {
        //Creo il delegato prende come argomento il nuovo valore della stringa
        public delegate void EventDelegate(string Line);

        //Creo l'evento per scrivere sul file log N.B. non può essere ereditato
        public event EventDelegate WriteLog;

        // Metodo per invocare indirettamente l'evento  N.B. può essere ereditato
        protected /*virtual*/ void InvokeWriteLog(string Line)
        {
            if (WriteLog != null) //Controllare prima se è null
                WriteLog?.Invoke(Line);
        }
    }
}

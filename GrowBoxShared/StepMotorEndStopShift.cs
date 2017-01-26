using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.Devices.Gpio;
#endif

//using Windows.Devices.Gpio;

namespace GrowBoxShared
{
    class StepMotorEndStopShift : Device
    {
        private OutShiftRegister _shift;
        private int _bitA1;
        private int _bitA2;
        private int _bitB1;
        private int _bitB2;
        private DigitalIO _endStopOr, _endStopAn; // Porte collegate ai fine corsa
        private int _fase;// Numero di fase
        /// <summary>
        /// inizializzazione dispositivo
        /// </summary>
        /// <param name="portaEndStopOrario">Porta collegata all fine corsa sulla rotazione oraria false quando premuto</param>
        /// <param name="portaEndStopAntiOrario">Porta collegata all fine corsa sulla rotazione anti-oraria false quando premuto</param>
        public StepMotorEndStopShift(
            OutShiftRegister shift,
            int bitA1,
            int bitA2,
            int bitB1,
            int bitB2,
            int portaEndStopOrario,
            int portaEndStopAntiOrario)
        {
            _shift = shift;
            _bitA1 = bitA1;
            _bitA2 = bitA2;
            _bitB1 = bitB1;
            _bitB2 = bitB2;

            //var gpio = GpioController.GetDefault();
            //if (gpio == null) throw new Exception("GPIO Initialization Failed");

            _endStopOr = new DigitalIO(portaEndStopOrario, GpioPinDriveMode.InputPullUp);
            _endStopAn = new DigitalIO(portaEndStopAntiOrario, GpioPinDriveMode.InputPullUp);

            _fase = 0;
            Folle();
        }

        /// <summary>
        /// Effettua la rotazione oraria di "s" passi tenendo conto del fine-corsa
        /// </summary>
        /// <param name="s">Numero di passi >=0</param>
        /// <returns>"false" arrivato a fine corsa "true" non arrivato a fine corsa</returns>
        public bool StepOrario(int s)
        {
            if (_endStopOr.Read() == GpioPinValue.Low) return (false);//Esce se arrivato a fine corsa
            for (int i = 0; i < s; i++) //Effettua i passi richiesti
            {
                if (_endStopOr.Read() == GpioPinValue.Low) return (false);//Esce se arrivato a fine corsa
                _fase++; //Passa alla fase successiva
                _fase = _fase % 4;//Rotazione fasi 0-1-2-3
                AttivaFase();

            }
            //Toglie alimentazione alle fasi
            Folle();
            //Thread.Sleep(3); 
            return (true);//Non è arrivato al fine corsa
        }
        /// <summary>
        /// Effettua la rotazione anti-oraria di "s" passi tenendo conto del fine-corsa
        /// </summary>
        /// <param name="s">Numero di passi >=0</param>
        /// <returns>"false" arrivato a fine corsa "true" non arrivato a fine corsa</returns>
        public bool StepAntiOrario(int s)
        {
            if (_endStopAn.Read() == GpioPinValue.Low) return (false);//Esce se arrivato a fine corsa
            for (int i = 0; i < s; i++) //Effettua i passi richiesti
            {
                if (_endStopAn.Read() == GpioPinValue.Low) return (false);//Esce se arrivato a fine corsa
                _fase--;//Passa alla fase precedente
                if (_fase < 0) _fase = 3;//Rotazione fasi 3-2-1-0
                AttivaFase();

            }
            //Toglie alimentazione alle fasi
            Folle();
            //Thread.Sleep(3); 
            return (true);//Non è arrivato al fine corsa
        }
        /// <summary>
        /// Frena il motore alla fase attuale
        /// </summary>
        /// <param name="attivato">"true" frena il motore "false" motore in folle</param>
        public void Freno(bool attivato)
        {
            if (attivato) AttivaFase();
            else Folle(); //Toglie alimentazione la fase attuale
        }

        private void AttivaFase()
        {
            switch (_fase)//Seleziona la fase 
            {
                case 0://Fase 0
                    _shift[_bitA1] = true;
                    _shift[_bitA2] = false;
                    _shift[_bitB1] = false;
                    _shift[_bitB2] = false;
                    _shift.OutBits();
                    break;
                case 1://Fase 1
                    _shift[_bitA1] = false;
                    _shift[_bitA2] = false;
                    _shift[_bitB1] = true;
                    _shift[_bitB2] = false;
                    _shift.OutBits();
                    break;
                case 2://Fase 2

                    _shift[_bitA1] = false;
                    _shift[_bitA2] = true;
                    _shift[_bitB1] = false;
                    _shift[_bitB2] = false;
                    _shift.OutBits();
                    break;
                case 3://Fase 3
                    _shift[_bitA1] = false;
                    _shift[_bitA2] = false;
                    _shift[_bitB1] = false;
                    _shift[_bitB2] = true;
                    _shift.OutBits();
                    break;
            }
            //L'albero motore deve avere il tempo di raggiungere la nuova posizione
            Task.Delay(20).Wait();// "1" per Stepper Motor Nema 17 "3" M42SP-5
        }

        public void Folle()
        {
            _shift[_bitA1] = false;
            _shift[_bitA2] = false;
            _shift[_bitB1] = false;
            _shift[_bitB2] = false;
            _shift.OutBits();
        }

        public void PosizionaFineCorsaOrario()
        {
            while (StepOrario(1)) ; //Si posiziona a fine-corsa anti oraria
            Folle();
        }

        public void PosizionaFineCorsaAntiOrario()
        {
            while (StepAntiOrario(1)) ; //Si posiziona a fine-corsa anti oraria 
            Folle();
        }
    }
}

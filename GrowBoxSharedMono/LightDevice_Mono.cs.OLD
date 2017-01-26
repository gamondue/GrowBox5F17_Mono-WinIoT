using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class LightDevice : StepMotorEndStopShift
    {
        private int _stepTotali; //Numero di step presenti fra un fine corsa e l'altro   
        private int _percentuale; //Percentuale di illuminazione attuale
        private int _posizioneAttualeInStep; //Posizione attuale del servo in numero di step
        private OutShiftRegister _shift;
        private int _bitLampada;
        /// <summary>
        /// Rotazione oraria apre la persiana paralume. Rotazione anti-oraria chiude la persiana paralume
        /// </summary>
        /// <param name="portaA1"></param>
        /// <param name="portaA2"></param>
        /// <param name="portaB1"></param>
        /// <param name="portaB2"></param>
        /// <param name="portaEndStopOrario"></param>
        /// <param name="portaEndStopAntiOrario"></param>
        /// <param name="portaOnOffLuce"></param>
        public LightDevice(
            OutShiftRegister shift,
            int bitA1,
            int bitA2,
            int bitB1,
            int bitB2,
            int bitLampada,
            int portaEndStopOrario,
            int portaEndStopAntiOrario)
            : base(shift, bitA1, bitA2, bitB1, bitB2, portaEndStopOrario, portaEndStopAntiOrario)
        {
            _shift = shift;
            _bitLampada = bitLampada;
            shift[bitLampada] = false;
            shift.OutBits();
            //Conta numero di step fra un fine-corsa e l'altro
            PosizionaFineCorsaAntiOrario(); //Si posiziona a fine-corsa anti oraria apre completamente la persiana paralume
            _stepTotali = 0;
            while (StepOrario(1)) _stepTotali++; //Conta il numero di step fra i due finecorsa e chiude la persiana paralume
            _posizioneAttualeInStep = 0; //Aggiorna la posizione attuale
            _percentuale = 0;   //Aggiorna la percentuale attuale
            this.InvokeWriteLog("LightDevice " + _percentuale);
            Folle();
        }

        public void PercentualeApertura(int percentuale)
        {
            if (percentuale == 0)
            {
                PosizionaFineCorsaAntiOrario(); //Si posiziona a fine-corsa anti oraria chiude le persiane paralume
                _shift[_bitLampada] = false;
                _shift.OutBits();  //Spegne la lampada
                _posizioneAttualeInStep = 0;
            }
            else
                if (percentuale == 100)
            {
                _shift[_bitLampada] = true;
                _shift.OutBits();  //Accende la lampada
                PosizionaFineCorsaAntiOrario(); //Si posiziona a fine-corsa oraria apre le persiane paralume
                _posizioneAttualeInStep = _stepTotali;
            }
            else
                    if ((percentuale < 100) && (percentuale > 0))
            {
                PosizionaFineCorsaOrario(); //Si posiziona a fine-corsa anti oraria chiude le persiane paralume
                _shift[_bitLampada] = true;
                _shift.OutBits();  //Accende la lampada
                _posizioneAttualeInStep = (Int32)(((double)_stepTotali / 100.0) * (double)percentuale);
                StepAntiOrario(_posizioneAttualeInStep);
            }
            _percentuale = percentuale;
            this.InvokeWriteLog("LightDevice " + _percentuale);
            Folle();
        }
        /// <summary>
        /// Apre completamente la persiana para-luce e accende la lampada
        /// </summary>
        public void ApriCompletamente()
        {
            PosizionaFineCorsaOrario();
            _shift[_bitLampada] = true;
            _shift.OutBits();  //Accende la lampada
            _percentuale = 100;
            this.InvokeWriteLog("LightDevice " + _percentuale);
            _posizioneAttualeInStep = _stepTotali;
            Folle();
        }
        /// <summary>
        /// Chiude completamente la persiana para-luce e spegne la lampada
        /// </summary>
        public void ChiudiCompletamente()
        {
            PosizionaFineCorsaAntiOrario(); //Si posiziona a fine-corsa anti oraria chiude le persiane paralume
            _shift[_bitLampada] = false;
            _shift.OutBits();  //Spegne la lampada
            _percentuale = 0;
            this.InvokeWriteLog("LightDevice " + _percentuale);
            _posizioneAttualeInStep = 0;
            Folle();
        }

        public void OnLuce()
        {
            _shift[_bitLampada] = true;
            _shift.OutBits();  //Accende la lampada
            if (_percentuale == 0) _percentuale = 1;
            this.InvokeWriteLog("LightDevice " + _percentuale);
        }


        public void Incrementa()
        {
            if (_percentuale >= 100) return;
            if (_percentuale == 0)
            {   //Accende la lampada
                _shift[_bitLampada] = true;
                _shift.OutBits();
            }
            StepAntiOrario(1);
            _posizioneAttualeInStep++;
            _percentuale = (Int32)((100.0 / (double)_stepTotali) * (double)_posizioneAttualeInStep);
            this.InvokeWriteLog("LightDevice " + _percentuale);
        }


        public void Decrementa()
        {
            if (_percentuale <= 0) return;
            StepOrario(1);
            _posizioneAttualeInStep--;
            _percentuale = (Int32)((100.0 / (double)_stepTotali) * (double)_posizioneAttualeInStep);
            if (_percentuale == 0)
            {   //Spegne la lampada
                _shift[_bitLampada] = false;
                _shift.OutBits();
            }
            this.InvokeWriteLog("LightDevice " + _percentuale);
        }

        public int Percentuale
        {
            get
            {
                return (_percentuale);
            }
        }
    }
}

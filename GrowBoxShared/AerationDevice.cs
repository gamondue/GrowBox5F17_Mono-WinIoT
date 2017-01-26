using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class AerationDevice
    {
        /*
        private InputPort endStopClose;
        private InputPort endStopOpen;
        private InterruptPort encoder;
        private PWM ventola;
        private int step; //Contatore inpulsi encoder incrementale
        private int totStep; //Numero totale di impulsi fra un fine corsa e l'altro
        private OutShiftRegister shift;
        private int numBitChiudi;
        private int numBitApri;
        private double percentuale;


        public ModulaAreazioneShift(OutShiftRegister shift, int numBitChiudi, int numBitApri, FEZ_Pin.Digital endStopClose, FEZ_Pin.Digital endStopOpen, FEZ_Pin.Interrupt encoder, FEZ_Pin.PWM ventola)
        {
            this.shift = shift;
            this.numBitChiudi = numBitChiudi;
            this.numBitApri = numBitApri;
            this.endStopClose = new InputPort((Cpu.Pin)endStopClose, false, Port.ResistorMode.PullUp);
            this.endStopOpen = new InputPort((Cpu.Pin)endStopOpen, false, Port.ResistorMode.PullUp);
            this.encoder = new InterruptPort((Cpu.Pin)encoder, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeLow);
            this.encoder.OnInterrupt += new NativeEventHandler(encoder_OnInterrupt);
            this.ventola = new PWM((PWM.Pin)ventola);
            this.ventola.Set(false);
            ApriCompletamente();
            step = 0;
            ChiudiCompletamente();
            totStep = step;
            percentuale = 0;
        }

        private void encoder_OnInterrupt(uint port, uint state, DateTime time)
        {
            step++;
        }

        /// <summary>
        /// Apre completamente 
        /// </summary>
        private void ApriCompletamente()
        {
            shift[numBitApri] = true;
            shift.OutBits();
            while (endStopOpen.Read()) ;
            shift[numBitApri] = false;
            shift.OutBits();
        }

        /// <summary>
        /// Chiude completamente 
        /// </summary>
        private void ChiudiCompletamente()
        {
            ventola.Set(false);
            shift[numBitChiudi] = true;
            shift.OutBits();
            while (endStopClose.Read()) ;
            shift[numBitChiudi] = false;
            shift.OutBits();
            ventola.Set(false);
        }

        public void Ventilazione(int percentuale)
        {
            if (percentuale == 0)
            {
                ChiudiCompletamente();
            }
            else
                if ((percentuale > 0) && (percentuale < 50))
            {
                int numStep = (Int32)(((double)totStep / 50.0) * (double)percentuale);
                ChiudiCompletamente();
                step = 0;
                shift[numBitApri] = true;
                shift.OutBits();
                while (step <= numStep) ;
                shift[numBitApri] = false;
                shift.OutBits();

            }
            else
                    if ((percentuale >= 50) && (percentuale < 100))
            {
                ApriCompletamente();
                ventola.Set(50, (byte)((percentuale - 50) * 2));
            }
            else
            {
                ApriCompletamente();
                ventola.Set(true);
            }
            this.percentuale = percentuale;
        }

        public void Incrementa()
        {
            if (percentuale < 50)
            {
                if (endStopOpen.Read())
                {
                    step = 0;
                    shift[numBitApri] = true;
                    shift.OutBits();
                    while ((step <= 0) && endStopOpen.Read()) ;
                    shift[numBitApri] = false;
                    shift.OutBits();
                }
                percentuale = percentuale + ((50.0) / (double)totStep);
            }
            else
                if ((percentuale >= 50) && (percentuale < 100))
            {
                if (endStopOpen.Read()) ApriCompletamente();
                percentuale = percentuale + 1;
                ventola.Set(50, (byte)((percentuale - 50) * 2));
            }

            if ((percentuale >= 100))
            {
                if (endStopOpen.Read()) ApriCompletamente();
                ventola.Set(true);
            }
        }

        /// <summary>
        /// Da collaudare sicuramente ha dei bug
        /// </summary>
        public void Decrementa()
        {
            if (percentuale <= 0) ChiudiCompletamente();
            else
                if ((percentuale > 0) && (percentuale < 50))
            {
                step = 0;
                shift[numBitChiudi] = true;
                shift.OutBits();
                while ((step <= 0) && endStopClose.Read()) ;
                shift[numBitChiudi] = false;
                shift.OutBits();
                percentuale = percentuale - ((50.0) / (double)totStep);
            }
            else
                    if ((percentuale >= 50) && (percentuale < 100))
            {
                ApriCompletamente();
                percentuale = percentuale - 1;
                ventola.Set(50, (byte)((percentuale - 50) * 2));
            }
            else
                        if ((percentuale >= 100))
            {
                ApriCompletamente();
                ventola.Set(true);
            }
        }

        public int Percentuale
        {
            get
            {
                return ((int)this.percentuale);
            }
        }

        */
    }
}

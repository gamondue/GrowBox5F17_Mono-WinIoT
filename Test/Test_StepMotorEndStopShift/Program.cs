using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowBoxShared;

namespace Test_StepMotorEndStopShift
{
    class Program
    {
        static void Main(string[] args)
        {
            OutShiftRegister reg = new OutShiftRegister(16, 14, 11, 12);
            StepMotorEndStopShift motor = new StepMotorEndStopShift(reg, 3, 4, 5, 6, 21, 20);

            while (motor.StepOrario(1)) ;
            while (motor.StepAntiOrario(1)) ;
            motor.Folle();
                   
    
            
        }
    }
}

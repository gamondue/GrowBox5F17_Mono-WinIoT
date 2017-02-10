using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrowBoxShared;

/// <summary>
/// Programma di prova per lo shift register di uscita per le attuazioni GrowBox
/// </summary>
namespace Test_OutShifRegister
{
    class Program
    {
        //TODO assegnare il lavoro ad un gruppo !!!!!
        static void Main(string[] args)
        {
            OutShiftRegister sr = new GrowBoxShared.OutShiftRegister(16, 25, 24, 23);
        }
    }
}

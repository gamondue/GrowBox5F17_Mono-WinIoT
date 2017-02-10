using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GrowBoxShared;

namespace Test_DigitalConverterMCP3208_Mono
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitalConverterMCP3208 mcp = new DigitalConverterMCP3208();

            bool ex = false;
            while (!ex)
            {
                int[] values = ReadAllADC(mcp);
                foreach (int val in values)
                {
                    Console.WriteLine(val);
                }
                Console.WriteLine();

                Thread.Sleep(1000);

                ex = Console.KeyAvailable;
            }
        }

        static int[] ReadAllADC(DigitalConverterMCP3208 mcp)
        {
            int[] values = new int[8];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = mcp.ReadValue(i);
            }

            return values;
        }
    }
}

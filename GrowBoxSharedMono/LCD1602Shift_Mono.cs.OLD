using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowBoxShared
{
    class LCD1602Shift
    {


        private OutShiftRegister _shift;
        int _LCDRegisterSelect;
        int _LCDEnable;
        int _LCDBit0;
        int _LCDBit1;
        int _LCDBit2;
        int _LCDBit3;
        const byte DISP_ON = 0xC; //Turn visible LCD on
        const byte CLR_DISP = 1; //Clear display
        const byte CUR_HOME = 2; //Move cursor home and clear screen memory
        const byte SET_CURSOR = 0x80; //SET_CURSOR + X : Sets cursor position to X

        public void Dispose() { }

        public LCD1602Shift(
            OutShiftRegister shift,
            int bit0,
            int bit1,
            int bit2,
            int bit3,
            int enable,
            int registerSelect)

        {
            _shift = shift;

            _LCDRegisterSelect = registerSelect;
            _LCDEnable = enable;
            _LCDBit0 = bit0;
            _LCDBit1 = bit1;
            _LCDBit2 = bit2;
            _LCDBit3 = bit3;

            shift[_LCDRegisterSelect] = false;
            shift.OutBits();

            // 4 bit data communication
            //Task.Delay(50);
            shift[_LCDBit3] = false;
            shift[_LCDBit2] = false;
            shift[_LCDBit1] = true;
            shift[_LCDBit0] = true;
            shift.OutBits();

            shift[_LCDEnable] = true; //Toggle the Enable Pin
            shift.OutBits();
            shift[_LCDEnable] = false;
            shift.OutBits();

            //Task.Delay(50);

            shift[_LCDBit3] = false;
            shift[_LCDBit2] = false;
            shift[_LCDBit1] = true;
            shift[_LCDBit0] = true;
            shift.OutBits();

            shift[_LCDEnable] = true; //Toggle the Enable Pin
            shift.OutBits();
            shift[_LCDEnable] = false;
            shift.OutBits();

            //Task.Delay(50);

            shift[_LCDBit3] = false;
            shift[_LCDBit2] = false;
            shift[_LCDBit1] = true;
            shift[_LCDBit0] = true;
            shift.OutBits();

            shift[_LCDEnable] = true; //Toggle the Enable Pin
            shift.OutBits();
            shift[_LCDEnable] = false;
            shift.OutBits();

            //Task.Delay(50);

            shift[_LCDBit3] = false;
            shift[_LCDBit2] = false;
            shift[_LCDBit1] = true;
            shift[_LCDBit0] = false;
            shift.OutBits();

            shift[_LCDEnable] = true; //Toggle the Enable Pin
            shift.OutBits();
            shift[_LCDEnable] = false;
            shift.OutBits();

            SendCmd(DISP_ON);
            SendCmd(CLR_DISP);
        }

        //Sends an ASCII character to the LCD
        void Putc(byte c)
        {


            _shift[_LCDBit3] = (c & 0x80) != 0;
            _shift[_LCDBit2] = (c & 0x40) != 0;
            _shift[_LCDBit1] = (c & 0x20) != 0;
            _shift[_LCDBit0] = (c & 0x10) != 0;
            _shift.OutBits();

            _shift[_LCDEnable] = true; //Toggle the Enable Pin
            _shift.OutBits();
            _shift[_LCDEnable] = false;
            _shift.OutBits();

            _shift[_LCDBit3] = (c & 0x08) != 0;
            _shift[_LCDBit2] = (c & 0x04) != 0;
            _shift[_LCDBit1] = (c & 0x02) != 0;
            _shift[_LCDBit0] = (c & 0x01) != 0;
            _shift.OutBits();

            _shift[_LCDEnable] = true; //Toggle the Enable Pin
            _shift.OutBits();
            _shift[_LCDEnable] = false;
            _shift.OutBits();

            //Task.Delay(1);
        }

        //Sends an LCD command
        void SendCmd(byte c)
        {
            _shift[_LCDRegisterSelect] = false; //set LCD to data mode
            _shift.OutBits();

            _shift[_LCDBit3] = (c & 0x80) != 0;
            _shift[_LCDBit2] = (c & 0x40) != 0;
            _shift[_LCDBit1] = (c & 0x20) != 0;
            _shift[_LCDBit0] = (c & 0x10) != 0;
            _shift.OutBits();

            _shift[_LCDEnable] = true; //Toggle the Enable Pin
            _shift.OutBits();
            _shift[_LCDEnable] = false;
            _shift.OutBits();

            _shift[_LCDBit3] = (c & 0x08) != 0;
            _shift[_LCDBit2] = (c & 0x04) != 0;
            _shift[_LCDBit1] = (c & 0x02) != 0;
            _shift[_LCDBit0] = (c & 0x01) != 0;
            _shift.OutBits();

            _shift[_LCDEnable] = true; //Toggle the Enable Pin
            _shift.OutBits();
            _shift[_LCDEnable] = false;
            _shift.OutBits();

            //Task.Delay(1);

            _shift[_LCDRegisterSelect] = true; //set LCD to data mode
            _shift.OutBits();
        }

        public void Print(string str)
        {
            for (int i = 0; i < str.Length; i++)
                Putc((byte)str[i]);
        }

        public void Clear()
        {
            SendCmd(CLR_DISP);
        }

        public void CursorHome()
        {
            SendCmd(CUR_HOME);
        }

        public void SetCursor(byte row, byte col)
        {
            SendCmd((byte)(SET_CURSOR | row << 6 | col));
        }

    }
}

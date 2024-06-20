using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ptnr
{
    static class SysDefs
    {
        public const int MAX_CHAMBER_CNT = 8;
        public const short MAX = 0xfff;
        public const short MIN = 0xfff*-1;
        public const short NOT_DEFVAL = 0xfff * -1;

        public const char STX = (char)0x02;
        public const char CR = (char)0x0d;
        public const char LF = (char)0x0a;
        public const int TEMP_TEST_CNT = 9;
        
        public const int MAX_REC_CHCNT = 9;

        public const int ADDR_CHAMBER1   = 1;
        public const int ADDR_RECORDER1  = 3;
        
        public const int ADDR_CHAMBER2  = 2;
        public const int ADDR_RECORDER2 = 6;

        public const int ADDR_CHAMBER3 = 4;
        public const int ADDR_RECORDER3 = 7;

        public const int ADDR_CHAMBER4 = 5;
        public const int ADDR_RECORDER4 = 8;

        public const int CH1 = 0;
        public const int CH2 = 1;
        public const int CH3 = 2;
        public const int CH4 = 3;
        public const int CH5 = 4;
        public const int CH6 = 5;
        public const int CH7 = 6;
        public const int CH8 = 7;
        public const int CH9 = 8;

        public static string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\";
        public const string CONFIG_FILE = "sys.cfg";
        public const string RESULT_FOLDER_PATH = "Res";

        public static byte[] StringToByteArray(string str, int length)
        {
            return Encoding.UTF8.GetBytes(str.PadRight(length, '\0'));
        }

        public static void makePcLinkSum(List<byte> data)
        {
            var sum = 0;
            foreach (char ch in data)
            {
                sum += ch;
            }
            sum = sum & 0x00ff;

            var c1 = ((sum >> 4) & 0x0f);
            var c2 = (sum & 0x0f);

            if (c1 > 9) c1 = (c1 - 10 + 'A');
            else c1 += '0';

            if (c2 > 9) c2 = (c2 - 10 + 'A');
            else c2 += '0';

            data.Insert(0, (byte)SysDefs.STX);

            data.Add((byte)c1);
            data.Add((byte)c2);
            data.Add((byte)SysDefs.CR);
            data.Add((byte)SysDefs.LF);
        }

        public static bool checkPcLinkSum(string rcv)
        {
            if (rcv == null || rcv.Length < 4)
            {
                return false;
            }

            List<byte> data = Encoding.ASCII.GetBytes(rcv).ToList();

            List<byte> oldSum = data.GetRange(data.Count - 4, 2);
            data = data.GetRange(1, data.Count - 5);

            var sum = 0;
            foreach (char ch in data)
            {
                sum += ch;
            }
            sum = sum & 0x00ff;

            var c1 = ((sum >> 4) & 0x0f);
            var c2 = (sum & 0x0f);

            if (c1 > 9) c1 = (c1 - 10 + 'A');
            else c1 += '0';

            if (c2 > 9) c2 = (c2 - 10 + 'A');
            else c2 += '0';

            if (oldSum[0] == c1 && oldSum[1] == c2)
            {
                return true;
            }
            return false;
        }

        public static string DotString(short val, int dp)
        {
            string str = "";

            if (dp == 0) str = string.Format("{0}", (int)val);
            if (dp == 1) str = string.Format("{0:0.0}", val * 0.1);
            if (dp == 2) str = string.Format("{0:0.00}", val * 0.01);
            if (dp == 3) str = string.Format("{0:0.000}", val * 0.001);

            return str;
        }

        public static short DotStringToVal(string str, int dp)
        {
            short val = 0;

            if (dp == 0) val = (short)Convert.ToDouble(str);
            if (dp == 1) val = (short)(Convert.ToDouble(str) * 10);
            if (dp == 2) val = (short)(Convert.ToDouble(str) * 100);
            if (dp == 3) val = (short)(Convert.ToDouble(str) * 1000);

            return val;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Ptnr
{
    public class EqmtConfig
    {
        public int Port { get; set; }
        public string Serial { get; set; }
        public string AmbTemp { get; set; }
        public string AmbHumi { get; set; }
        public string CoolTemp { get; set; }
        public string Approv { get; set; }

        public EqmtConfig()
        {
            Port = 0;

            Serial = "000000-00";
            AmbTemp = "0.0";
            AmbHumi = "0.0";
            CoolTemp = "0.0";
            Approv = "APPROV-NAME";
        }
    }

    public class CommConfig
    {
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int DataBit { get; set; }

        public CommConfig() 
        { 
            BaudRate = 115200;
            Parity = Parity.None;
            StopBits = StopBits.One;
            DataBit = 8;
        }
    }

    public class TempConfig
    {
        public short[] TSp { get; set; }
        public int[] WaitTm { get; set; }
        public int[] TestTm { get; set; }
        public int[] StblTm { get; set; }

        public short[] TempDiff { get; set; }
        public short[] Ramp { get; set; }
        public short[] Uniformity { get; set; }
        public short[] TOver { get; set; }
        
        public bool[] bUseTDiff { get; set; }
        public bool[] bUseRamp { get; set; }
        public bool[] bUseUnif { get; set; }
        public bool[] bUseTOver { get; set; }
        public bool[] bUseStableTm { get; set; }

        public TempConfig()
        {
            TSp = new short[SysDefs.TEMP_TEST_CNT];
            WaitTm = new int[SysDefs.TEMP_TEST_CNT];
            TestTm = new int[SysDefs.TEMP_TEST_CNT];
            StblTm = new int[SysDefs.TEMP_TEST_CNT];

            TempDiff = new short[SysDefs.TEMP_TEST_CNT];
            Ramp = new short[SysDefs.TEMP_TEST_CNT];
            Uniformity = new short[SysDefs.TEMP_TEST_CNT];
            TOver = new short[SysDefs.TEMP_TEST_CNT];

            bUseTDiff = new bool[SysDefs.TEMP_TEST_CNT];
            bUseRamp = new bool[SysDefs.TEMP_TEST_CNT];
            bUseUnif = new bool[SysDefs.TEMP_TEST_CNT];

            bUseTOver = new bool[SysDefs.TEMP_TEST_CNT];
            bUseStableTm = new bool[SysDefs.TEMP_TEST_CNT];
        }
    }


    public class Config
    {
        public TempConfig TpCfg { get; set; }
        public CommConfig CommCfg { get; set; }
        public EqmtConfig[] EqmtCfg { get; set; }

        public string title;

        public Config()
        {
            EqmtCfg = new EqmtConfig[] {
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(),
            new EqmtConfig(), };

            TpCfg = new TempConfig();
            CommCfg = new CommConfig();
        }
    }
}

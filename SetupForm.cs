using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ptnr
{
    public partial class SetupForm : Form
    {
        Config Cfg { get; set; }

        public SetupForm(Config cfg)
        {
            InitializeComponent();

            this.cmbEqmt1Port.Items.Clear();
            this.cmbEqmt2Port.Items.Clear();
            this.cmbEqmt3Port.Items.Clear();
            this.cmbEqmt4Port.Items.Clear();
            this.cmbEqmt5Port.Items.Clear();
            this.cmbEqmt6Port.Items.Clear();
            this.cmbEqmt7Port.Items.Clear();
            this.cmbEqmt8Port.Items.Clear();

            this.cmbEqmt1Port.Items.Add("COM1");
            this.cmbEqmt1Port.Items.Add("COM2");
            this.cmbEqmt1Port.Items.Add("COM3");
            this.cmbEqmt1Port.Items.Add("COM4");
            this.cmbEqmt1Port.Items.Add("COM5");
            this.cmbEqmt1Port.Items.Add("COM6");
            this.cmbEqmt1Port.Items.Add("COM7");
            this.cmbEqmt1Port.Items.Add("COM8");
            this.cmbEqmt1Port.Items.Add("COM9");
            this.cmbEqmt1Port.Items.Add("COM10");
            this.cmbEqmt1Port.Items.Add("COM11");
            this.cmbEqmt1Port.Items.Add("COM12");
            this.cmbEqmt1Port.Items.Add("COM13");
            this.cmbEqmt1Port.Items.Add("COM14");
            this.cmbEqmt1Port.Items.Add("UNUSE");

            this.cmbEqmt2Port.Items.Add("COM1");
            this.cmbEqmt2Port.Items.Add("COM2");
            this.cmbEqmt2Port.Items.Add("COM3");
            this.cmbEqmt2Port.Items.Add("COM4");
            this.cmbEqmt2Port.Items.Add("COM5");
            this.cmbEqmt2Port.Items.Add("COM6");
            this.cmbEqmt2Port.Items.Add("COM7");
            this.cmbEqmt2Port.Items.Add("COM8");
            this.cmbEqmt2Port.Items.Add("COM9");
            this.cmbEqmt2Port.Items.Add("COM10");
            this.cmbEqmt2Port.Items.Add("COM11");
            this.cmbEqmt2Port.Items.Add("COM12");
            this.cmbEqmt2Port.Items.Add("COM13");
            this.cmbEqmt2Port.Items.Add("COM14");
            this.cmbEqmt2Port.Items.Add("UNUSE");

            this.cmbEqmt3Port.Items.Add("COM1");
            this.cmbEqmt3Port.Items.Add("COM2");
            this.cmbEqmt3Port.Items.Add("COM3");
            this.cmbEqmt3Port.Items.Add("COM4");
            this.cmbEqmt3Port.Items.Add("COM5");
            this.cmbEqmt3Port.Items.Add("COM6");
            this.cmbEqmt3Port.Items.Add("COM7");
            this.cmbEqmt3Port.Items.Add("COM8");
            this.cmbEqmt3Port.Items.Add("COM9");
            this.cmbEqmt3Port.Items.Add("COM10");
            this.cmbEqmt3Port.Items.Add("COM11");
            this.cmbEqmt3Port.Items.Add("COM12");
            this.cmbEqmt3Port.Items.Add("COM13");
            this.cmbEqmt3Port.Items.Add("COM14");
            this.cmbEqmt3Port.Items.Add("UNUSE");

            this.cmbEqmt4Port.Items.Add("COM1");
            this.cmbEqmt4Port.Items.Add("COM2");
            this.cmbEqmt4Port.Items.Add("COM3");
            this.cmbEqmt4Port.Items.Add("COM4");
            this.cmbEqmt4Port.Items.Add("COM5");
            this.cmbEqmt4Port.Items.Add("COM6");
            this.cmbEqmt4Port.Items.Add("COM7");
            this.cmbEqmt4Port.Items.Add("COM8");
            this.cmbEqmt4Port.Items.Add("COM9");
            this.cmbEqmt4Port.Items.Add("COM10");
            this.cmbEqmt4Port.Items.Add("COM11");
            this.cmbEqmt4Port.Items.Add("COM12");
            this.cmbEqmt4Port.Items.Add("COM13");
            this.cmbEqmt4Port.Items.Add("COM14");
            this.cmbEqmt4Port.Items.Add("UNUSE");

            this.cmbEqmt5Port.Items.Add("COM1");
            this.cmbEqmt5Port.Items.Add("COM2");
            this.cmbEqmt5Port.Items.Add("COM3");
            this.cmbEqmt5Port.Items.Add("COM4");
            this.cmbEqmt5Port.Items.Add("COM5");
            this.cmbEqmt5Port.Items.Add("COM6");
            this.cmbEqmt5Port.Items.Add("COM7");
            this.cmbEqmt5Port.Items.Add("COM8");
            this.cmbEqmt5Port.Items.Add("COM9");
            this.cmbEqmt5Port.Items.Add("COM10");
            this.cmbEqmt5Port.Items.Add("COM11");
            this.cmbEqmt5Port.Items.Add("COM12");
            this.cmbEqmt5Port.Items.Add("COM13");
            this.cmbEqmt5Port.Items.Add("COM14");
            this.cmbEqmt5Port.Items.Add("UNUSE");

            this.cmbEqmt6Port.Items.Add("COM1");
            this.cmbEqmt6Port.Items.Add("COM2");
            this.cmbEqmt6Port.Items.Add("COM3");
            this.cmbEqmt6Port.Items.Add("COM4");
            this.cmbEqmt6Port.Items.Add("COM5");
            this.cmbEqmt6Port.Items.Add("COM6");
            this.cmbEqmt6Port.Items.Add("COM7");
            this.cmbEqmt6Port.Items.Add("COM8");
            this.cmbEqmt6Port.Items.Add("COM9");
            this.cmbEqmt6Port.Items.Add("COM10");
            this.cmbEqmt6Port.Items.Add("COM11");
            this.cmbEqmt6Port.Items.Add("COM12");
            this.cmbEqmt6Port.Items.Add("COM13");
            this.cmbEqmt6Port.Items.Add("COM14");
            this.cmbEqmt6Port.Items.Add("UNUSE");

            this.cmbEqmt7Port.Items.Add("COM1");
            this.cmbEqmt7Port.Items.Add("COM2");
            this.cmbEqmt7Port.Items.Add("COM3");
            this.cmbEqmt7Port.Items.Add("COM4");
            this.cmbEqmt7Port.Items.Add("COM5");
            this.cmbEqmt7Port.Items.Add("COM6");
            this.cmbEqmt7Port.Items.Add("COM7");
            this.cmbEqmt7Port.Items.Add("COM8");
            this.cmbEqmt7Port.Items.Add("COM9");
            this.cmbEqmt7Port.Items.Add("COM10");
            this.cmbEqmt7Port.Items.Add("COM11");
            this.cmbEqmt7Port.Items.Add("COM12");
            this.cmbEqmt7Port.Items.Add("COM13");
            this.cmbEqmt7Port.Items.Add("COM14");
            this.cmbEqmt7Port.Items.Add("UNUSE");

            this.cmbEqmt8Port.Items.Add("COM1");
            this.cmbEqmt8Port.Items.Add("COM2");
            this.cmbEqmt8Port.Items.Add("COM3");
            this.cmbEqmt8Port.Items.Add("COM4");
            this.cmbEqmt8Port.Items.Add("COM5");
            this.cmbEqmt8Port.Items.Add("COM6");
            this.cmbEqmt8Port.Items.Add("COM7");
            this.cmbEqmt8Port.Items.Add("COM8");
            this.cmbEqmt8Port.Items.Add("COM9");
            this.cmbEqmt8Port.Items.Add("COM10");
            this.cmbEqmt8Port.Items.Add("COM11");
            this.cmbEqmt8Port.Items.Add("COM12");
            this.cmbEqmt8Port.Items.Add("COM13");
            this.cmbEqmt8Port.Items.Add("COM14");
            this.cmbEqmt8Port.Items.Add("UNUSE");

            Cfg = cfg;
            UpdateForm();
        }

        private void UpdateForm()
        {
            // Comm Set

            this.cmbEqmt1Port.SelectedIndex = Cfg.EqmtCfg[0].Port - 1;
            this.cmbEqmt2Port.SelectedIndex = Cfg.EqmtCfg[1].Port - 1;
            this.cmbEqmt3Port.SelectedIndex = Cfg.EqmtCfg[2].Port - 1;
            this.cmbEqmt4Port.SelectedIndex = Cfg.EqmtCfg[3].Port - 1;
            this.cmbEqmt5Port.SelectedIndex = Cfg.EqmtCfg[4].Port - 1;
            this.cmbEqmt6Port.SelectedIndex = Cfg.EqmtCfg[5].Port - 1;
            this.cmbEqmt7Port.SelectedIndex = Cfg.EqmtCfg[6].Port - 1;
            this.cmbEqmt8Port.SelectedIndex = Cfg.EqmtCfg[7].Port - 1;

            if (Cfg.CommCfg.BaudRate == 9600) rdoBaud9600.Select();
            if (Cfg.CommCfg.BaudRate == 19200) rdoBaud19200.Select();
            if (Cfg.CommCfg.BaudRate == 38400) rdoBaud38400.Select();
            if (Cfg.CommCfg.BaudRate == 57600) rdoBaud57600.Select();
            if (Cfg.CommCfg.BaudRate == 115200) rdoBaud115200.Select();

            if (Cfg.CommCfg.Parity == System.IO.Ports.Parity.None) rdoParityNone.Select();
            if (Cfg.CommCfg.Parity == System.IO.Ports.Parity.Odd) rdoParityOdd.Select();
            if (Cfg.CommCfg.Parity == System.IO.Ports.Parity.Even) rdoParityEven.Select();

            if (Cfg.CommCfg.StopBits == System.IO.Ports.StopBits.One) rdoStBit1.Select();
            if (Cfg.CommCfg.StopBits == System.IO.Ports.StopBits.Two) rdoStBit2.Select();

            if (Cfg.CommCfg.DataBit == 7) rdoDLen7.Select();
            if (Cfg.CommCfg.DataBit == 8) rdoDLen8.Select();

            int idx = 0;
            txtChamberTSp1.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp1.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm1.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm1.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff1.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff1.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp1.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif1.Text  = (Cfg.TmCfg.bUseUnif[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);

            txtChamberTOver1.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver1.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);

            chkT1DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp2.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp2.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm2.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm2.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff2.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff2.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp2.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif2.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver2.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver2.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT2DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp3.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp3.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm3.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm3.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff3.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff3.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp3.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif3.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver3.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver3.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT3DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp4.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp4.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm4.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm4.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff4.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff4.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp4.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif4.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver4.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver4.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT4DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp5.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp5.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm5.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm5.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff5.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff5.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp5.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif5.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver5.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver5.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT5DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp6.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp6.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm6.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm6.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff6.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff6.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp6.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif6.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver6.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver6.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT6DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp7.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp7.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm7.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm7.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff7.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff7.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp7.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif7.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver7.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver7.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT7DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp8.Text   = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp8.Text   = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm8.Text   = (Cfg.TmCfg.WaitTm[idx]     == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm8.Text   = (Cfg.TmCfg.TestTm[idx]     == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff8.Text = (Cfg.TmCfg.bUseTDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff8.Text = (Cfg.TmCfg.bUseHDiff[idx]  == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp8.Text  = (Cfg.TmCfg.bUseRamp[idx]   == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif8.Text  = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver8.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver8.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT8DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            idx++;
            txtChamberTSp9.Text = SysDefs.DotString(Cfg.TmCfg.TSp[idx], 1);
            txtChamberHSp9.Text = SysDefs.DotString(Cfg.TmCfg.HSp[idx], 1);
            txtChamberWTm9.Text = (Cfg.TmCfg.WaitTm[idx] == 0) ? "-" : Cfg.TmCfg.WaitTm[idx].ToString();
            txtChamberTTm9.Text = (Cfg.TmCfg.TestTm[idx] == 0) ? "-" : Cfg.TmCfg.TestTm[idx].ToString();
            txtChamberTDiff9.Text = (Cfg.TmCfg.bUseTDiff[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TempDiff[idx], 1);
            txtChamberHDiff9.Text = (Cfg.TmCfg.bUseHDiff[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HumiDiff[idx], 1);
            txtChamberRamp9.Text = (Cfg.TmCfg.bUseRamp[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Ramp[idx], 1);
            txtChamberUnif9.Text = (Cfg.TmCfg.bUseUnif[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.Uniformity[idx], 1);
            txtChamberTOver9.Text = (Cfg.TmCfg.bUseTOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.TOver[idx], 1);
            txtChamberHOver9.Text = (Cfg.TmCfg.bUseHOver[idx] == false) ? "-" : SysDefs.DotString(Cfg.TmCfg.HOver[idx], 1);
            chkT9DoReport.Checked = (Cfg.TmCfg.bReport[idx] == false) ? false : true;

            // Temp
            txtTempTSp1.Text = SysDefs.DotString(Cfg.TpCfg.TSp[0], 1);
            txtTempTSp2.Text = SysDefs.DotString(Cfg.TpCfg.TSp[1], 1);
            txtTempTSp3.Text = SysDefs.DotString(Cfg.TpCfg.TSp[2], 1);
            txtTempTSp4.Text = SysDefs.DotString(Cfg.TpCfg.TSp[3], 1);
            txtTempTSp5.Text = SysDefs.DotString(Cfg.TpCfg.TSp[4], 1);

            txtTempWTm1.Text = (Cfg.TpCfg.WaitTm[0] == 0) ? "-" : Cfg.TpCfg.WaitTm[0].ToString();
            txtTempWTm2.Text = (Cfg.TpCfg.WaitTm[1] == 0) ? "-" : Cfg.TpCfg.WaitTm[1].ToString();
            txtTempWTm3.Text = (Cfg.TpCfg.WaitTm[2] == 0) ? "-" : Cfg.TpCfg.WaitTm[2].ToString();
            txtTempWTm4.Text = (Cfg.TpCfg.WaitTm[3] == 0) ? "-" : Cfg.TpCfg.WaitTm[3].ToString();
            txtTempWTm5.Text = (Cfg.TpCfg.WaitTm[4] == 0) ? "-" : Cfg.TpCfg.WaitTm[4].ToString();

            txtTempTTm1.Text = (Cfg.TpCfg.TestTm[0] == 0) ? "-" : Cfg.TpCfg.TestTm[0].ToString();
            txtTempTTm2.Text = (Cfg.TpCfg.TestTm[1] == 0) ? "-" : Cfg.TpCfg.TestTm[1].ToString();
            txtTempTTm3.Text = (Cfg.TpCfg.TestTm[2] == 0) ? "-" : Cfg.TpCfg.TestTm[2].ToString();
            txtTempTTm4.Text = (Cfg.TpCfg.TestTm[3] == 0) ? "-" : Cfg.TpCfg.TestTm[3].ToString();
            txtTempTTm5.Text = (Cfg.TpCfg.TestTm[4] == 0) ? "-" : Cfg.TpCfg.TestTm[4].ToString();

            txtTempTDiff1.Text = (Cfg.TpCfg.bUseTDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[0], 1);
            txtTempTDiff2.Text = (Cfg.TpCfg.bUseTDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[1], 1);
            txtTempTDiff3.Text = (Cfg.TpCfg.bUseTDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[2], 1);
            txtTempTDiff4.Text = (Cfg.TpCfg.bUseTDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[3], 1);
            txtTempTDiff5.Text = (Cfg.TpCfg.bUseTDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[4], 1);

            txtTempRamp1.Text = (Cfg.TpCfg.bUseRamp[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[0], 1);
            txtTempRamp2.Text = (Cfg.TpCfg.bUseRamp[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[1], 1);
            txtTempRamp3.Text = (Cfg.TpCfg.bUseRamp[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[2], 1);
            txtTempRamp4.Text = (Cfg.TpCfg.bUseRamp[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[3], 1);
            txtTempRamp5.Text = (Cfg.TpCfg.bUseRamp[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[4], 1);

            txtTempUnif1.Text = (Cfg.TpCfg.bUseUnif[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[0], 1);
            txtTempUnif2.Text = (Cfg.TpCfg.bUseUnif[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[1], 1);
            txtTempUnif3.Text = (Cfg.TpCfg.bUseUnif[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[2], 1);
            txtTempUnif4.Text = (Cfg.TpCfg.bUseUnif[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[3], 1);
            txtTempUnif5.Text = (Cfg.TpCfg.bUseUnif[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[4], 1);

            txtTempTOver1.Text = (Cfg.TpCfg.bUseTOver[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[0], 1);
            txtTempTOver2.Text = (Cfg.TpCfg.bUseTOver[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[1], 1);
            txtTempTOver3.Text = (Cfg.TpCfg.bUseTOver[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[2], 1);
            txtTempTOver4.Text = (Cfg.TpCfg.bUseTOver[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[3], 1);
            txtTempTOver5.Text = (Cfg.TpCfg.bUseTOver[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[4], 1);

            // Temi Chiller.
            txtChillerTSp1.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[0], 1);
            txtChillerTSp2.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[1], 1);
            txtChillerTSp3.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[2], 1);
            txtChillerTSp4.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[3], 1);
            txtChillerTSp5.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[4], 1);
            txtChillerTSp6.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[5], 1);
            txtChillerTSp7.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[6], 1);
            txtChillerTSp8.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[7], 1);
            txtChillerTSp9.Text = SysDefs.DotString(Cfg.TmChillerCfg.TSp[8], 1);

            txtChillerSSp1.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[0], 2);
            txtChillerSSp2.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[1], 2);
            txtChillerSSp3.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[2], 2);
            txtChillerSSp4.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[3], 2);
            txtChillerSSp5.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[4], 2);
            txtChillerSSp6.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[5], 2);
            txtChillerSSp7.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[6], 2);
            txtChillerSSp8.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[7], 2);
            txtChillerSSp9.Text = SysDefs.DotString(Cfg.TmChillerCfg.SSp[8], 2);

            txtChillerWTm1.Text = (Cfg.TmChillerCfg.WaitTm[0] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[0].ToString();
            txtChillerWTm2.Text = (Cfg.TmChillerCfg.WaitTm[1] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[1].ToString();
            txtChillerWTm3.Text = (Cfg.TmChillerCfg.WaitTm[2] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[2].ToString();
            txtChillerWTm4.Text = (Cfg.TmChillerCfg.WaitTm[3] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[3].ToString();
            txtChillerWTm5.Text = (Cfg.TmChillerCfg.WaitTm[4] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[4].ToString();
            txtChillerWTm6.Text = (Cfg.TmChillerCfg.WaitTm[5] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[5].ToString();
            txtChillerWTm7.Text = (Cfg.TmChillerCfg.WaitTm[6] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[6].ToString();
            txtChillerWTm8.Text = (Cfg.TmChillerCfg.WaitTm[7] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[7].ToString();
            txtChillerWTm9.Text = (Cfg.TmChillerCfg.WaitTm[8] == 0) ? "-" : Cfg.TmChillerCfg.WaitTm[8].ToString();

            txtChillerTTm1.Text = (Cfg.TmChillerCfg.TestTm[0] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[0].ToString();
            txtChillerTTm2.Text = (Cfg.TmChillerCfg.TestTm[1] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[1].ToString();
            txtChillerTTm3.Text = (Cfg.TmChillerCfg.TestTm[2] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[2].ToString();
            txtChillerTTm4.Text = (Cfg.TmChillerCfg.TestTm[3] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[3].ToString();
            txtChillerTTm5.Text = (Cfg.TmChillerCfg.TestTm[4] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[4].ToString();
            txtChillerTTm6.Text = (Cfg.TmChillerCfg.TestTm[5] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[5].ToString();
            txtChillerTTm7.Text = (Cfg.TmChillerCfg.TestTm[6] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[6].ToString();
            txtChillerTTm8.Text = (Cfg.TmChillerCfg.TestTm[7] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[7].ToString();
            txtChillerTTm9.Text = (Cfg.TmChillerCfg.TestTm[8] == 0) ? "-" : Cfg.TmChillerCfg.TestTm[8].ToString();

            txtChillerTDiff1.Text = (Cfg.TmChillerCfg.bUseTDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[0], 1);
            txtChillerTDiff2.Text = (Cfg.TmChillerCfg.bUseTDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[1], 1);
            txtChillerTDiff3.Text = (Cfg.TmChillerCfg.bUseTDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[2], 1);
            txtChillerTDiff4.Text = (Cfg.TmChillerCfg.bUseTDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[3], 1);
            txtChillerTDiff5.Text = (Cfg.TmChillerCfg.bUseTDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[4], 1);
            txtChillerTDiff6.Text = (Cfg.TmChillerCfg.bUseTDiff[5] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[5], 1);
            txtChillerTDiff7.Text = (Cfg.TmChillerCfg.bUseTDiff[6] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[6], 1);
            txtChillerTDiff8.Text = (Cfg.TmChillerCfg.bUseTDiff[7] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[7], 1);
            txtChillerTDiff9.Text = (Cfg.TmChillerCfg.bUseTDiff[8] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.TDiff[8], 1);
                                        
            txtChillerSDiff1.Text = (Cfg.TmChillerCfg.bUseSDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[0], 2);
            txtChillerSDiff2.Text = (Cfg.TmChillerCfg.bUseSDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[1], 2);
            txtChillerSDiff3.Text = (Cfg.TmChillerCfg.bUseSDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[2], 2);
            txtChillerSDiff4.Text = (Cfg.TmChillerCfg.bUseSDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[3], 2);
            txtChillerSDiff5.Text = (Cfg.TmChillerCfg.bUseSDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[4], 2);
            txtChillerSDiff6.Text = (Cfg.TmChillerCfg.bUseSDiff[5] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[5], 2);
            txtChillerSDiff7.Text = (Cfg.TmChillerCfg.bUseSDiff[6] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[6], 2);
            txtChillerSDiff8.Text = (Cfg.TmChillerCfg.bUseSDiff[7] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[7], 2);
            txtChillerSDiff9.Text = (Cfg.TmChillerCfg.bUseSDiff[8] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.SDiff[8], 2);
                                        
            txtChillerRamp1.Text = (Cfg.TmChillerCfg.bUseRamp[0] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[0], 1);
            txtChillerRamp2.Text = (Cfg.TmChillerCfg.bUseRamp[1] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[1], 1);
            txtChillerRamp3.Text = (Cfg.TmChillerCfg.bUseRamp[2] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[2], 1);
            txtChillerRamp4.Text = (Cfg.TmChillerCfg.bUseRamp[3] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[3], 1);
            txtChillerRamp5.Text = (Cfg.TmChillerCfg.bUseRamp[4] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[4], 1);
            txtChillerRamp6.Text = (Cfg.TmChillerCfg.bUseRamp[5] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[5], 1);
            txtChillerRamp7.Text = (Cfg.TmChillerCfg.bUseRamp[6] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[6], 1);
            txtChillerRamp8.Text = (Cfg.TmChillerCfg.bUseRamp[7] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[7], 1);
            txtChillerRamp9.Text = (Cfg.TmChillerCfg.bUseRamp[8] == false) ? "-" : SysDefs.DotString(Cfg.TmChillerCfg.Ramp[8], 1);

            // Temp Chiller.
            txtTpChillerTSp1.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[0], 1);
            txtTpChillerTSp2.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[1], 1);
            txtTpChillerTSp3.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[2], 1);
            txtTpChillerTSp4.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[3], 1);
            txtTpChillerTSp5.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[4], 1);
            txtTpChillerTSp6.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[5], 1);
            txtTpChillerTSp7.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[6], 1);
            txtTpChillerTSp8.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[7], 1);
            txtTpChillerTSp9.Text = SysDefs.DotString(Cfg.TpChillerCfg.TSp[8], 1);

            txtTpChillerSSp1.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[0], 2);
            txtTpChillerSSp2.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[1], 2);
            txtTpChillerSSp3.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[2], 2);
            txtTpChillerSSp4.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[3], 2);
            txtTpChillerSSp5.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[4], 2);
            txtTpChillerSSp6.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[5], 2);
            txtTpChillerSSp7.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[6], 2);
            txtTpChillerSSp8.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[7], 2);
            txtTpChillerSSp9.Text = SysDefs.DotString(Cfg.TpChillerCfg.SSp[8], 2);

            txtTpChillerWTm1.Text = (Cfg.TpChillerCfg.WaitTm[0] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[0].ToString();
            txtTpChillerWTm2.Text = (Cfg.TpChillerCfg.WaitTm[1] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[1].ToString();
            txtTpChillerWTm3.Text = (Cfg.TpChillerCfg.WaitTm[2] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[2].ToString();
            txtTpChillerWTm4.Text = (Cfg.TpChillerCfg.WaitTm[3] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[3].ToString();
            txtTpChillerWTm5.Text = (Cfg.TpChillerCfg.WaitTm[4] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[4].ToString();
            txtTpChillerWTm6.Text = (Cfg.TpChillerCfg.WaitTm[5] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[5].ToString();
            txtTpChillerWTm7.Text = (Cfg.TpChillerCfg.WaitTm[6] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[6].ToString();
            txtTpChillerWTm8.Text = (Cfg.TpChillerCfg.WaitTm[7] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[7].ToString();
            txtTpChillerWTm9.Text = (Cfg.TpChillerCfg.WaitTm[8] == 0) ? "-" : Cfg.TpChillerCfg.WaitTm[8].ToString();
                                         
            txtTpChillerTTm1.Text = (Cfg.TpChillerCfg.TestTm[0] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[0].ToString();
            txtTpChillerTTm2.Text = (Cfg.TpChillerCfg.TestTm[1] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[1].ToString();
            txtTpChillerTTm3.Text = (Cfg.TpChillerCfg.TestTm[2] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[2].ToString();
            txtTpChillerTTm4.Text = (Cfg.TpChillerCfg.TestTm[3] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[3].ToString();
            txtTpChillerTTm5.Text = (Cfg.TpChillerCfg.TestTm[4] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[4].ToString();
            txtTpChillerTTm6.Text = (Cfg.TpChillerCfg.TestTm[5] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[5].ToString();
            txtTpChillerTTm7.Text = (Cfg.TpChillerCfg.TestTm[6] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[6].ToString();
            txtTpChillerTTm8.Text = (Cfg.TpChillerCfg.TestTm[7] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[7].ToString();
            txtTpChillerTTm9.Text = (Cfg.TpChillerCfg.TestTm[8] == 0) ? "-" : Cfg.TpChillerCfg.TestTm[8].ToString();

            txtTpChillerTDiff1.Text = (Cfg.TpChillerCfg.bUseTDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[0], 1);
            txtTpChillerTDiff2.Text = (Cfg.TpChillerCfg.bUseTDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[1], 1);
            txtTpChillerTDiff3.Text = (Cfg.TpChillerCfg.bUseTDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[2], 1);
            txtTpChillerTDiff4.Text = (Cfg.TpChillerCfg.bUseTDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[3], 1);
            txtTpChillerTDiff5.Text = (Cfg.TpChillerCfg.bUseTDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[4], 1);
            txtTpChillerTDiff6.Text = (Cfg.TpChillerCfg.bUseTDiff[5] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[5], 1);
            txtTpChillerTDiff7.Text = (Cfg.TpChillerCfg.bUseTDiff[6] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[6], 1);
            txtTpChillerTDiff8.Text = (Cfg.TpChillerCfg.bUseTDiff[7] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[7], 1);
            txtTpChillerTDiff9.Text = (Cfg.TpChillerCfg.bUseTDiff[8] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.TDiff[8], 1);
                                           
            txtTpChillerSDiff1.Text = (Cfg.TpChillerCfg.bUseSDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[0], 2);
            txtTpChillerSDiff2.Text = (Cfg.TpChillerCfg.bUseSDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[1], 2);
            txtTpChillerSDiff3.Text = (Cfg.TpChillerCfg.bUseSDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[2], 2);
            txtTpChillerSDiff4.Text = (Cfg.TpChillerCfg.bUseSDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[3], 2);
            txtTpChillerSDiff5.Text = (Cfg.TpChillerCfg.bUseSDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[4], 2);
            txtTpChillerSDiff6.Text = (Cfg.TpChillerCfg.bUseSDiff[5] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[5], 2);
            txtTpChillerSDiff7.Text = (Cfg.TpChillerCfg.bUseSDiff[6] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[6], 2);
            txtTpChillerSDiff8.Text = (Cfg.TpChillerCfg.bUseSDiff[7] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[7], 2);
            txtTpChillerSDiff9.Text = (Cfg.TpChillerCfg.bUseSDiff[8] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.SDiff[8], 2);

            txtTpChillerRamp1.Text = (Cfg.TpChillerCfg.bUseRamp[0] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[0], 1);
            txtTpChillerRamp2.Text = (Cfg.TpChillerCfg.bUseRamp[1] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[1], 1);
            txtTpChillerRamp3.Text = (Cfg.TpChillerCfg.bUseRamp[2] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[2], 1);
            txtTpChillerRamp4.Text = (Cfg.TpChillerCfg.bUseRamp[3] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[3], 1);
            txtTpChillerRamp5.Text = (Cfg.TpChillerCfg.bUseRamp[4] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[4], 1);
            txtTpChillerRamp6.Text = (Cfg.TpChillerCfg.bUseRamp[5] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[5], 1);
            txtTpChillerRamp7.Text = (Cfg.TpChillerCfg.bUseRamp[6] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[6], 1);
            txtTpChillerRamp8.Text = (Cfg.TpChillerCfg.bUseRamp[7] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[7], 1);
            txtTpChillerRamp9.Text = (Cfg.TpChillerCfg.bUseRamp[8] == false) ? "-" : SysDefs.DotString(Cfg.TpChillerCfg.Ramp[8], 1);
        }

        private void OnOk(object sender, EventArgs e)
        {
            Cfg.EqmtCfg[0].Port = this.cmbEqmt1Port.SelectedIndex + 1;
            Cfg.EqmtCfg[1].Port = this.cmbEqmt2Port.SelectedIndex + 1;
            Cfg.EqmtCfg[2].Port = this.cmbEqmt3Port.SelectedIndex + 1;
            Cfg.EqmtCfg[3].Port = this.cmbEqmt4Port.SelectedIndex + 1;
            Cfg.EqmtCfg[4].Port = this.cmbEqmt5Port.SelectedIndex + 1;
            Cfg.EqmtCfg[5].Port = this.cmbEqmt6Port.SelectedIndex + 1;
            Cfg.EqmtCfg[6].Port = this.cmbEqmt7Port.SelectedIndex + 1;
            Cfg.EqmtCfg[7].Port = this.cmbEqmt8Port.SelectedIndex + 1;

            if (rdoBaud9600.Checked) Cfg.CommCfg.BaudRate = 9600;
            if (rdoBaud19200.Checked) Cfg.CommCfg.BaudRate = 19200;
            if (rdoBaud38400.Checked) Cfg.CommCfg.BaudRate = 38400;
            if (rdoBaud57600.Checked) Cfg.CommCfg.BaudRate = 57600;
            if (rdoBaud115200.Checked) Cfg.CommCfg.BaudRate = 115200;

            if (rdoParityNone.Checked) Cfg.CommCfg.Parity = System.IO.Ports.Parity.None;
            if (rdoParityOdd.Checked) Cfg.CommCfg.Parity = System.IO.Ports.Parity.Odd;
            if (rdoParityEven.Checked) Cfg.CommCfg.Parity = System.IO.Ports.Parity.Even;

            if (rdoStBit1.Checked) Cfg.CommCfg.StopBits = System.IO.Ports.StopBits.One;
            if (rdoStBit2.Checked) Cfg.CommCfg.StopBits = System.IO.Ports.StopBits.Two;

            if (rdoDLen7.Checked) Cfg.CommCfg.DataBit = 7;
            if (rdoDLen8.Checked) Cfg.CommCfg.DataBit = 8;

            // temi
            Cfg.TmCfg.TSp[0] = (this.txtChamberTSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp1.Text, 1);
            Cfg.TmCfg.TSp[1] = (this.txtChamberTSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp2.Text, 1);
            Cfg.TmCfg.TSp[2] = (this.txtChamberTSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp3.Text, 1);
            Cfg.TmCfg.TSp[3] = (this.txtChamberTSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp4.Text, 1);
            Cfg.TmCfg.TSp[4] = (this.txtChamberTSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp5.Text, 1);
            Cfg.TmCfg.TSp[5] = (this.txtChamberTSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp6.Text, 1);
            Cfg.TmCfg.TSp[6] = (this.txtChamberTSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp7.Text, 1);
            Cfg.TmCfg.TSp[7] = (this.txtChamberTSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp8.Text, 1);
            Cfg.TmCfg.TSp[8] = (this.txtChamberTSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTSp9.Text, 1);

            Cfg.TmCfg.HSp[0] = (this.txtChamberHSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp1.Text, 1);
            Cfg.TmCfg.HSp[1] = (this.txtChamberHSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp2.Text, 1);
            Cfg.TmCfg.HSp[2] = (this.txtChamberHSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp3.Text, 1);
            Cfg.TmCfg.HSp[3] = (this.txtChamberHSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp4.Text, 1);
            Cfg.TmCfg.HSp[4] = (this.txtChamberHSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp5.Text, 1);
            Cfg.TmCfg.HSp[5] = (this.txtChamberHSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp6.Text, 1);
            Cfg.TmCfg.HSp[6] = (this.txtChamberHSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp7.Text, 1);
            Cfg.TmCfg.HSp[7] = (this.txtChamberHSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp8.Text, 1);
            Cfg.TmCfg.HSp[8] = (this.txtChamberHSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHSp9.Text, 1);

            Cfg.TmCfg.WaitTm[0] = (this.txtChamberWTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm1.Text);
            Cfg.TmCfg.WaitTm[1] = (this.txtChamberWTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm2.Text);
            Cfg.TmCfg.WaitTm[2] = (this.txtChamberWTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm3.Text);
            Cfg.TmCfg.WaitTm[3] = (this.txtChamberWTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm4.Text);
            Cfg.TmCfg.WaitTm[4] = (this.txtChamberWTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm5.Text);
            Cfg.TmCfg.WaitTm[5] = (this.txtChamberWTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm6.Text);
            Cfg.TmCfg.WaitTm[6] = (this.txtChamberWTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm7.Text);
            Cfg.TmCfg.WaitTm[7] = (this.txtChamberWTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm8.Text);
            Cfg.TmCfg.WaitTm[8] = (this.txtChamberWTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberWTm9.Text);

            Cfg.TmCfg.TestTm[0] = (this.txtChamberTTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm1.Text);
            Cfg.TmCfg.TestTm[1] = (this.txtChamberTTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm2.Text);
            Cfg.TmCfg.TestTm[2] = (this.txtChamberTTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm3.Text);
            Cfg.TmCfg.TestTm[3] = (this.txtChamberTTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm4.Text);
            Cfg.TmCfg.TestTm[4] = (this.txtChamberTTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm5.Text);
            Cfg.TmCfg.TestTm[5] = (this.txtChamberTTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm6.Text);
            Cfg.TmCfg.TestTm[6] = (this.txtChamberTTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm7.Text);
            Cfg.TmCfg.TestTm[7] = (this.txtChamberTTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm8.Text);
            Cfg.TmCfg.TestTm[8] = (this.txtChamberTTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberTTm9.Text);

            Cfg.TmCfg.TempDiff[0] = (this.txtChamberTDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff1.Text, 1);
            Cfg.TmCfg.TempDiff[1] = (this.txtChamberTDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff2.Text, 1);
            Cfg.TmCfg.TempDiff[2] = (this.txtChamberTDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff3.Text, 1);
            Cfg.TmCfg.TempDiff[3] = (this.txtChamberTDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff4.Text, 1);
            Cfg.TmCfg.TempDiff[4] = (this.txtChamberTDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff5.Text, 1);
            Cfg.TmCfg.TempDiff[5] = (this.txtChamberTDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff6.Text, 1);
            Cfg.TmCfg.TempDiff[6] = (this.txtChamberTDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff7.Text, 1);
            Cfg.TmCfg.TempDiff[7] = (this.txtChamberTDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff8.Text, 1);
            Cfg.TmCfg.TempDiff[8] = (this.txtChamberTDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTDiff9.Text, 1);

            Cfg.TmCfg.HumiDiff[0] = (this.txtChamberHDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff1.Text, 1);
            Cfg.TmCfg.HumiDiff[1] = (this.txtChamberHDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff2.Text, 1);
            Cfg.TmCfg.HumiDiff[2] = (this.txtChamberHDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff3.Text, 1);
            Cfg.TmCfg.HumiDiff[3] = (this.txtChamberHDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff4.Text, 1);
            Cfg.TmCfg.HumiDiff[4] = (this.txtChamberHDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff5.Text, 1);
            Cfg.TmCfg.HumiDiff[5] = (this.txtChamberHDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff6.Text, 1);
            Cfg.TmCfg.HumiDiff[6] = (this.txtChamberHDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff7.Text, 1);
            Cfg.TmCfg.HumiDiff[7] = (this.txtChamberHDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff8.Text, 1);
            Cfg.TmCfg.HumiDiff[8] = (this.txtChamberHDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHDiff9.Text, 1);

            Cfg.TmCfg.Ramp[0] = (this.txtChamberRamp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp1.Text, 1);
            Cfg.TmCfg.Ramp[1] = (this.txtChamberRamp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp2.Text, 1);
            Cfg.TmCfg.Ramp[2] = (this.txtChamberRamp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp3.Text, 1);
            Cfg.TmCfg.Ramp[3] = (this.txtChamberRamp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp4.Text, 1);
            Cfg.TmCfg.Ramp[4] = (this.txtChamberRamp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp5.Text, 1);
            Cfg.TmCfg.Ramp[5] = (this.txtChamberRamp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp6.Text, 1);
            Cfg.TmCfg.Ramp[6] = (this.txtChamberRamp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp7.Text, 1);
            Cfg.TmCfg.Ramp[7] = (this.txtChamberRamp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp8.Text, 1);
            Cfg.TmCfg.Ramp[8] = (this.txtChamberRamp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberRamp9.Text, 1);

            Cfg.TmCfg.Uniformity[0] = (this.txtChamberUnif1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif1.Text, 1);
            Cfg.TmCfg.Uniformity[1] = (this.txtChamberUnif2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif2.Text, 1);
            Cfg.TmCfg.Uniformity[2] = (this.txtChamberUnif3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif3.Text, 1);
            Cfg.TmCfg.Uniformity[3] = (this.txtChamberUnif4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif4.Text, 1);
            Cfg.TmCfg.Uniformity[4] = (this.txtChamberUnif5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif5.Text, 1);
            Cfg.TmCfg.Uniformity[5] = (this.txtChamberUnif6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif6.Text, 1);
            Cfg.TmCfg.Uniformity[6] = (this.txtChamberUnif7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif7.Text, 1);
            Cfg.TmCfg.Uniformity[7] = (this.txtChamberUnif8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif8.Text, 1);
            Cfg.TmCfg.Uniformity[8] = (this.txtChamberUnif9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberUnif9.Text, 1);

            Cfg.TmCfg.TOver[0] = (this.txtChamberTOver1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver1.Text, 1);
            Cfg.TmCfg.TOver[1] = (this.txtChamberTOver2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver2.Text, 1);
            Cfg.TmCfg.TOver[2] = (this.txtChamberTOver3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver3.Text, 1);
            Cfg.TmCfg.TOver[3] = (this.txtChamberTOver4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver4.Text, 1);
            Cfg.TmCfg.TOver[4] = (this.txtChamberTOver5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver5.Text, 1);
            Cfg.TmCfg.TOver[5] = (this.txtChamberTOver6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver6.Text, 1);
            Cfg.TmCfg.TOver[6] = (this.txtChamberTOver7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver7.Text, 1);
            Cfg.TmCfg.TOver[7] = (this.txtChamberTOver8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver8.Text, 1);
            Cfg.TmCfg.TOver[8] = (this.txtChamberTOver9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberTOver9.Text, 1);

            Cfg.TmCfg.HOver[0] = (this.txtChamberHOver1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver1.Text, 1);
            Cfg.TmCfg.HOver[1] = (this.txtChamberHOver2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver2.Text, 1);
            Cfg.TmCfg.HOver[2] = (this.txtChamberHOver3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver3.Text, 1);
            Cfg.TmCfg.HOver[3] = (this.txtChamberHOver4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver4.Text, 1);
            Cfg.TmCfg.HOver[4] = (this.txtChamberHOver5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver5.Text, 1);
            Cfg.TmCfg.HOver[5] = (this.txtChamberHOver6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver6.Text, 1);
            Cfg.TmCfg.HOver[6] = (this.txtChamberHOver7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver7.Text, 1);
            Cfg.TmCfg.HOver[7] = (this.txtChamberHOver8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver8.Text, 1);
            Cfg.TmCfg.HOver[8] = (this.txtChamberHOver9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChamberHOver9.Text, 1);

            Cfg.TmCfg.bReport[0] = this.chkT1DoReport.Checked;
            Cfg.TmCfg.bReport[1] = this.chkT2DoReport.Checked;
            Cfg.TmCfg.bReport[2] = this.chkT3DoReport.Checked;
            Cfg.TmCfg.bReport[3] = this.chkT4DoReport.Checked;
            Cfg.TmCfg.bReport[4] = this.chkT5DoReport.Checked;
            Cfg.TmCfg.bReport[5] = this.chkT6DoReport.Checked;
            Cfg.TmCfg.bReport[6] = this.chkT7DoReport.Checked;
            Cfg.TmCfg.bReport[7] = this.chkT8DoReport.Checked;
            Cfg.TmCfg.bReport[8] = this.chkT9DoReport.Checked;

            if (this.txtChamberTDiff1.Text == "-") Cfg.TmCfg.bUseTDiff[0] = false;  else Cfg.TmCfg.bUseTDiff[0] = true;
            if (this.txtChamberTDiff2.Text == "-") Cfg.TmCfg.bUseTDiff[1] = false;  else Cfg.TmCfg.bUseTDiff[1] = true;
            if (this.txtChamberTDiff3.Text == "-") Cfg.TmCfg.bUseTDiff[2] = false;  else Cfg.TmCfg.bUseTDiff[2] = true;
            if (this.txtChamberTDiff4.Text == "-") Cfg.TmCfg.bUseTDiff[3] = false;  else Cfg.TmCfg.bUseTDiff[3] = true;
            if (this.txtChamberTDiff5.Text == "-") Cfg.TmCfg.bUseTDiff[4] = false;  else Cfg.TmCfg.bUseTDiff[4] = true;
            if (this.txtChamberTDiff6.Text == "-") Cfg.TmCfg.bUseTDiff[5] = false;  else Cfg.TmCfg.bUseTDiff[5] = true;
            if (this.txtChamberTDiff7.Text == "-") Cfg.TmCfg.bUseTDiff[6] = false;  else Cfg.TmCfg.bUseTDiff[6] = true;
            if (this.txtChamberTDiff8.Text == "-") Cfg.TmCfg.bUseTDiff[7] = false;  else Cfg.TmCfg.bUseTDiff[7] = true;
            if (this.txtChamberTDiff9.Text == "-") Cfg.TmCfg.bUseTDiff[8] = false;  else Cfg.TmCfg.bUseTDiff[8] = true;

            if (this.txtChamberHDiff1.Text == "-") Cfg.TmCfg.bUseHDiff[0] = false;  else Cfg.TmCfg.bUseHDiff[0] = true;
            if (this.txtChamberHDiff2.Text == "-") Cfg.TmCfg.bUseHDiff[1] = false;  else Cfg.TmCfg.bUseHDiff[1] = true;
            if (this.txtChamberHDiff3.Text == "-") Cfg.TmCfg.bUseHDiff[2] = false;  else Cfg.TmCfg.bUseHDiff[2] = true;
            if (this.txtChamberHDiff4.Text == "-") Cfg.TmCfg.bUseHDiff[3] = false;  else Cfg.TmCfg.bUseHDiff[3] = true;
            if (this.txtChamberHDiff5.Text == "-") Cfg.TmCfg.bUseHDiff[4] = false;  else Cfg.TmCfg.bUseHDiff[4] = true;
            if (this.txtChamberHDiff6.Text == "-") Cfg.TmCfg.bUseHDiff[5] = false;  else Cfg.TmCfg.bUseHDiff[5] = true;
            if (this.txtChamberHDiff7.Text == "-") Cfg.TmCfg.bUseHDiff[6] = false;  else Cfg.TmCfg.bUseHDiff[6] = true;
            if (this.txtChamberHDiff8.Text == "-") Cfg.TmCfg.bUseHDiff[7] = false;  else Cfg.TmCfg.bUseHDiff[7] = true;
            if (this.txtChamberHDiff9.Text == "-") Cfg.TmCfg.bUseHDiff[8] = false; else Cfg.TmCfg.bUseHDiff[8] = true;

            if (this.txtChamberRamp1.Text == "-") Cfg.TmCfg.bUseRamp[0] = false; else Cfg.TmCfg.bUseRamp[0] = true;
            if (this.txtChamberRamp2.Text == "-") Cfg.TmCfg.bUseRamp[1] = false; else Cfg.TmCfg.bUseRamp[1] = true;
            if (this.txtChamberRamp3.Text == "-") Cfg.TmCfg.bUseRamp[2] = false; else Cfg.TmCfg.bUseRamp[2] = true;
            if (this.txtChamberRamp4.Text == "-") Cfg.TmCfg.bUseRamp[3] = false; else Cfg.TmCfg.bUseRamp[3] = true;
            if (this.txtChamberRamp5.Text == "-") Cfg.TmCfg.bUseRamp[4] = false; else Cfg.TmCfg.bUseRamp[4] = true;
            if (this.txtChamberRamp6.Text == "-") Cfg.TmCfg.bUseRamp[5] = false; else Cfg.TmCfg.bUseRamp[5] = true;
            if (this.txtChamberRamp7.Text == "-") Cfg.TmCfg.bUseRamp[6] = false; else Cfg.TmCfg.bUseRamp[6] = true;
            if (this.txtChamberRamp8.Text == "-") Cfg.TmCfg.bUseRamp[7] = false; else Cfg.TmCfg.bUseRamp[7] = true;
            if (this.txtChamberRamp9.Text == "-") Cfg.TmCfg.bUseRamp[8] = false; else Cfg.TmCfg.bUseRamp[8] = true;

            if (this.txtChamberUnif1.Text == "-") Cfg.TmCfg.bUseUnif[0] = false; else Cfg.TmCfg.bUseUnif[0] = true;
            if (this.txtChamberUnif2.Text == "-") Cfg.TmCfg.bUseUnif[1] = false; else Cfg.TmCfg.bUseUnif[1] = true;
            if (this.txtChamberUnif3.Text == "-") Cfg.TmCfg.bUseUnif[2] = false; else Cfg.TmCfg.bUseUnif[2] = true;
            if (this.txtChamberUnif4.Text == "-") Cfg.TmCfg.bUseUnif[3] = false; else Cfg.TmCfg.bUseUnif[3] = true;
            if (this.txtChamberUnif5.Text == "-") Cfg.TmCfg.bUseUnif[4] = false; else Cfg.TmCfg.bUseUnif[4] = true;
            if (this.txtChamberUnif6.Text == "-") Cfg.TmCfg.bUseUnif[5] = false; else Cfg.TmCfg.bUseUnif[5] = true;
            if (this.txtChamberUnif7.Text == "-") Cfg.TmCfg.bUseUnif[6] = false; else Cfg.TmCfg.bUseUnif[6] = true;
            if (this.txtChamberUnif8.Text == "-") Cfg.TmCfg.bUseUnif[7] = false; else Cfg.TmCfg.bUseUnif[7] = true;
            if (this.txtChamberUnif9.Text == "-") Cfg.TmCfg.bUseUnif[8] = false; else Cfg.TmCfg.bUseUnif[8] = true;

            if (this.txtChamberTOver1.Text == "-") Cfg.TmCfg.bUseTOver[0] = false; else Cfg.TmCfg.bUseTOver[0] = true;
            if (this.txtChamberTOver2.Text == "-") Cfg.TmCfg.bUseTOver[1] = false; else Cfg.TmCfg.bUseTOver[1] = true;
            if (this.txtChamberTOver3.Text == "-") Cfg.TmCfg.bUseTOver[2] = false; else Cfg.TmCfg.bUseTOver[2] = true;
            if (this.txtChamberTOver4.Text == "-") Cfg.TmCfg.bUseTOver[3] = false; else Cfg.TmCfg.bUseTOver[3] = true;
            if (this.txtChamberTOver5.Text == "-") Cfg.TmCfg.bUseTOver[4] = false; else Cfg.TmCfg.bUseTOver[4] = true;
            if (this.txtChamberTOver6.Text == "-") Cfg.TmCfg.bUseTOver[5] = false; else Cfg.TmCfg.bUseTOver[5] = true;
            if (this.txtChamberTOver7.Text == "-") Cfg.TmCfg.bUseTOver[6] = false; else Cfg.TmCfg.bUseTOver[6] = true;
            if (this.txtChamberTOver8.Text == "-") Cfg.TmCfg.bUseTOver[7] = false; else Cfg.TmCfg.bUseTOver[7] = true;
            if (this.txtChamberTOver9.Text == "-") Cfg.TmCfg.bUseTOver[8] = false; else Cfg.TmCfg.bUseTOver[8] = true;

            if (this.txtChamberHOver1.Text == "-") Cfg.TmCfg.bUseHOver[0] = false; else Cfg.TmCfg.bUseHOver[0] = true;
            if (this.txtChamberHOver2.Text == "-") Cfg.TmCfg.bUseHOver[1] = false; else Cfg.TmCfg.bUseHOver[1] = true;
            if (this.txtChamberHOver3.Text == "-") Cfg.TmCfg.bUseHOver[2] = false; else Cfg.TmCfg.bUseHOver[2] = true;
            if (this.txtChamberHOver4.Text == "-") Cfg.TmCfg.bUseHOver[3] = false; else Cfg.TmCfg.bUseHOver[3] = true;
            if (this.txtChamberHOver5.Text == "-") Cfg.TmCfg.bUseHOver[4] = false; else Cfg.TmCfg.bUseHOver[4] = true;
            if (this.txtChamberHOver6.Text == "-") Cfg.TmCfg.bUseHOver[5] = false; else Cfg.TmCfg.bUseHOver[5] = true;
            if (this.txtChamberHOver7.Text == "-") Cfg.TmCfg.bUseHOver[6] = false; else Cfg.TmCfg.bUseHOver[6] = true;
            if (this.txtChamberHOver8.Text == "-") Cfg.TmCfg.bUseHOver[7] = false; else Cfg.TmCfg.bUseHOver[7] = true;
            if (this.txtChamberHOver9.Text == "-") Cfg.TmCfg.bUseHOver[8] = false; else Cfg.TmCfg.bUseHOver[8] = true;

            // temp
            Cfg.TpCfg.TSp[0] = (this.txtTempTSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp1.Text, 1);
            Cfg.TpCfg.TSp[1] = (this.txtTempTSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp2.Text, 1);
            Cfg.TpCfg.TSp[2] = (this.txtTempTSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp3.Text, 1);
            Cfg.TpCfg.TSp[3] = (this.txtTempTSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp4.Text, 1);
            Cfg.TpCfg.TSp[4] = (this.txtTempTSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp5.Text, 1);
            
            Cfg.TpCfg.WaitTm[0] = (this.txtTempWTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm1.Text);
            Cfg.TpCfg.WaitTm[1] = (this.txtTempWTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm2.Text);
            Cfg.TpCfg.WaitTm[2] = (this.txtTempWTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm3.Text);
            Cfg.TpCfg.WaitTm[3] = (this.txtTempWTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm4.Text);
            Cfg.TpCfg.WaitTm[4] = (this.txtTempWTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm5.Text);
            
            Cfg.TpCfg.TestTm[0] = (this.txtTempTTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm1.Text);
            Cfg.TpCfg.TestTm[1] = (this.txtTempTTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm2.Text);
            Cfg.TpCfg.TestTm[2] = (this.txtTempTTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm3.Text);
            Cfg.TpCfg.TestTm[3] = (this.txtTempTTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm4.Text);
            Cfg.TpCfg.TestTm[4] = (this.txtTempTTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm5.Text);
            
            Cfg.TpCfg.TempDiff[0] = (this.txtTempTDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff1.Text, 1);
            Cfg.TpCfg.TempDiff[1] = (this.txtTempTDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff2.Text, 1);
            Cfg.TpCfg.TempDiff[2] = (this.txtTempTDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff3.Text, 1);
            Cfg.TpCfg.TempDiff[3] = (this.txtTempTDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff4.Text, 1);
            Cfg.TpCfg.TempDiff[4] = (this.txtTempTDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff5.Text, 1);
            
            Cfg.TpCfg.Ramp[0] = (this.txtTempRamp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp1.Text, 1);
            Cfg.TpCfg.Ramp[1] = (this.txtTempRamp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp2.Text, 1);
            Cfg.TpCfg.Ramp[2] = (this.txtTempRamp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp3.Text, 1);
            Cfg.TpCfg.Ramp[3] = (this.txtTempRamp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp4.Text, 1);
            Cfg.TpCfg.Ramp[4] = (this.txtTempRamp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp5.Text, 1);
            
            Cfg.TpCfg.Uniformity[0] = (this.txtTempUnif1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif1.Text, 1);
            Cfg.TpCfg.Uniformity[1] = (this.txtTempUnif2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif2.Text, 1);
            Cfg.TpCfg.Uniformity[2] = (this.txtTempUnif3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif3.Text, 1);
            Cfg.TpCfg.Uniformity[3] = (this.txtTempUnif4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif4.Text, 1);
            Cfg.TpCfg.Uniformity[4] = (this.txtTempUnif5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif5.Text, 1);
            
            Cfg.TpCfg.TOver[0] = (this.txtTempTOver1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver1.Text, 1);
            Cfg.TpCfg.TOver[1] = (this.txtTempTOver2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver2.Text, 1);
            Cfg.TpCfg.TOver[2] = (this.txtTempTOver3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver3.Text, 1);
            Cfg.TpCfg.TOver[3] = (this.txtTempTOver4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver4.Text, 1);
            Cfg.TpCfg.TOver[4] = (this.txtTempTOver5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver5.Text, 1);
            
            if (this.txtTempTDiff1.Text == "-") Cfg.TpCfg.bUseTDiff[0] = false; else Cfg.TpCfg.bUseTDiff[0] = true;
            if (this.txtTempTDiff2.Text == "-") Cfg.TpCfg.bUseTDiff[1] = false; else Cfg.TpCfg.bUseTDiff[1] = true;
            if (this.txtTempTDiff3.Text == "-") Cfg.TpCfg.bUseTDiff[2] = false; else Cfg.TpCfg.bUseTDiff[2] = true;
            if (this.txtTempTDiff4.Text == "-") Cfg.TpCfg.bUseTDiff[3] = false; else Cfg.TpCfg.bUseTDiff[3] = true;
            if (this.txtTempTDiff5.Text == "-") Cfg.TpCfg.bUseTDiff[4] = false; else Cfg.TpCfg.bUseTDiff[4] = true;
            
            if (this.txtTempRamp1.Text == "-") Cfg.TpCfg.bUseRamp[0] = false; else Cfg.TpCfg.bUseRamp[0] = true;
            if (this.txtTempRamp2.Text == "-") Cfg.TpCfg.bUseRamp[1] = false; else Cfg.TpCfg.bUseRamp[1] = true;
            if (this.txtTempRamp3.Text == "-") Cfg.TpCfg.bUseRamp[2] = false; else Cfg.TpCfg.bUseRamp[2] = true;
            if (this.txtTempRamp4.Text == "-") Cfg.TpCfg.bUseRamp[3] = false; else Cfg.TpCfg.bUseRamp[3] = true;
            if (this.txtTempRamp5.Text == "-") Cfg.TpCfg.bUseRamp[4] = false; else Cfg.TpCfg.bUseRamp[4] = true;
            
            if (this.txtTempUnif1.Text == "-") Cfg.TpCfg.bUseUnif[0] = false; else Cfg.TpCfg.bUseUnif[0] = true;
            if (this.txtTempUnif2.Text == "-") Cfg.TpCfg.bUseUnif[1] = false; else Cfg.TpCfg.bUseUnif[1] = true;
            if (this.txtTempUnif3.Text == "-") Cfg.TpCfg.bUseUnif[2] = false; else Cfg.TpCfg.bUseUnif[2] = true;
            if (this.txtTempUnif4.Text == "-") Cfg.TpCfg.bUseUnif[3] = false; else Cfg.TpCfg.bUseUnif[3] = true;
            if (this.txtTempUnif5.Text == "-") Cfg.TpCfg.bUseUnif[4] = false; else Cfg.TpCfg.bUseUnif[4] = true;
            
            if (this.txtTempTOver1.Text == "-") Cfg.TpCfg.bUseTOver[0] = false; else Cfg.TpCfg.bUseTOver[0] = true;
            if (this.txtTempTOver2.Text == "-") Cfg.TpCfg.bUseTOver[1] = false; else Cfg.TpCfg.bUseTOver[1] = true;
            if (this.txtTempTOver3.Text == "-") Cfg.TpCfg.bUseTOver[2] = false; else Cfg.TpCfg.bUseTOver[2] = true;
            if (this.txtTempTOver4.Text == "-") Cfg.TpCfg.bUseTOver[3] = false; else Cfg.TpCfg.bUseTOver[3] = true;
            if (this.txtTempTOver5.Text == "-") Cfg.TpCfg.bUseTOver[4] = false; else Cfg.TpCfg.bUseTOver[4] = true;
            
            // Temi Chiller.
            Cfg.TmChillerCfg.TSp[0] = (txtChillerTSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp1.Text, 1);
            Cfg.TmChillerCfg.TSp[1] = (txtChillerTSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp2.Text, 1);
            Cfg.TmChillerCfg.TSp[2] = (txtChillerTSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp3.Text, 1);
            Cfg.TmChillerCfg.TSp[3] = (txtChillerTSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp4.Text, 1);
            Cfg.TmChillerCfg.TSp[4] = (txtChillerTSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp5.Text, 1);
            Cfg.TmChillerCfg.TSp[5] = (txtChillerTSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp6.Text, 1);
            Cfg.TmChillerCfg.TSp[6] = (txtChillerTSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp7.Text, 1);
            Cfg.TmChillerCfg.TSp[7] = (txtChillerTSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp8.Text, 1);
            Cfg.TmChillerCfg.TSp[8] = (txtChillerTSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerTSp9.Text, 1);
                
            Cfg.TmChillerCfg.SSp[0] = (txtChillerSSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp1.Text, 2);
            Cfg.TmChillerCfg.SSp[1] = (txtChillerSSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp2.Text, 2);
            Cfg.TmChillerCfg.SSp[2] = (txtChillerSSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp3.Text, 2);
            Cfg.TmChillerCfg.SSp[3] = (txtChillerSSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp4.Text, 2);
            Cfg.TmChillerCfg.SSp[4] = (txtChillerSSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp5.Text, 2);
            Cfg.TmChillerCfg.SSp[5] = (txtChillerSSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp6.Text, 2);
            Cfg.TmChillerCfg.SSp[6] = (txtChillerSSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp7.Text, 2);
            Cfg.TmChillerCfg.SSp[7] = (txtChillerSSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp8.Text, 2);
            Cfg.TmChillerCfg.SSp[8] = (txtChillerSSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtChillerSSp9.Text, 2);

            Cfg.TmChillerCfg.WaitTm[0] = (this.txtChillerWTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm1.Text);
            Cfg.TmChillerCfg.WaitTm[1] = (this.txtChillerWTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm2.Text);
            Cfg.TmChillerCfg.WaitTm[2] = (this.txtChillerWTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm3.Text);
            Cfg.TmChillerCfg.WaitTm[3] = (this.txtChillerWTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm4.Text);
            Cfg.TmChillerCfg.WaitTm[4] = (this.txtChillerWTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm5.Text);
            Cfg.TmChillerCfg.WaitTm[5] = (this.txtChillerWTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm6.Text);
            Cfg.TmChillerCfg.WaitTm[6] = (this.txtChillerWTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm7.Text);
            Cfg.TmChillerCfg.WaitTm[7] = (this.txtChillerWTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm8.Text);
            Cfg.TmChillerCfg.WaitTm[8] = (this.txtChillerWTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerWTm9.Text);

            Cfg.TmChillerCfg.TestTm[0] = (this.txtChillerTTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm1.Text);
            Cfg.TmChillerCfg.TestTm[1] = (this.txtChillerTTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm2.Text);
            Cfg.TmChillerCfg.TestTm[2] = (this.txtChillerTTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm3.Text);
            Cfg.TmChillerCfg.TestTm[3] = (this.txtChillerTTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm4.Text);
            Cfg.TmChillerCfg.TestTm[4] = (this.txtChillerTTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm5.Text);
            Cfg.TmChillerCfg.TestTm[5] = (this.txtChillerTTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm6.Text);
            Cfg.TmChillerCfg.TestTm[6] = (this.txtChillerTTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm7.Text);
            Cfg.TmChillerCfg.TestTm[7] = (this.txtChillerTTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm8.Text);
            Cfg.TmChillerCfg.TestTm[8] = (this.txtChillerTTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChillerTTm9.Text);
                
            Cfg.TmChillerCfg.TDiff[0] = (this.txtChillerTDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff1.Text, 1);
            Cfg.TmChillerCfg.TDiff[1] = (this.txtChillerTDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff2.Text, 1);
            Cfg.TmChillerCfg.TDiff[2] = (this.txtChillerTDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff3.Text, 1);
            Cfg.TmChillerCfg.TDiff[3] = (this.txtChillerTDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff4.Text, 1);
            Cfg.TmChillerCfg.TDiff[4] = (this.txtChillerTDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff5.Text, 1);
            Cfg.TmChillerCfg.TDiff[5] = (this.txtChillerTDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff6.Text, 1);
            Cfg.TmChillerCfg.TDiff[6] = (this.txtChillerTDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff7.Text, 1);
            Cfg.TmChillerCfg.TDiff[7] = (this.txtChillerTDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff8.Text, 1);
            Cfg.TmChillerCfg.TDiff[8] = (this.txtChillerTDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerTDiff9.Text, 1);

            Cfg.TmChillerCfg.SDiff[0] = (this.txtChillerSDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff1.Text, 2);
            Cfg.TmChillerCfg.SDiff[1] = (this.txtChillerSDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff2.Text, 2);
            Cfg.TmChillerCfg.SDiff[2] = (this.txtChillerSDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff3.Text, 2);
            Cfg.TmChillerCfg.SDiff[3] = (this.txtChillerSDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff4.Text, 2);
            Cfg.TmChillerCfg.SDiff[4] = (this.txtChillerSDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff5.Text, 2);
            Cfg.TmChillerCfg.SDiff[5] = (this.txtChillerSDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff6.Text, 2);
            Cfg.TmChillerCfg.SDiff[6] = (this.txtChillerSDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff7.Text, 2);
            Cfg.TmChillerCfg.SDiff[7] = (this.txtChillerSDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff8.Text, 2);
            Cfg.TmChillerCfg.SDiff[8] = (this.txtChillerSDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerSDiff9.Text, 2);

            Cfg.TmChillerCfg.Ramp[0] = (this.txtChillerRamp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp1.Text, 1);
            Cfg.TmChillerCfg.Ramp[1] = (this.txtChillerRamp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp2.Text, 1);
            Cfg.TmChillerCfg.Ramp[2] = (this.txtChillerRamp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp3.Text, 1);
            Cfg.TmChillerCfg.Ramp[3] = (this.txtChillerRamp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp4.Text, 1);
            Cfg.TmChillerCfg.Ramp[4] = (this.txtChillerRamp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp5.Text, 1);
            Cfg.TmChillerCfg.Ramp[5] = (this.txtChillerRamp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp6.Text, 1);
            Cfg.TmChillerCfg.Ramp[6] = (this.txtChillerRamp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp7.Text, 1);
            Cfg.TmChillerCfg.Ramp[7] = (this.txtChillerRamp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp8.Text, 1);
            Cfg.TmChillerCfg.Ramp[8] = (this.txtChillerRamp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtChillerRamp9.Text, 1);

            if (this.txtChillerTDiff1.Text == "-") Cfg.TmChillerCfg.bUseTDiff[0] = false; else Cfg.TmChillerCfg.bUseTDiff[0] = true;
            if (this.txtChillerTDiff2.Text == "-") Cfg.TmChillerCfg.bUseTDiff[1] = false; else Cfg.TmChillerCfg.bUseTDiff[1] = true;
            if (this.txtChillerTDiff3.Text == "-") Cfg.TmChillerCfg.bUseTDiff[2] = false; else Cfg.TmChillerCfg.bUseTDiff[2] = true;
            if (this.txtChillerTDiff4.Text == "-") Cfg.TmChillerCfg.bUseTDiff[3] = false; else Cfg.TmChillerCfg.bUseTDiff[3] = true;
            if (this.txtChillerTDiff5.Text == "-") Cfg.TmChillerCfg.bUseTDiff[4] = false; else Cfg.TmChillerCfg.bUseTDiff[4] = true;
            if (this.txtChillerTDiff6.Text == "-") Cfg.TmChillerCfg.bUseTDiff[5] = false; else Cfg.TmChillerCfg.bUseTDiff[5] = true;
            if (this.txtChillerTDiff7.Text == "-") Cfg.TmChillerCfg.bUseTDiff[6] = false; else Cfg.TmChillerCfg.bUseTDiff[6] = true;
            if (this.txtChillerTDiff8.Text == "-") Cfg.TmChillerCfg.bUseTDiff[7] = false; else Cfg.TmChillerCfg.bUseTDiff[7] = true;
            if (this.txtChillerTDiff9.Text == "-") Cfg.TmChillerCfg.bUseTDiff[8] = false; else Cfg.TmChillerCfg.bUseTDiff[8] = true;
                                                       
            if (this.txtChillerSDiff1.Text == "-") Cfg.TmChillerCfg.bUseSDiff[0] = false; else Cfg.TmChillerCfg.bUseSDiff[0] = true;
            if (this.txtChillerSDiff2.Text == "-") Cfg.TmChillerCfg.bUseSDiff[1] = false; else Cfg.TmChillerCfg.bUseSDiff[1] = true;
            if (this.txtChillerSDiff3.Text == "-") Cfg.TmChillerCfg.bUseSDiff[2] = false; else Cfg.TmChillerCfg.bUseSDiff[2] = true;
            if (this.txtChillerSDiff4.Text == "-") Cfg.TmChillerCfg.bUseSDiff[3] = false; else Cfg.TmChillerCfg.bUseSDiff[3] = true;
            if (this.txtChillerSDiff5.Text == "-") Cfg.TmChillerCfg.bUseSDiff[4] = false; else Cfg.TmChillerCfg.bUseSDiff[4] = true;
            if (this.txtChillerSDiff6.Text == "-") Cfg.TmChillerCfg.bUseSDiff[5] = false; else Cfg.TmChillerCfg.bUseSDiff[5] = true;
            if (this.txtChillerSDiff7.Text == "-") Cfg.TmChillerCfg.bUseSDiff[6] = false; else Cfg.TmChillerCfg.bUseSDiff[6] = true;
            if (this.txtChillerSDiff8.Text == "-") Cfg.TmChillerCfg.bUseSDiff[7] = false; else Cfg.TmChillerCfg.bUseSDiff[7] = true;
            if (this.txtChillerSDiff9.Text == "-") Cfg.TmChillerCfg.bUseSDiff[8] = false; else Cfg.TmChillerCfg.bUseSDiff[8] = true;

            if (this.txtChillerRamp1.Text == "-") Cfg.TmChillerCfg.bUseRamp[0] = false; else Cfg.TmChillerCfg.bUseRamp[0] = true;
            if (this.txtChillerRamp2.Text == "-") Cfg.TmChillerCfg.bUseRamp[1] = false; else Cfg.TmChillerCfg.bUseRamp[1] = true;
            if (this.txtChillerRamp3.Text == "-") Cfg.TmChillerCfg.bUseRamp[2] = false; else Cfg.TmChillerCfg.bUseRamp[2] = true;
            if (this.txtChillerRamp4.Text == "-") Cfg.TmChillerCfg.bUseRamp[3] = false; else Cfg.TmChillerCfg.bUseRamp[3] = true;
            if (this.txtChillerRamp5.Text == "-") Cfg.TmChillerCfg.bUseRamp[4] = false; else Cfg.TmChillerCfg.bUseRamp[4] = true;
            if (this.txtChillerRamp6.Text == "-") Cfg.TmChillerCfg.bUseRamp[5] = false; else Cfg.TmChillerCfg.bUseRamp[5] = true;
            if (this.txtChillerRamp7.Text == "-") Cfg.TmChillerCfg.bUseRamp[6] = false; else Cfg.TmChillerCfg.bUseRamp[6] = true;
            if (this.txtChillerRamp8.Text == "-") Cfg.TmChillerCfg.bUseRamp[7] = false; else Cfg.TmChillerCfg.bUseRamp[7] = true;
            if (this.txtChillerRamp9.Text == "-") Cfg.TmChillerCfg.bUseRamp[8] = false; else Cfg.TmChillerCfg.bUseRamp[8] = true;

            // Temp Chiller.
            Cfg.TpChillerCfg.TSp[0] = (txtTpChillerTSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp1.Text, 1);
            Cfg.TpChillerCfg.TSp[1] = (txtTpChillerTSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp2.Text, 1);
            Cfg.TpChillerCfg.TSp[2] = (txtTpChillerTSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp3.Text, 1);
            Cfg.TpChillerCfg.TSp[3] = (txtTpChillerTSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp4.Text, 1);
            Cfg.TpChillerCfg.TSp[4] = (txtTpChillerTSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp5.Text, 1);
            Cfg.TpChillerCfg.TSp[5] = (txtTpChillerTSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp6.Text, 1);
            Cfg.TpChillerCfg.TSp[6] = (txtTpChillerTSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp7.Text, 1);
            Cfg.TpChillerCfg.TSp[7] = (txtTpChillerTSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp8.Text, 1);
            Cfg.TpChillerCfg.TSp[8] = (txtTpChillerTSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerTSp9.Text, 1);

            Cfg.TpChillerCfg.SSp[0] = (txtTpChillerSSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp1.Text, 2);
            Cfg.TpChillerCfg.SSp[1] = (txtTpChillerSSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp2.Text, 2);
            Cfg.TpChillerCfg.SSp[2] = (txtTpChillerSSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp3.Text, 2);
            Cfg.TpChillerCfg.SSp[3] = (txtTpChillerSSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp4.Text, 2);
            Cfg.TpChillerCfg.SSp[4] = (txtTpChillerSSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp5.Text, 2);
            Cfg.TpChillerCfg.SSp[5] = (txtTpChillerSSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp6.Text, 2);
            Cfg.TpChillerCfg.SSp[6] = (txtTpChillerSSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp7.Text, 2);
            Cfg.TpChillerCfg.SSp[7] = (txtTpChillerSSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp8.Text, 2);
            Cfg.TpChillerCfg.SSp[8] = (txtTpChillerSSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(txtTpChillerSSp9.Text, 2);

            Cfg.TpChillerCfg.WaitTm[0] = (this.txtTpChillerWTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm1.Text);
            Cfg.TpChillerCfg.WaitTm[1] = (this.txtTpChillerWTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm2.Text);
            Cfg.TpChillerCfg.WaitTm[2] = (this.txtTpChillerWTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm3.Text);
            Cfg.TpChillerCfg.WaitTm[3] = (this.txtTpChillerWTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm4.Text);
            Cfg.TpChillerCfg.WaitTm[4] = (this.txtTpChillerWTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm5.Text);
            Cfg.TpChillerCfg.WaitTm[5] = (this.txtTpChillerWTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm6.Text);
            Cfg.TpChillerCfg.WaitTm[6] = (this.txtTpChillerWTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm7.Text);
            Cfg.TpChillerCfg.WaitTm[7] = (this.txtTpChillerWTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm8.Text);
            Cfg.TpChillerCfg.WaitTm[8] = (this.txtTpChillerWTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerWTm9.Text);
                
            Cfg.TpChillerCfg.TestTm[0] = (this.txtTpChillerTTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm1.Text);
            Cfg.TpChillerCfg.TestTm[1] = (this.txtTpChillerTTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm2.Text);
            Cfg.TpChillerCfg.TestTm[2] = (this.txtTpChillerTTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm3.Text);
            Cfg.TpChillerCfg.TestTm[3] = (this.txtTpChillerTTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm4.Text);
            Cfg.TpChillerCfg.TestTm[4] = (this.txtTpChillerTTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm5.Text);
            Cfg.TpChillerCfg.TestTm[5] = (this.txtTpChillerTTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm6.Text);
            Cfg.TpChillerCfg.TestTm[6] = (this.txtTpChillerTTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm7.Text);
            Cfg.TpChillerCfg.TestTm[7] = (this.txtTpChillerTTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm8.Text);
            Cfg.TpChillerCfg.TestTm[8] = (this.txtTpChillerTTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTpChillerTTm9.Text);
                
            Cfg.TpChillerCfg.TDiff[0] = (this.txtTpChillerTDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff1.Text, 1);
            Cfg.TpChillerCfg.TDiff[1] = (this.txtTpChillerTDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff2.Text, 1);
            Cfg.TpChillerCfg.TDiff[2] = (this.txtTpChillerTDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff3.Text, 1);
            Cfg.TpChillerCfg.TDiff[3] = (this.txtTpChillerTDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff4.Text, 1);
            Cfg.TpChillerCfg.TDiff[4] = (this.txtTpChillerTDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff5.Text, 1);
            Cfg.TpChillerCfg.TDiff[5] = (this.txtTpChillerTDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff6.Text, 1);
            Cfg.TpChillerCfg.TDiff[6] = (this.txtTpChillerTDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff7.Text, 1);
            Cfg.TpChillerCfg.TDiff[7] = (this.txtTpChillerTDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff8.Text, 1);
            Cfg.TpChillerCfg.TDiff[8] = (this.txtTpChillerTDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerTDiff9.Text, 1);

            Cfg.TpChillerCfg.SDiff[0] = (this.txtTpChillerSDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff1.Text, 2);
            Cfg.TpChillerCfg.SDiff[1] = (this.txtTpChillerSDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff2.Text, 2);
            Cfg.TpChillerCfg.SDiff[2] = (this.txtTpChillerSDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff3.Text, 2);
            Cfg.TpChillerCfg.SDiff[3] = (this.txtTpChillerSDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff4.Text, 2);
            Cfg.TpChillerCfg.SDiff[4] = (this.txtTpChillerSDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff5.Text, 2);
            Cfg.TpChillerCfg.SDiff[5] = (this.txtTpChillerSDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff6.Text, 2);
            Cfg.TpChillerCfg.SDiff[6] = (this.txtTpChillerSDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff7.Text, 2);
            Cfg.TpChillerCfg.SDiff[7] = (this.txtTpChillerSDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff8.Text, 2);
            Cfg.TpChillerCfg.SDiff[8] = (this.txtTpChillerSDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerSDiff9.Text, 2);

            Cfg.TpChillerCfg.Ramp[0] = (this.txtTpChillerRamp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp1.Text, 1);
            Cfg.TpChillerCfg.Ramp[1] = (this.txtTpChillerRamp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp2.Text, 1);
            Cfg.TpChillerCfg.Ramp[2] = (this.txtTpChillerRamp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp3.Text, 1);
            Cfg.TpChillerCfg.Ramp[3] = (this.txtTpChillerRamp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp4.Text, 1);
            Cfg.TpChillerCfg.Ramp[4] = (this.txtTpChillerRamp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp5.Text, 1);
            Cfg.TpChillerCfg.Ramp[5] = (this.txtTpChillerRamp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp6.Text, 1);
            Cfg.TpChillerCfg.Ramp[6] = (this.txtTpChillerRamp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp7.Text, 1);
            Cfg.TpChillerCfg.Ramp[7] = (this.txtTpChillerRamp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp8.Text, 1);
            Cfg.TpChillerCfg.Ramp[8] = (this.txtTpChillerRamp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTpChillerRamp9.Text, 1);

            if (this.txtTpChillerTDiff1.Text == "-") Cfg.TpChillerCfg.bUseTDiff[0] = false; else Cfg.TpChillerCfg.bUseTDiff[0] = true;
            if (this.txtTpChillerTDiff2.Text == "-") Cfg.TpChillerCfg.bUseTDiff[1] = false; else Cfg.TpChillerCfg.bUseTDiff[1] = true;
            if (this.txtTpChillerTDiff3.Text == "-") Cfg.TpChillerCfg.bUseTDiff[2] = false; else Cfg.TpChillerCfg.bUseTDiff[2] = true;
            if (this.txtTpChillerTDiff4.Text == "-") Cfg.TpChillerCfg.bUseTDiff[3] = false; else Cfg.TpChillerCfg.bUseTDiff[3] = true;
            if (this.txtTpChillerTDiff5.Text == "-") Cfg.TpChillerCfg.bUseTDiff[4] = false; else Cfg.TpChillerCfg.bUseTDiff[4] = true;
            if (this.txtTpChillerTDiff6.Text == "-") Cfg.TpChillerCfg.bUseTDiff[5] = false; else Cfg.TpChillerCfg.bUseTDiff[5] = true;
            if (this.txtTpChillerTDiff7.Text == "-") Cfg.TpChillerCfg.bUseTDiff[6] = false; else Cfg.TpChillerCfg.bUseTDiff[6] = true;
            if (this.txtTpChillerTDiff8.Text == "-") Cfg.TpChillerCfg.bUseTDiff[7] = false; else Cfg.TpChillerCfg.bUseTDiff[7] = true;
            if (this.txtTpChillerTDiff9.Text == "-") Cfg.TpChillerCfg.bUseTDiff[8] = false; else Cfg.TpChillerCfg.bUseTDiff[8] = true;

            if (this.txtTpChillerSDiff1.Text == "-") Cfg.TpChillerCfg.bUseSDiff[0] = false; else Cfg.TpChillerCfg.bUseSDiff[0] = true;
            if (this.txtTpChillerSDiff2.Text == "-") Cfg.TpChillerCfg.bUseSDiff[1] = false; else Cfg.TpChillerCfg.bUseSDiff[1] = true;
            if (this.txtTpChillerSDiff3.Text == "-") Cfg.TpChillerCfg.bUseSDiff[2] = false; else Cfg.TpChillerCfg.bUseSDiff[2] = true;
            if (this.txtTpChillerSDiff4.Text == "-") Cfg.TpChillerCfg.bUseSDiff[3] = false; else Cfg.TpChillerCfg.bUseSDiff[3] = true;
            if (this.txtTpChillerSDiff5.Text == "-") Cfg.TpChillerCfg.bUseSDiff[4] = false; else Cfg.TpChillerCfg.bUseSDiff[4] = true;
            if (this.txtTpChillerSDiff6.Text == "-") Cfg.TpChillerCfg.bUseSDiff[5] = false; else Cfg.TpChillerCfg.bUseSDiff[5] = true;
            if (this.txtTpChillerSDiff7.Text == "-") Cfg.TpChillerCfg.bUseSDiff[6] = false; else Cfg.TpChillerCfg.bUseSDiff[6] = true;
            if (this.txtTpChillerSDiff8.Text == "-") Cfg.TpChillerCfg.bUseSDiff[7] = false; else Cfg.TpChillerCfg.bUseSDiff[7] = true;
            if (this.txtTpChillerSDiff9.Text == "-") Cfg.TpChillerCfg.bUseSDiff[8] = false; else Cfg.TpChillerCfg.bUseSDiff[8] = true;

            if (this.txtTpChillerRamp1.Text == "-") Cfg.TpChillerCfg.bUseRamp[0] = false; else Cfg.TpChillerCfg.bUseRamp[0] = true;
            if (this.txtTpChillerRamp2.Text == "-") Cfg.TpChillerCfg.bUseRamp[1] = false; else Cfg.TpChillerCfg.bUseRamp[1] = true;
            if (this.txtTpChillerRamp3.Text == "-") Cfg.TpChillerCfg.bUseRamp[2] = false; else Cfg.TpChillerCfg.bUseRamp[2] = true;
            if (this.txtTpChillerRamp4.Text == "-") Cfg.TpChillerCfg.bUseRamp[3] = false; else Cfg.TpChillerCfg.bUseRamp[3] = true;
            if (this.txtTpChillerRamp5.Text == "-") Cfg.TpChillerCfg.bUseRamp[4] = false; else Cfg.TpChillerCfg.bUseRamp[4] = true;
            if (this.txtTpChillerRamp6.Text == "-") Cfg.TpChillerCfg.bUseRamp[5] = false; else Cfg.TpChillerCfg.bUseRamp[5] = true;
            if (this.txtTpChillerRamp7.Text == "-") Cfg.TpChillerCfg.bUseRamp[6] = false; else Cfg.TpChillerCfg.bUseRamp[6] = true;
            if (this.txtTpChillerRamp8.Text == "-") Cfg.TpChillerCfg.bUseRamp[7] = false; else Cfg.TpChillerCfg.bUseRamp[7] = true;
            if (this.txtTpChillerRamp9.Text == "-") Cfg.TpChillerCfg.bUseRamp[8] = false; else Cfg.TpChillerCfg.bUseRamp[8] = true;
        }
    }
}

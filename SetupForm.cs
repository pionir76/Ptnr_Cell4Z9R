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
            // Title
            this.edtTitle.Text = Cfg.title;

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
            txtChamberCtrStblTm1.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();

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
            txtChamberCtrStblTm2.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();

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
            txtChamberCtrStblTm3.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm4.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm5.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm6.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm7.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm8.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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
            txtChamberCtrStblTm9.Text = (Cfg.TmCfg.CtrlStblTm[idx] == 0) ? "-" : Cfg.TmCfg.CtrlStblTm[idx].ToString();
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

            txtTempCtlStblTm1.Text = (Cfg.TpCfg.CtrlStblTm[0] == 0) ? "-" : Cfg.TpCfg.CtrlStblTm[0].ToString();
            txtTempCtlStblTm2.Text = (Cfg.TpCfg.CtrlStblTm[1] == 0) ? "-" : Cfg.TpCfg.CtrlStblTm[1].ToString();
            txtTempCtlStblTm3.Text = (Cfg.TpCfg.CtrlStblTm[2] == 0) ? "-" : Cfg.TpCfg.CtrlStblTm[2].ToString();
            txtTempCtlStblTm4.Text = (Cfg.TpCfg.CtrlStblTm[3] == 0) ? "-" : Cfg.TpCfg.CtrlStblTm[3].ToString();
            txtTempCtlStblTm5.Text = (Cfg.TpCfg.CtrlStblTm[4] == 0) ? "-" : Cfg.TpCfg.CtrlStblTm[4].ToString();
        }

        private void OnOk(object sender, EventArgs e)
        {
            Cfg.title = this.edtTitle.Text;

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

            Cfg.TmCfg.CtrlStblTm[0] = (this.txtChamberCtrStblTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm1.Text);
            Cfg.TmCfg.CtrlStblTm[1] = (this.txtChamberCtrStblTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm2.Text);
            Cfg.TmCfg.CtrlStblTm[2] = (this.txtChamberCtrStblTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm3.Text);
            Cfg.TmCfg.CtrlStblTm[3] = (this.txtChamberCtrStblTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm4.Text);
            Cfg.TmCfg.CtrlStblTm[4] = (this.txtChamberCtrStblTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm5.Text);
            Cfg.TmCfg.CtrlStblTm[5] = (this.txtChamberCtrStblTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm6.Text);
            Cfg.TmCfg.CtrlStblTm[6] = (this.txtChamberCtrStblTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm7.Text);
            Cfg.TmCfg.CtrlStblTm[7] = (this.txtChamberCtrStblTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm8.Text);
            Cfg.TmCfg.CtrlStblTm[8] = (this.txtChamberCtrStblTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtChamberCtrStblTm9.Text);

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

            Cfg.TpCfg.CtrlStblTm[0] = (this.txtTempCtlStblTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempCtlStblTm1.Text);
            Cfg.TpCfg.CtrlStblTm[1] = (this.txtTempCtlStblTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempCtlStblTm2.Text);
            Cfg.TpCfg.CtrlStblTm[2] = (this.txtTempCtlStblTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempCtlStblTm3.Text);
            Cfg.TpCfg.CtrlStblTm[3] = (this.txtTempCtlStblTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempCtlStblTm4.Text);
            Cfg.TpCfg.CtrlStblTm[4] = (this.txtTempCtlStblTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempCtlStblTm5.Text);

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

        }
    }
}

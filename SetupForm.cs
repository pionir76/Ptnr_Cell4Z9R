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

            txtTempTSp1.Text = SysDefs.DotString(Cfg.TpCfg.TSp[0], 1);
            txtTempTSp2.Text = SysDefs.DotString(Cfg.TpCfg.TSp[1], 1);
            txtTempTSp3.Text = SysDefs.DotString(Cfg.TpCfg.TSp[2], 1);
            txtTempTSp4.Text = SysDefs.DotString(Cfg.TpCfg.TSp[3], 1);
            txtTempTSp5.Text = SysDefs.DotString(Cfg.TpCfg.TSp[4], 1);
            txtTempTSp6.Text = SysDefs.DotString(Cfg.TpCfg.TSp[5], 1);
            txtTempTSp7.Text = SysDefs.DotString(Cfg.TpCfg.TSp[6], 1);
            txtTempTSp8.Text = SysDefs.DotString(Cfg.TpCfg.TSp[7], 1);
            txtTempTSp9.Text = SysDefs.DotString(Cfg.TpCfg.TSp[8], 1);

            txtTempWTm1.Text = (Cfg.TpCfg.WaitTm[0] == 0) ? "-" : Cfg.TpCfg.WaitTm[0].ToString();
            txtTempWTm2.Text = (Cfg.TpCfg.WaitTm[1] == 0) ? "-" : Cfg.TpCfg.WaitTm[1].ToString();
            txtTempWTm3.Text = (Cfg.TpCfg.WaitTm[2] == 0) ? "-" : Cfg.TpCfg.WaitTm[2].ToString();
            txtTempWTm4.Text = (Cfg.TpCfg.WaitTm[3] == 0) ? "-" : Cfg.TpCfg.WaitTm[3].ToString();
            txtTempWTm5.Text = (Cfg.TpCfg.WaitTm[4] == 0) ? "-" : Cfg.TpCfg.WaitTm[4].ToString();
            txtTempWTm6.Text = (Cfg.TpCfg.WaitTm[5] == 0) ? "-" : Cfg.TpCfg.WaitTm[5].ToString();
            txtTempWTm7.Text = (Cfg.TpCfg.WaitTm[6] == 0) ? "-" : Cfg.TpCfg.WaitTm[6].ToString();
            txtTempWTm8.Text = (Cfg.TpCfg.WaitTm[7] == 0) ? "-" : Cfg.TpCfg.WaitTm[7].ToString();
            txtTempWTm9.Text = (Cfg.TpCfg.WaitTm[8] == 0) ? "-" : Cfg.TpCfg.WaitTm[8].ToString();

            txtTempTTm1.Text = (Cfg.TpCfg.TestTm[0] == 0) ? "-" : Cfg.TpCfg.TestTm[0].ToString();
            txtTempTTm2.Text = (Cfg.TpCfg.TestTm[1] == 0) ? "-" : Cfg.TpCfg.TestTm[1].ToString();
            txtTempTTm3.Text = (Cfg.TpCfg.TestTm[2] == 0) ? "-" : Cfg.TpCfg.TestTm[2].ToString();
            txtTempTTm4.Text = (Cfg.TpCfg.TestTm[3] == 0) ? "-" : Cfg.TpCfg.TestTm[3].ToString();
            txtTempTTm5.Text = (Cfg.TpCfg.TestTm[4] == 0) ? "-" : Cfg.TpCfg.TestTm[4].ToString();
            txtTempTTm6.Text = (Cfg.TpCfg.TestTm[5] == 0) ? "-" : Cfg.TpCfg.TestTm[5].ToString();
            txtTempTTm7.Text = (Cfg.TpCfg.TestTm[6] == 0) ? "-" : Cfg.TpCfg.TestTm[6].ToString();
            txtTempTTm8.Text = (Cfg.TpCfg.TestTm[7] == 0) ? "-" : Cfg.TpCfg.TestTm[7].ToString();
            txtTempTTm9.Text = (Cfg.TpCfg.TestTm[8] == 0) ? "-" : Cfg.TpCfg.TestTm[8].ToString();

            txtTempTDiff1.Text = (Cfg.TpCfg.bUseTDiff[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[0], 1);
            txtTempTDiff2.Text = (Cfg.TpCfg.bUseTDiff[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[1], 1);
            txtTempTDiff3.Text = (Cfg.TpCfg.bUseTDiff[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[2], 1);
            txtTempTDiff4.Text = (Cfg.TpCfg.bUseTDiff[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[3], 1);
            txtTempTDiff5.Text = (Cfg.TpCfg.bUseTDiff[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[4], 1);
            txtTempTDiff6.Text = (Cfg.TpCfg.bUseTDiff[5] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[5], 1);
            txtTempTDiff7.Text = (Cfg.TpCfg.bUseTDiff[6] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[6], 1);
            txtTempTDiff8.Text = (Cfg.TpCfg.bUseTDiff[7] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[7], 1);
            txtTempTDiff9.Text = (Cfg.TpCfg.bUseTDiff[8] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TempDiff[8], 1);

            txtTempRamp1.Text = (Cfg.TpCfg.bUseRamp[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[0], 1);
            txtTempRamp2.Text = (Cfg.TpCfg.bUseRamp[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[1], 1);
            txtTempRamp3.Text = (Cfg.TpCfg.bUseRamp[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[2], 1);
            txtTempRamp4.Text = (Cfg.TpCfg.bUseRamp[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[3], 1);
            txtTempRamp5.Text = (Cfg.TpCfg.bUseRamp[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[4], 1);
            txtTempRamp6.Text = (Cfg.TpCfg.bUseRamp[5] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[5], 1);
            txtTempRamp7.Text = (Cfg.TpCfg.bUseRamp[6] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[6], 1);
            txtTempRamp8.Text = (Cfg.TpCfg.bUseRamp[7] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[7], 1);
            txtTempRamp9.Text = (Cfg.TpCfg.bUseRamp[8] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Ramp[8], 1);

            txtTempUnif1.Text = (Cfg.TpCfg.bUseUnif[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[0], 1);
            txtTempUnif2.Text = (Cfg.TpCfg.bUseUnif[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[1], 1);
            txtTempUnif3.Text = (Cfg.TpCfg.bUseUnif[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[2], 1);
            txtTempUnif4.Text = (Cfg.TpCfg.bUseUnif[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[3], 1);
            txtTempUnif5.Text = (Cfg.TpCfg.bUseUnif[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[4], 1);
            txtTempUnif6.Text = (Cfg.TpCfg.bUseUnif[5] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[5], 1);
            txtTempUnif7.Text = (Cfg.TpCfg.bUseUnif[6] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[6], 1);
            txtTempUnif8.Text = (Cfg.TpCfg.bUseUnif[7] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[7], 1);
            txtTempUnif9.Text = (Cfg.TpCfg.bUseUnif[8] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.Uniformity[8], 1);

            txtTempTOver1.Text = (Cfg.TpCfg.bUseTOver[0] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[0], 1);
            txtTempTOver2.Text = (Cfg.TpCfg.bUseTOver[1] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[1], 1);
            txtTempTOver3.Text = (Cfg.TpCfg.bUseTOver[2] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[2], 1);
            txtTempTOver4.Text = (Cfg.TpCfg.bUseTOver[3] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[3], 1);
            txtTempTOver5.Text = (Cfg.TpCfg.bUseTOver[4] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[4], 1);
            txtTempTOver6.Text = (Cfg.TpCfg.bUseTOver[5] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[5], 1);
            txtTempTOver7.Text = (Cfg.TpCfg.bUseTOver[6] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[6], 1);
            txtTempTOver8.Text = (Cfg.TpCfg.bUseTOver[7] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[7], 1);
            txtTempTOver9.Text = (Cfg.TpCfg.bUseTOver[8] == false) ? "-" : SysDefs.DotString(Cfg.TpCfg.TOver[8], 1);

            txtTempUnifStblTm1.Text = (Cfg.TpCfg.StblTm[0] == 0) ? "-" : Cfg.TpCfg.StblTm[0].ToString();
            txtTempUnifStblTm2.Text = (Cfg.TpCfg.StblTm[1] == 0) ? "-" : Cfg.TpCfg.StblTm[1].ToString();
            txtTempUnifStblTm3.Text = (Cfg.TpCfg.StblTm[2] == 0) ? "-" : Cfg.TpCfg.StblTm[2].ToString();
            txtTempUnifStblTm4.Text = (Cfg.TpCfg.StblTm[3] == 0) ? "-" : Cfg.TpCfg.StblTm[3].ToString();
            txtTempUnifStblTm5.Text = (Cfg.TpCfg.StblTm[4] == 0) ? "-" : Cfg.TpCfg.StblTm[4].ToString();
            txtTempUnifStblTm6.Text = (Cfg.TpCfg.StblTm[5] == 0) ? "-" : Cfg.TpCfg.StblTm[5].ToString();
            txtTempUnifStblTm7.Text = (Cfg.TpCfg.StblTm[6] == 0) ? "-" : Cfg.TpCfg.StblTm[6].ToString();
            txtTempUnifStblTm8.Text = (Cfg.TpCfg.StblTm[7] == 0) ? "-" : Cfg.TpCfg.StblTm[7].ToString();
            txtTempUnifStblTm9.Text = (Cfg.TpCfg.StblTm[8] == 0) ? "-" : Cfg.TpCfg.StblTm[8].ToString();


            txtWarmUpSp.Text = SysDefs.DotString(Cfg.WarmUpSp, 1);
            txtWarmUpTm.Text = Cfg.WarmUpTm.ToString();
            chkWarmUp.Checked = Cfg.DoWarmUp;
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

            // temp
            Cfg.TpCfg.TSp[0] = (this.txtTempTSp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp1.Text, 1);
            Cfg.TpCfg.TSp[1] = (this.txtTempTSp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp2.Text, 1);
            Cfg.TpCfg.TSp[2] = (this.txtTempTSp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp3.Text, 1);
            Cfg.TpCfg.TSp[3] = (this.txtTempTSp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp4.Text, 1);
            Cfg.TpCfg.TSp[4] = (this.txtTempTSp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp5.Text, 1);
            Cfg.TpCfg.TSp[5] = (this.txtTempTSp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp6.Text, 1);
            Cfg.TpCfg.TSp[6] = (this.txtTempTSp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp7.Text, 1);
            Cfg.TpCfg.TSp[7] = (this.txtTempTSp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp8.Text, 1);
            Cfg.TpCfg.TSp[8] = (this.txtTempTSp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTSp9.Text, 1);

            Cfg.TpCfg.WaitTm[0] = (this.txtTempWTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm1.Text);
            Cfg.TpCfg.WaitTm[1] = (this.txtTempWTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm2.Text);
            Cfg.TpCfg.WaitTm[2] = (this.txtTempWTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm3.Text);
            Cfg.TpCfg.WaitTm[3] = (this.txtTempWTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm4.Text);
            Cfg.TpCfg.WaitTm[4] = (this.txtTempWTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm5.Text);
            Cfg.TpCfg.WaitTm[5] = (this.txtTempWTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm6.Text);
            Cfg.TpCfg.WaitTm[6] = (this.txtTempWTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm7.Text);
            Cfg.TpCfg.WaitTm[7] = (this.txtTempWTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm8.Text);
            Cfg.TpCfg.WaitTm[8] = (this.txtTempWTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempWTm9.Text);

            Cfg.TpCfg.TestTm[0] = (this.txtTempTTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm1.Text);
            Cfg.TpCfg.TestTm[1] = (this.txtTempTTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm2.Text);
            Cfg.TpCfg.TestTm[2] = (this.txtTempTTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm3.Text);
            Cfg.TpCfg.TestTm[3] = (this.txtTempTTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm4.Text);
            Cfg.TpCfg.TestTm[4] = (this.txtTempTTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm5.Text);
            Cfg.TpCfg.TestTm[5] = (this.txtTempTTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm6.Text);
            Cfg.TpCfg.TestTm[6] = (this.txtTempTTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm7.Text);
            Cfg.TpCfg.TestTm[7] = (this.txtTempTTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm8.Text);
            Cfg.TpCfg.TestTm[8] = (this.txtTempTTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempTTm9.Text);

            Cfg.TpCfg.TempDiff[0] = (this.txtTempTDiff1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff1.Text, 1);
            Cfg.TpCfg.TempDiff[1] = (this.txtTempTDiff2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff2.Text, 1);
            Cfg.TpCfg.TempDiff[2] = (this.txtTempTDiff3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff3.Text, 1);
            Cfg.TpCfg.TempDiff[3] = (this.txtTempTDiff4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff4.Text, 1);
            Cfg.TpCfg.TempDiff[4] = (this.txtTempTDiff5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff5.Text, 1);
            Cfg.TpCfg.TempDiff[5] = (this.txtTempTDiff6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff6.Text, 1);
            Cfg.TpCfg.TempDiff[6] = (this.txtTempTDiff7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff7.Text, 1);
            Cfg.TpCfg.TempDiff[7] = (this.txtTempTDiff8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff8.Text, 1);
            Cfg.TpCfg.TempDiff[8] = (this.txtTempTDiff9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTDiff9.Text, 1);

            Cfg.TpCfg.Ramp[0] = (this.txtTempRamp1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp1.Text, 1);
            Cfg.TpCfg.Ramp[1] = (this.txtTempRamp2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp2.Text, 1);
            Cfg.TpCfg.Ramp[2] = (this.txtTempRamp3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp3.Text, 1);
            Cfg.TpCfg.Ramp[3] = (this.txtTempRamp4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp4.Text, 1);
            Cfg.TpCfg.Ramp[4] = (this.txtTempRamp5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp5.Text, 1);
            Cfg.TpCfg.Ramp[5] = (this.txtTempRamp6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp6.Text, 1);
            Cfg.TpCfg.Ramp[6] = (this.txtTempRamp7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp7.Text, 1);
            Cfg.TpCfg.Ramp[7] = (this.txtTempRamp8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp8.Text, 1);
            Cfg.TpCfg.Ramp[8] = (this.txtTempRamp9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempRamp9.Text, 1);

            Cfg.TpCfg.Uniformity[0] = (this.txtTempUnif1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif1.Text, 1);
            Cfg.TpCfg.Uniformity[1] = (this.txtTempUnif2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif2.Text, 1);
            Cfg.TpCfg.Uniformity[2] = (this.txtTempUnif3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif3.Text, 1);
            Cfg.TpCfg.Uniformity[3] = (this.txtTempUnif4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif4.Text, 1);
            Cfg.TpCfg.Uniformity[4] = (this.txtTempUnif5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif5.Text, 1);
            Cfg.TpCfg.Uniformity[5] = (this.txtTempUnif6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif6.Text, 1);
            Cfg.TpCfg.Uniformity[6] = (this.txtTempUnif7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif7.Text, 1);
            Cfg.TpCfg.Uniformity[7] = (this.txtTempUnif8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif8.Text, 1);
            Cfg.TpCfg.Uniformity[8] = (this.txtTempUnif9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempUnif9.Text, 1);

            Cfg.TpCfg.TOver[0] = (this.txtTempTOver1.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver1.Text, 1);
            Cfg.TpCfg.TOver[1] = (this.txtTempTOver2.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver2.Text, 1);
            Cfg.TpCfg.TOver[2] = (this.txtTempTOver3.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver3.Text, 1);
            Cfg.TpCfg.TOver[3] = (this.txtTempTOver4.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver4.Text, 1);
            Cfg.TpCfg.TOver[4] = (this.txtTempTOver5.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver5.Text, 1);
            Cfg.TpCfg.TOver[5] = (this.txtTempTOver6.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver6.Text, 1);
            Cfg.TpCfg.TOver[6] = (this.txtTempTOver7.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver7.Text, 1);
            Cfg.TpCfg.TOver[7] = (this.txtTempTOver8.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver8.Text, 1);
            Cfg.TpCfg.TOver[8] = (this.txtTempTOver9.Text == "-") ? (short)0 : SysDefs.DotStringToVal(this.txtTempTOver9.Text, 1);

            Cfg.TpCfg.StblTm[0] = (this.txtTempUnifStblTm1.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm1.Text);
            Cfg.TpCfg.StblTm[1] = (this.txtTempUnifStblTm2.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm2.Text);
            Cfg.TpCfg.StblTm[2] = (this.txtTempUnifStblTm3.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm3.Text);
            Cfg.TpCfg.StblTm[3] = (this.txtTempUnifStblTm4.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm4.Text);
            Cfg.TpCfg.StblTm[4] = (this.txtTempUnifStblTm5.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm5.Text);
            Cfg.TpCfg.StblTm[5] = (this.txtTempUnifStblTm6.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm6.Text);
            Cfg.TpCfg.StblTm[6] = (this.txtTempUnifStblTm7.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm7.Text);
            Cfg.TpCfg.StblTm[7] = (this.txtTempUnifStblTm8.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm8.Text);
            Cfg.TpCfg.StblTm[8] = (this.txtTempUnifStblTm9.Text == "-") ? (short)0 : Convert.ToInt32(this.txtTempUnifStblTm9.Text);

            if (this.txtTempTDiff1.Text == "-") Cfg.TpCfg.bUseTDiff[0] = false; else Cfg.TpCfg.bUseTDiff[0] = true;
            if (this.txtTempTDiff2.Text == "-") Cfg.TpCfg.bUseTDiff[1] = false; else Cfg.TpCfg.bUseTDiff[1] = true;
            if (this.txtTempTDiff3.Text == "-") Cfg.TpCfg.bUseTDiff[2] = false; else Cfg.TpCfg.bUseTDiff[2] = true;
            if (this.txtTempTDiff4.Text == "-") Cfg.TpCfg.bUseTDiff[3] = false; else Cfg.TpCfg.bUseTDiff[3] = true;
            if (this.txtTempTDiff5.Text == "-") Cfg.TpCfg.bUseTDiff[4] = false; else Cfg.TpCfg.bUseTDiff[4] = true;
            if (this.txtTempTDiff6.Text == "-") Cfg.TpCfg.bUseTDiff[5] = false; else Cfg.TpCfg.bUseTDiff[5] = true;
            if (this.txtTempTDiff7.Text == "-") Cfg.TpCfg.bUseTDiff[6] = false; else Cfg.TpCfg.bUseTDiff[6] = true;
            if (this.txtTempTDiff8.Text == "-") Cfg.TpCfg.bUseTDiff[7] = false; else Cfg.TpCfg.bUseTDiff[7] = true;
            if (this.txtTempTDiff9.Text == "-") Cfg.TpCfg.bUseTDiff[8] = false; else Cfg.TpCfg.bUseTDiff[8] = true;

            if (this.txtTempRamp1.Text == "-") Cfg.TpCfg.bUseRamp[0] = false; else Cfg.TpCfg.bUseRamp[0] = true;
            if (this.txtTempRamp2.Text == "-") Cfg.TpCfg.bUseRamp[1] = false; else Cfg.TpCfg.bUseRamp[1] = true;
            if (this.txtTempRamp3.Text == "-") Cfg.TpCfg.bUseRamp[2] = false; else Cfg.TpCfg.bUseRamp[2] = true;
            if (this.txtTempRamp4.Text == "-") Cfg.TpCfg.bUseRamp[3] = false; else Cfg.TpCfg.bUseRamp[3] = true;
            if (this.txtTempRamp5.Text == "-") Cfg.TpCfg.bUseRamp[4] = false; else Cfg.TpCfg.bUseRamp[4] = true;
            if (this.txtTempRamp6.Text == "-") Cfg.TpCfg.bUseRamp[5] = false; else Cfg.TpCfg.bUseRamp[5] = true;
            if (this.txtTempRamp7.Text == "-") Cfg.TpCfg.bUseRamp[6] = false; else Cfg.TpCfg.bUseRamp[6] = true;
            if (this.txtTempRamp8.Text == "-") Cfg.TpCfg.bUseRamp[7] = false; else Cfg.TpCfg.bUseRamp[7] = true;
            if (this.txtTempRamp9.Text == "-") Cfg.TpCfg.bUseRamp[8] = false; else Cfg.TpCfg.bUseRamp[8] = true;

            if (this.txtTempUnif1.Text == "-") Cfg.TpCfg.bUseUnif[0] = false; else Cfg.TpCfg.bUseUnif[0] = true;
            if (this.txtTempUnif2.Text == "-") Cfg.TpCfg.bUseUnif[1] = false; else Cfg.TpCfg.bUseUnif[1] = true;
            if (this.txtTempUnif3.Text == "-") Cfg.TpCfg.bUseUnif[2] = false; else Cfg.TpCfg.bUseUnif[2] = true;
            if (this.txtTempUnif4.Text == "-") Cfg.TpCfg.bUseUnif[3] = false; else Cfg.TpCfg.bUseUnif[3] = true;
            if (this.txtTempUnif5.Text == "-") Cfg.TpCfg.bUseUnif[4] = false; else Cfg.TpCfg.bUseUnif[4] = true;
            if (this.txtTempUnif6.Text == "-") Cfg.TpCfg.bUseUnif[5] = false; else Cfg.TpCfg.bUseUnif[5] = true;
            if (this.txtTempUnif7.Text == "-") Cfg.TpCfg.bUseUnif[6] = false; else Cfg.TpCfg.bUseUnif[6] = true;
            if (this.txtTempUnif8.Text == "-") Cfg.TpCfg.bUseUnif[7] = false; else Cfg.TpCfg.bUseUnif[7] = true;
            if (this.txtTempUnif9.Text == "-") Cfg.TpCfg.bUseUnif[8] = false; else Cfg.TpCfg.bUseUnif[8] = true;

            if (this.txtTempTOver1.Text == "-") Cfg.TpCfg.bUseTOver[0] = false; else Cfg.TpCfg.bUseTOver[0] = true;
            if (this.txtTempTOver2.Text == "-") Cfg.TpCfg.bUseTOver[1] = false; else Cfg.TpCfg.bUseTOver[1] = true;
            if (this.txtTempTOver3.Text == "-") Cfg.TpCfg.bUseTOver[2] = false; else Cfg.TpCfg.bUseTOver[2] = true;
            if (this.txtTempTOver4.Text == "-") Cfg.TpCfg.bUseTOver[3] = false; else Cfg.TpCfg.bUseTOver[3] = true;
            if (this.txtTempTOver5.Text == "-") Cfg.TpCfg.bUseTOver[4] = false; else Cfg.TpCfg.bUseTOver[4] = true;
            if (this.txtTempTOver6.Text == "-") Cfg.TpCfg.bUseTOver[5] = false; else Cfg.TpCfg.bUseTOver[5] = true;
            if (this.txtTempTOver7.Text == "-") Cfg.TpCfg.bUseTOver[6] = false; else Cfg.TpCfg.bUseTOver[6] = true;
            if (this.txtTempTOver8.Text == "-") Cfg.TpCfg.bUseTOver[7] = false; else Cfg.TpCfg.bUseTOver[7] = true;
            if (this.txtTempTOver9.Text == "-") Cfg.TpCfg.bUseTOver[8] = false; else Cfg.TpCfg.bUseTOver[8] = true;

            if (this.txtTempUnifStblTm1.Text == "-") Cfg.TpCfg.bUseStableTm[0] = false; else Cfg.TpCfg.bUseStableTm[0] = true;
            if (this.txtTempUnifStblTm2.Text == "-") Cfg.TpCfg.bUseStableTm[1] = false; else Cfg.TpCfg.bUseStableTm[1] = true;
            if (this.txtTempUnifStblTm3.Text == "-") Cfg.TpCfg.bUseStableTm[2] = false; else Cfg.TpCfg.bUseStableTm[2] = true;
            if (this.txtTempUnifStblTm4.Text == "-") Cfg.TpCfg.bUseStableTm[3] = false; else Cfg.TpCfg.bUseStableTm[3] = true;
            if (this.txtTempUnifStblTm5.Text == "-") Cfg.TpCfg.bUseStableTm[4] = false; else Cfg.TpCfg.bUseStableTm[4] = true;
            if (this.txtTempUnifStblTm6.Text == "-") Cfg.TpCfg.bUseStableTm[5] = false; else Cfg.TpCfg.bUseStableTm[5] = true;
            if (this.txtTempUnifStblTm7.Text == "-") Cfg.TpCfg.bUseStableTm[6] = false; else Cfg.TpCfg.bUseStableTm[6] = true;
            if (this.txtTempUnifStblTm8.Text == "-") Cfg.TpCfg.bUseStableTm[7] = false; else Cfg.TpCfg.bUseStableTm[7] = true;
            if (this.txtTempUnifStblTm9.Text == "-") Cfg.TpCfg.bUseStableTm[8] = false; else Cfg.TpCfg.bUseStableTm[8] = true;

            Cfg.WarmUpSp = SysDefs.DotStringToVal(this.txtWarmUpSp.Text, 1);
            Cfg.WarmUpTm = Convert.ToInt32(this.txtWarmUpTm.Text);
            Cfg.DoWarmUp = chkWarmUp.Checked;
        }
    }
}

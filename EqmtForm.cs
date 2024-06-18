using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static Ptnr.TSpec;

namespace Ptnr
{

    public partial class EqmtForm : Form
    {
        public DateTime WorkStartTime { get; private set; }
        public bool bWorkingNow { get; private set; }
        
        public SerialComm Comm { get; }
        public event EventHandler UpdateConfig;
        public event EventHandler EqmtClose;

        public int idx = 0;
        public int EqmtIdx { get; }
        private Config Cfg;

        private bool[] _doFirstRunChamber = new bool[4] { false, false, false, false };

        private ChamberSts[] _chamber = new ChamberSts[]
        {
            new ChamberSts(),
            new ChamberSts(),
            new ChamberSts(),
            new ChamberSts()
        };

        private RecorderSts[] _recorder = new RecorderSts[]
        {
            new RecorderSts(),
            new RecorderSts(),
            new RecorderSts(),
            new RecorderSts()
        };

        private TSpecTpChamber[][] specTpChamber = new TSpecTpChamber[][]
        {
            new TSpecTpChamber[SysDefs.TEMP_TEST_CNT],
            new TSpecTpChamber[SysDefs.TEMP_TEST_CNT],
            new TSpecTpChamber[SysDefs.TEMP_TEST_CNT],
            new TSpecTpChamber[SysDefs.TEMP_TEST_CNT]
        };


        private int[] _chamberWorkingIdx = new int[4] {0,0,0,0};
        private List<int>[] _chamberTestList = new List<int>[]
        {
            new List<int>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        };

        private System.Windows.Forms.Timer[] _workChamberTmr = new System.Windows.Forms.Timer[]
        { 
            new System.Windows.Forms.Timer(),
            new System.Windows.Forms.Timer(),
            new System.Windows.Forms.Timer(),
            new System.Windows.Forms.Timer()
        };

        private bool[] _bWorkChamber = new bool[4] { false, false, false, false };
        private bool[] _bChamberHold = new bool[4] { false, false, false, false };
        private bool[] _bChamberStep = new bool[4] { false, false, false, false };

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        public void SetType()
        {
            this.lblTitle.Text = string.Format("항온\n  Chamber #{0}", EqmtIdx + 1);

            InitChamberTestSheetList();
            UpdateTestSheet();
        }

        public EqmtForm(int _idx, Config _cfg) 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            EqmtIdx = _idx;
            Cfg = _cfg;

            this.lblTitle.Text = string.Format("항온\n  Chamber #{0}", EqmtIdx + 1);
            
            bWorkingNow = false;

            for (int r = 0; r < 4; r++)
            {
                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    specTpChamber[r][i] = new TSpecTpChamber();
                    specTpChamber[r][i] = new TSpecTpChamber();
                }
                _chamberWorkingIdx[r] = 0;

                _workChamberTmr[r].Tick += new EventHandler(OnWorkChamberTmrEvent);
                _workChamberTmr[r].Interval = 1000;
            }

            //----------------------------------------------------------------//
            // Result report infomations
            //----------------------------------------------------------------//
            txtSerialNo.Text = _cfg.EqmtCfg[EqmtIdx].Serial;
            txtAmbTemp.Text = _cfg.EqmtCfg[EqmtIdx].AmbTemp;
            txtAmbHumi.Text = _cfg.EqmtCfg[EqmtIdx].AmbHumi;
            txtApprov.Text = _cfg.EqmtCfg[EqmtIdx].Approv;
            txtCoolTemp.Text = _cfg.EqmtCfg[EqmtIdx].CoolTemp;

            //--------------------------------------------------------------------------//
            // Chamber #1, #2 Comm. Status
            //--------------------------------------------------------------------------//
            lblChamb11Sts.Text = "OFFLINE";
            lblChamb12Sts.Text = "OFFLINE";
            lblRecorder11Sts.Text = "OFFLINE";

            lblChamb21Sts.Text = "OFFLINE";
            lblChamb22Sts.Text = "OFFLINE";
            lblRecorder21Sts.Text = "OFFLINE";

            UpdateTestSheet();
            InitChamberTestSheetList();

            Comm = new SerialComm();
            Comm.UpdateCtrlStatus += new UpdateCtrlStatusEventHandler(onUpdateCtrlSts);
            Comm.CommClose += new EventHandler(OnCommClosed);

            //----------------------------------------------------------------//
            // Set up clock.
            //----------------------------------------------------------------//
            System.Timers.Timer clock = new System.Timers.Timer(1000);
            clock.SynchronizingObject = this;

            clock.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            clock.AutoReset = true;
            clock.Enabled = true;
        }

        public void Dispose()
        {
            if (Comm.IsRun)
            {
                //------------------------------------------------------------------//
                // Write Temp Chamber #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHAMBER11, 104, 4);
                Comm.Write(SysDefs.ADDR_CHAMBER12, 104, 4);

                Comm.Write(SysDefs.ADDR_CHAMBER21, 104, 4);
                Comm.Write(SysDefs.ADDR_CHAMBER22, 104, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                Comm.CommStop();
            }
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            EqmtClose(this, EventArgs.Empty);
        }

        private void OnCommClosed(object src, EventArgs e)
        {
            _chamber[0].bOnLine = false;
            _chamber[1].bOnLine = false;
            _chamber[2].bOnLine = false;
            _chamber[3].bOnLine = false;

            _recorder[0].bOnLine = false;
            _recorder[1].bOnLine = false;
            _recorder[2].bOnLine = false;
            _recorder[3].bOnLine = false;
        }

        //--------------------------------------------------------------------------//
        // Current Date/Time
        //--------------------------------------------------------------------------//
        private void OnTimedEvent(Object src, ElapsedEventArgs e)
        {
            UpdateStatus();

            bWorkingNow = false;

            for (int i = 0; i < 4; i++)
            {
                if (_workChamberTmr[i].Enabled)
                {
                    _bWorkChamber[i] = true;
                    bWorkingNow = true;
                }
                else
                {
                    _bWorkChamber[i] = false;

                }
            }

            if (_bWorkChamber[0] == false)
            {
                btnStartChamber11.BackColor = Color.LightGray;
                btnStartChamber11.ForeColor = Color.Black;

                btnStartSelectedChamber11.BackColor = Color.LightGray;
                btnStartSelectedChamber11.ForeColor = Color.Black;

                btnStartChamber11.Enabled = true;
                btnStartSelectedChamber11.Enabled = true;
            }

            if (_bWorkChamber[1] == false)
            {
                btnStartChamber12.BackColor = Color.LightGray;
                btnStartChamber12.ForeColor = Color.Black;

                btnStartSelectedChamber12.BackColor = Color.LightGray;
                btnStartSelectedChamber12.ForeColor = Color.Black;

                btnStartChamber12.Enabled = true;
                btnStartSelectedChamber12.Enabled = true;
            }

            if (_bWorkChamber[2] == false)
            {
                btnStartChamber21.BackColor = Color.LightGray;
                btnStartChamber21.ForeColor = Color.Black;

                btnStartSelectedChamber21.BackColor = Color.LightGray;
                btnStartSelectedChamber21.ForeColor = Color.Black;

                btnStartChamber21.Enabled = true;
                btnStartSelectedChamber21.Enabled = true;
            }

            if (_bWorkChamber[3] == false)
            {
                btnStartChamber22.BackColor = Color.LightGray;
                btnStartChamber22.ForeColor = Color.Black;

                btnStartSelectedChamber22.BackColor = Color.LightGray;
                btnStartSelectedChamber22.ForeColor = Color.Black;

                btnStartChamber22.Enabled = true;
                btnStartSelectedChamber22.Enabled = true;
            }
        }

        private void UpdateStatus()
        {
            //--------------------------------------------------------------------------//
            // Chamber#11 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber[0].bOnLine)
            {
                lblChamb11Sts.Text = "ONLINE";
                lblChamb11Sts.BackColor = Color.Salmon;
                lblChamb11Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb11Sts.Text = "OFFLINE";
                lblChamb11Sts.BackColor = Color.DarkGray;
                lblChamb11Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Chamber#12 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber[1].bOnLine)
            {
                lblChamb12Sts.Text = "ONLINE";
                lblChamb12Sts.BackColor = Color.Salmon;
                lblChamb12Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb12Sts.Text = "OFFLINE";
                lblChamb12Sts.BackColor = Color.DarkGray;
                lblChamb12Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Recorder#11 Comm. Status
            //--------------------------------------------------------------------------//
            if (_recorder[0].bOnLine)
            {
                lblRecorder11Sts.Text = "ONLINE";
                lblRecorder11Sts.BackColor = Color.Salmon;
                lblRecorder11Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder11Sts.Text = "OFFLINE";
                lblRecorder11Sts.BackColor = Color.DarkGray;
                lblRecorder11Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Recorder#12 Comm. Status
            //--------------------------------------------------------------------------//
            if (_recorder[1].bOnLine)
            {
                lblRecorder12Sts.Text = "ONLINE";
                lblRecorder12Sts.BackColor = Color.Salmon;
                lblRecorder12Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder12Sts.Text = "OFFLINE";
                lblRecorder12Sts.BackColor = Color.DarkGray;
                lblRecorder12Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Chamber#21 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber[2].bOnLine)
            {
                lblChamb21Sts.Text = "ONLINE";
                lblChamb21Sts.BackColor = Color.Salmon;
                lblChamb21Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb21Sts.Text = "OFFLINE";
                lblChamb21Sts.BackColor = Color.DarkGray;
                lblChamb21Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Chamber#2 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber[3].bOnLine)
            {
                lblChamb22Sts.Text = "ONLINE";
                lblChamb22Sts.BackColor = Color.Salmon;
                lblChamb22Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb22Sts.Text = "OFFLINE";
                lblChamb22Sts.BackColor = Color.DarkGray;
                lblChamb22Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Recorder#21 Comm. Status
            //--------------------------------------------------------------------------//
            if (_recorder[2].bOnLine)
            {
                lblRecorder21Sts.Text = "ONLINE";
                lblRecorder21Sts.BackColor = Color.Salmon;
                lblRecorder21Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder21Sts.Text = "OFFLINE";
                lblRecorder21Sts.BackColor = Color.DarkGray;
                lblRecorder21Sts.ForeColor = Color.WhiteSmoke;
            }

            //--------------------------------------------------------------------------//
            // Recorder#22 Comm. Status
            //--------------------------------------------------------------------------//
            if (_recorder[3].bOnLine)
            {
                lblRecorder22Sts.Text = "ONLINE";
                lblRecorder22Sts.BackColor = Color.Salmon;
                lblRecorder22Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder22Sts.Text = "OFFLINE";
                lblRecorder22Sts.BackColor = Color.DarkGray;
                lblRecorder22Sts.ForeColor = Color.WhiteSmoke;
            }


            if (_chamber[0].bOnLine && _recorder[0].bOnLine) lblStsRoom1.BackColor = Color.Crimson;
            else lblStsRoom1.BackColor = Color.DarkGray;

            if (_chamber[1].bOnLine && _recorder[1].bOnLine) lblStsRoom2.BackColor = Color.Crimson;
            else lblStsRoom2.BackColor = Color.DarkGray;

            if (_chamber[2].bOnLine && _recorder[2].bOnLine) lblStsRoom3.BackColor = Color.Crimson;
            else lblStsRoom3.BackColor = Color.DarkGray;

            if (_chamber[3].bOnLine && _recorder[3].bOnLine) lblStsRoom4.BackColor = Color.Crimson;
            else lblStsRoom4.BackColor = Color.DarkGray;
        }

        private void UpdateChamberTestSheetList(ListViewNF lsv)
        {
            if (lsv.Items.Count == 0) return;

            int ch = 0;

            if      (lsv == lsvChmbSpc11) ch = 0;
            else if (lsv == lsvChmbSpc12) ch = 1;
            else if (lsv == lsvChmbSpc21) ch = 2;
            else if (lsv == lsvChmbSpc22) ch = 3;

            for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    TSpecTpChamber sp1 = new TSpecTpChamber();
                    sp1 = specTpChamber[ch][i];

                    string strTsp1 = SysDefs.DotString(sp1.tsp, 1) + "℃";
                    string strWTm1 = (sp1.waitTm == 0) ? "-" : sp1.waitTm.ToString() + "Min";
                    string strTTm1 = (sp1.testTm == 0) ? "-" : sp1.testTm.ToString() + "Min";

                    string strResTCtrMin1 = "-";
                    string strResTCtrMax1 = "-";

                    string strResTOver = "-";
                    string strResROver = "-";

                    string strResStableTm = "-";
                    string strResRamp1 = "-";

                    string strResUnifMin1 = "-";
                    string strResUnifMax1 = "-";
                    string strResult1 = "";

                if (sp1.result == WorkingRes.NotDef)
                {
                    if (sp1.workingSts == WorkingSts.Begin) strResult1 = "Ready..";
                    if (sp1.workingSts == WorkingSts.TouchSpCheck) strResult1 = "Checking PV";
                    if (sp1.workingSts == WorkingSts.Waiting) strResult1 = "Wating..";

                    if (sp1.workingSts == WorkingSts.Testing)
                    {
                        strResult1 = "Testing..";

                        if (_bChamberStep[ch] == true)
                        {
                            strResult1 = "Step";
                        }

                        if (_bChamberHold[ch] == true)
                        {
                            strResult1 = "Holding..";
                        }
                    }
                }

                else
                {
                    if (sp1.result == WorkingRes.Success) strResult1 = "GOOD";
                    if (sp1.result == WorkingRes.Fail) strResult1 = "NG";

                    if (sp1.bReport == false)
                    {
                        strResult1 = "NOT TEST";
                    }
                }

                    strResTCtrMin1 = (sp1.bUseTDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrTMin, 1);
                    strResTCtrMax1 = (sp1.bUseTDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrTMax, 1);
                    
                    strResTOver = (sp1.bUseTOver == false) ? "-" : SysDefs.DotString(sp1.resTOver, 1);
                    strResROver = SysDefs.DotString(sp1.resROver, 1);
                    strResStableTm = sp1.resStableTm.ToString();

                    strResRamp1 = (sp1.bUseRamp == false) ? "-" : SysDefs.DotString(sp1.resCtrRamp, 1);

                    strResUnifMin1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMin, 1);
                    strResUnifMax1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMax, 1);

                    if (sp1.resCtrTMin == SysDefs.NOT_DEFVAL) strResTCtrMin1 = "-";
                    if (sp1.resCtrTMax == SysDefs.NOT_DEFVAL) strResTCtrMax1 = "-";

                    if (sp1.resTOver == SysDefs.NOT_DEFVAL) strResTOver = "-";
                    if (sp1.resROver == SysDefs.NOT_DEFVAL) strResROver = "-";

                    if (sp1.resStableTm == SysDefs.NOT_DEFVAL) strResStableTm = "-";

                    if (sp1.resCtrRamp == SysDefs.NOT_DEFVAL) strResRamp1 = "-";

                    if (sp1.resUnifMin == SysDefs.NOT_DEFVAL) strResUnifMin1 = "-";
                    if (sp1.resUnifMax == SysDefs.NOT_DEFVAL) strResUnifMax1 = "-";

                    lsv.Items[i].SubItems[0].Text = String.Format("{0}", i + 1);
                    lsv.Items[i].SubItems[1].Text = strTsp1;
                    lsv.Items[i].SubItems[2].Text = strWTm1;
                    lsv.Items[i].SubItems[3].Text = strTTm1;

                    lsv.Items[i].SubItems[4].Text = strResTCtrMin1;
                    lsv.Items[i].SubItems[5].Text = strResTCtrMax1;

                    lsv.Items[i].SubItems[6].Text = strResTOver;
                    lsv.Items[i].SubItems[7].Text = strResROver;

                    lsv.Items[i].SubItems[8].Text = strResStableTm;

                    lsv.Items[i].SubItems[9].Text = strResRamp1;
                    lsv.Items[i].SubItems[10].Text = strResUnifMin1;
                    lsv.Items[i].SubItems[11].Text = strResUnifMax1;

                    lsv.Items[i].SubItems[12].Text = strResult1;
                }
        }

        //-----------------------------------------------------------------//
        // Start Chamber Test
        //-----------------------------------------------------------------//
        private void btnStartChamber_Click(object sender, EventArgs e)
        {
            int ch = 0;
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;

            if (sender == this.btnStartChamber11) ch = 0;
            else if (sender == this.btnStartChamber12) ch = 1;
            else if (sender == this.btnStartChamber21) ch = 2;
            else if (sender == this.btnStartChamber22) ch = 3;
            else
            {
                return;
            }

            if (_bWorkChamber[ch] == true)
            {
                if (MessageBox.Show("챔버 일괄 테스트를 중지 하시겠습니까?",
                "테스트 정지?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                _workChamberTmr[ch].Stop();
                _chamberTestList[ch].Clear();

                //------------------------------------------------------------------//
                // Write Chamber #1 Stop
                //------------------------------------------------------------------//
                if (ch == 0) Comm.Write(SysDefs.ADDR_CHAMBER11, 102, 4);
                if (ch == 1) Comm.Write(SysDefs.ADDR_CHAMBER12, 102, 4);
                if (ch == 2) Comm.Write(SysDefs.ADDR_CHAMBER21, 102, 4);
                if (ch == 3) Comm.Write(SysDefs.ADDR_CHAMBER22, 102, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                if (_bWorkChamber[0] == false && _bWorkChamber[1] == false)
                {
                    Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);
                }

                if (_bWorkChamber[2] == false && _bWorkChamber[3] == false)
                {
                    Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);
                }
            }

            else
            {
                if (MessageBox.Show("챔버 일괄 테스트를 실행 하시겠습니까?",
                "테스트 실행?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btn.BackColor = Color.Salmon;
                btn.ForeColor = Color.WhiteSmoke;

                if      (ch == 0) btnStartSelectedChamber11.Enabled = false;
                else if (ch == 1) btnStartSelectedChamber12.Enabled = false;
                else if (ch == 2) btnStartSelectedChamber21.Enabled = false;
                else if (ch == 3) btnStartSelectedChamber22.Enabled = false;

                _doFirstRunChamber[ch] = false;
                _chamberWorkingIdx[ch] = 0;
                _chamberTestList[ch].Clear();

                for (int i = 0; i < specTpChamber[ch].Length; i++)
                {
                    specTpChamber[ch][i].Reset();
                    _chamberTestList[ch].Add(i);
                }
                _workChamberTmr[ch].Start();
            }
        }

        //--------------------------------------------------------------------------//
        // TESTING CHAMBER #1
        //--------------------------------------------------------------------------//
        private void OnWorkChamberTmrEvent(Object src, EventArgs e)
        {
            System.Windows.Forms.Timer _src = (System.Windows.Forms.Timer)src;
            int ch = 0;
            
            if      (_src == _workChamberTmr[0]) ch = 0;
            else if (_src == _workChamberTmr[1]) ch = 1;
            else if (_src == _workChamberTmr[2]) ch = 2;
            else if (_src == _workChamberTmr[3]) ch = 3;
            else
            {
                return;
            }

            TSpecTpChamber spcChamber = specTpChamber[ch][_chamberWorkingIdx[ch]];
            if (spcChamber.result == WorkingRes.NotDef)
            {
                DoTpChamberTest(ch, spcChamber);
            }

            else
            {
                if (_chamberTestList[ch].Count > 0)
                {
                    _chamberWorkingIdx[ch] = _chamberTestList[ch].ElementAt(0);
                    _chamberTestList[ch].RemoveAt(0);
                }
                else
                {
                    _workChamberTmr[ch].Stop();
                    _bWorkChamber[ch] = false;

                    //------------------------------------------------------------------//
                    // All Chamber Test are Finished.
                    // Write Chamber Stop
                    //------------------------------------------------------------------//
                    if (ch == 0) Comm.Write(SysDefs.ADDR_CHAMBER11, 102, 4);
                    if (ch == 1) Comm.Write(SysDefs.ADDR_CHAMBER12, 102, 4);
                    if (ch == 2) Comm.Write(SysDefs.ADDR_CHAMBER21, 102, 4);
                    if (ch == 3) Comm.Write(SysDefs.ADDR_CHAMBER22, 102, 4);

                    //------------------------------------------------------------------//
                    // Write Recorder #1 Record Stop
                    //------------------------------------------------------------------//
                    if (_bWorkChamber[0] == false && _bWorkChamber[1] == false)
                    {
                        Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);
                    }

                    if (_bWorkChamber[2] == false && _bWorkChamber[3] == false)
                    {
                        Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);
                    }
                }
            }
        }

        // Selected Cell Test Start/Stop
        private void btnStartChamberTest_Click(object sender, EventArgs e)
        {
            int ch = 0;
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;

            if      (sender == this.btnStartSelectedChamber11) ch = 0;
            else if (sender == this.btnStartSelectedChamber12) ch = 1;
            else if (sender == this.btnStartSelectedChamber21) ch = 2;
            else if (sender == this.btnStartSelectedChamber22) ch = 3;
            else
            {
                return;
            }

            if (_bWorkChamber[ch])
            {
                if (MessageBox.Show("챔버 테스트를 중지 하시겠습니까?",
                                    "장비 정지?",
                                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                _workChamberTmr[ch].Stop();

                //------------------------------------------------------------------//
                // Write Chamber #1 Stop
                //------------------------------------------------------------------//
                if (ch == 0) Comm.Write(SysDefs.ADDR_CHAMBER11, 102, 4);
                if (ch == 1) Comm.Write(SysDefs.ADDR_CHAMBER12, 102, 4);
                if (ch == 2) Comm.Write(SysDefs.ADDR_CHAMBER21, 102, 4);
                if (ch == 3) Comm.Write(SysDefs.ADDR_CHAMBER22, 102, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                if (_bWorkChamber[0] == false && _bWorkChamber[1] == false)
                {
                    Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);
                }

                if (_bWorkChamber[2] == false && _bWorkChamber[3] == false)
                {
                    Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);
                }
            }

            else
            {
                if (MessageBox.Show("챔버 테스트를 시작 하시겠습니까?",
                    "테스트 시작?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btn.BackColor = Color.Salmon;
                btn.ForeColor = Color.WhiteSmoke;

                if (ch == 0) btnStartChamber11.Enabled = false;
                else if (ch == 1) btnStartChamber12.Enabled = false;
                else if (ch == 2) btnStartChamber21.Enabled = false;
                else if (ch == 3) btnStartChamber22.Enabled = false;

                _doFirstRunChamber[ch] = false;
                _workChamberTmr[ch].Start();

                btn.BackColor = Color.Salmon;
                btn.ForeColor = Color.WhiteSmoke;

                _doFirstRunChamber[ch] = false;
                
                int wrkIdx = _chamberWorkingIdx[ch];
                specTpChamber[ch][wrkIdx].Reset();
                
                _chamberTestList[ch].Clear();
                _chamberTestList[ch].Add(wrkIdx);

                _workChamberTmr[ch].Start();
            }
        }

        private void DoTpChamberTest(int ch, TSpecTpChamber spcChamber)
        {
            if (spcChamber.workingSts == WorkingSts.Begin)
            {
                PerformTpChamberTestBegin(ch, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.TouchSpCheck)
            {
                PerformTpChamberTestTouch(ch, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Waiting)
            {
                PerformTpChamberTestWait(ch, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Testing)
            {
                PerformTpChamberDoTest(ch, spcChamber);
            }

            //------------------------------------------------------------------//
            // Test End.
            //------------------------------------------------------------------//
            if (spcChamber.workingSts == WorkingSts.End)
            {
                spcChamber.result = WorkingRes.Success;
                //------------------------------------------------------------------//
                // Success check : Disparity
                //------------------------------------------------------------------//
                if (spcChamber.bUseTDisp)
                {
                    if (spcChamber.resCtrTMax > (spcChamber.tsp + spcChamber.jugTDisp) ||
                        spcChamber.resCtrTMin < (spcChamber.tsp - spcChamber.jugTDisp))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                //------------------------------------------------------------------//
                // Success check : Ramp 
                //------------------------------------------------------------------//
                if (spcChamber.bUseRamp)
                {
                    if (spcChamber.resCtrRamp < spcChamber.jugRamp)
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                //------------------------------------------------------------------//
                // Success check : Uniformity
                //------------------------------------------------------------------//
                if (spcChamber.bUseUnif)
                {
                    if (spcChamber.resUnifMax > (spcChamber.tsp + spcChamber.jugUnif))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }

                    if (spcChamber.resUnifMin < (spcChamber.tsp - spcChamber.jugUnif))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                //------------------------------------------------------------------//
                // Success check : Temp Over
                //------------------------------------------------------------------//
                if (spcChamber.bUseTOver)
                {
                    if (spcChamber.resTOver > (spcChamber.tsp + spcChamber.jugTOver))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                //------------------------------------------------------------------//
                // Success check : Temp Control stable time
                //------------------------------------------------------------------//
                if (spcChamber.bUseStableTm)
                {
                    if (spcChamber.resStableTm > (short)(spcChamber.jugStableTm))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                spcChamber.workEndTm = DateTime.Now;
                GenReport();
            }

            if (ch == 0) UpdateChamberTestSheetList(lsvChmbSpc11);
            if (ch == 1) UpdateChamberTestSheetList(lsvChmbSpc12);
            if (ch == 2) UpdateChamberTestSheetList(lsvChmbSpc21);
            if (ch == 3) UpdateChamberTestSheetList(lsvChmbSpc22);
        }

        //--------------------------------------------------------------------------//
        // 1. TEMP Chamber Test Begin
        //--------------------------------------------------------------------------//
        private void PerformTpChamberTestBegin(int ch, TSpecTpChamber spc)
        {
            int addr = 0;
            if (ch == 0) addr = SysDefs.ADDR_CHAMBER11;
            if (ch == 1) addr = SysDefs.ADDR_CHAMBER12;
            if (ch == 2) addr = SysDefs.ADDR_CHAMBER21;
            if (ch == 3) addr = SysDefs.ADDR_CHAMBER22;

            short tpv = _chamber[ch].tpv;

            if (tpv < spc.tsp) spc.tempSlop = Slop.Up;
            else               spc.tempSlop = Slop.Dn;
               
            // Write Target Temp
            Comm.Write(addr, 104, spc.tsp);

            if (_doFirstRunChamber[ch] == false)
            {
                // Write Chamber RUN
                if ((_chamber[ch].sts & 0x0001) > 0)
                {
                    Comm.Write(addr, 102, 1);
                    _doFirstRunChamber[ch] = true;
                }
            }

            //------------------------------------------------------------------//
            // Write Recorder #1 Record Start
            //------------------------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER11 || addr == SysDefs.ADDR_CHAMBER12)
            {
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 1);
            }

            if (addr == SysDefs.ADDR_CHAMBER21 || addr == SysDefs.ADDR_CHAMBER22)
            {
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 1);
            }

            spc.startPv = tpv;
            spc.workingSts = WorkingSts.TouchSpCheck;
            spc.workStartTm = DateTime.Now;

            spc.bTouchTemp = false;

            spc.resCtrTMin = SysDefs.NOT_DEFVAL;
            spc.resCtrTMax = SysDefs.NOT_DEFVAL;

            spc.resTOver = SysDefs.NOT_DEFVAL;
            spc.resROver = SysDefs.NOT_DEFVAL;

            spc.resStableTm = SysDefs.NOT_DEFVAL;
            spc.resCtrRamp = SysDefs.NOT_DEFVAL;

            spc.resUnifMin = SysDefs.NOT_DEFVAL;
            spc.resUnifMax = SysDefs.NOT_DEFVAL;

            spc.resRec[SysDefs.CH1].Clear();
            spc.resRec[SysDefs.CH2].Clear();
            spc.resRec[SysDefs.CH3].Clear();
            spc.resRec[SysDefs.CH4].Clear();
        }

        //--------------------------------------------------------------------------//
        // 2. TEMP Chamber : Touch Target SP Check..
        //--------------------------------------------------------------------------//
        private void PerformTpChamberTestTouch(int ch, TSpecTpChamber spc)
        {
            short tpv = _chamber[ch].tpv;

            //--------------------------------------------------------//
            // Temp touch check.
            //--------------------------------------------------------//
            if (spc.bTouchTemp == false)
            {
                if (spc.tempSlop == Slop.Up)
                {
                    if (tpv >= spc.tsp) spc.bTouchTemp = true;
                }
                else
                {
                    if (tpv <= spc.tsp) spc.bTouchTemp = true;
                }
            }

            //--------------------------------------------------------//
            // Temp touched to SP
            //--------------------------------------------------------//
            if (spc.bTouchTemp == true)
            {
                TimeSpan span = DateTime.Now.Subtract(spc.workStartTm);
                spc.resCtrRamp = (short)(Math.Abs(spc.tsp - spc.startPv) / (span.TotalMinutes + 1));

                Console.WriteLine(">> Touch PV : "+span.TotalMinutes.ToString() +" / "+ (Math.Abs(spc.tsp - spc.startPv)).ToString());

                spc.workingSts = WorkingSts.Waiting;
                spc.workStartTm = DateTime.Now;
                spc.pvPassingTm = DateTime.MinValue;

                return;
            }

            // Wait End check.
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= 60)
            {
                spc.workingSts = WorkingSts.End;
            }
        }

        //--------------------------------------------------------------------------//
        // 3. TEMP Chamber : Test Wating..
        //--------------------------------------------------------------------------//
        private void PerformTpChamberTestWait(int ch, TSpecTpChamber spc)
        {
            short tpv = _chamber[ch].tpv;

            if (spc.resTOver == SysDefs.NOT_DEFVAL)
            {
                spc.resTOver = tpv;
            }

            if (spc.tempSlop == Slop.Up)
            {
                // To check control stable time
                if (tpv > spc.tsp && spc.pvPassingTm == DateTime.MinValue)
                {
                    spc.pvPassingTm = DateTime.Now;
                }

                if (tpv >= spc.resTOver) spc.resTOver = tpv;

                for (int ofs = 0; ofs < SysDefs.MAX_REC_CHCNT; ofs++)
                {
                    short val = _recorder[ch].ch[ofs];
                    if (spc.resROver == SysDefs.NOT_DEFVAL)
                    {
                        spc.resROver = val;
                    }
                    if (val >= spc.resROver) spc.resROver = val;
                }
            }
            else
            {
                // To check control stable time
                if (tpv < spc.tsp && spc.pvPassingTm == DateTime.MinValue)
                {
                    spc.pvPassingTm = DateTime.Now;
                }

                if (tpv <= spc.resTOver) spc.resTOver = tpv;

                for (int ofs = 0; ofs < SysDefs.MAX_REC_CHCNT; ofs++)
                {
                    short val = _recorder[ch].ch[ofs];
                    if (spc.resROver == SysDefs.NOT_DEFVAL)
                    {
                        spc.resROver = val;
                    }
                    if (val <= spc.resROver) spc.resROver = val;
                }
            }

            //--------------------------------------------------------------------------//
            // Get Stable time.
            //--------------------------------------------------------------------------//
            if (spc.resStableTm == SysDefs.NOT_DEFVAL)
            {
                int hlmt = (spc.tsp + spc.jugUnif);
                int llmt = (spc.tsp - spc.jugUnif);

                for (int ofs = 0; ofs < SysDefs.MAX_REC_CHCNT; ofs++)
                {
                    short val = _recorder[ch].ch[ofs];
                    if (val > hlmt || val < llmt)
                    {
                        if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.waitTm)
                        {
                            spc.resStableTm = (short)(spc.waitTm);
                            spc.workingSts = WorkingSts.Testing;
                            spc.workStartTm = DateTime.Now;
                        }
                        return;
                    }
                }
                spc.resStableTm = (short)(DateTime.Now.Subtract(spc.workStartTm).TotalMinutes + 1);
            }

            spc.workingSts = WorkingSts.Testing;
            spc.workStartTm = DateTime.Now;
        }

        //--------------------------------------------------------------------------//
        // 4. TEMP Chamber : Testing...
        //--------------------------------------------------------------------------//
        private void PerformTpChamberDoTest(int ch, TSpecTpChamber spc)
        {
            //------------------------------------------------//
            // Chamber Holding..
            //------------------------------------------------//
            if (_bChamberHold[ch] == true)
            {
                return;
            }

            int testTm = spc.testTm;
            int waitTm = 10;

            if (_bChamberStep[ch] == true)
            {
                testTm = 10;
                waitTm = 0;
            }

            short tpv = _chamber[ch].tpv;

            // 테스트 시작후 10분 후 부터 테스트 종료시간 까지 측정 (6.13)
            if ((DateTime.Now.Subtract(spc.workStartTm).TotalMinutes) >= waitTm)
            {
                if (spc.resCtrTMax == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrTMax = tpv;
                }

                if (spc.resCtrTMin == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrTMin = tpv;
                }

                if (tpv >= spc.resCtrTMax) spc.resCtrTMax = tpv;
                if (tpv <= spc.resCtrTMin) spc.resCtrTMin = tpv;
            }

            //--------------------------------------------------------------------------//
            // Record pv
            //--------------------------------------------------------------------------//
            for (int ofs = 0; ofs < SysDefs.MAX_REC_CHCNT; ofs++)
            {
                short val = _recorder[ch].ch[ofs];
                spc.AddRecData(ofs, val);
            }

            //--------------------------------------------------------------------------//
            // Test End check.
            // Get Uniformity MIN/MAX from record list.
            //--------------------------------------------------------------------------//
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= testTm)
            {
                short[] avg = new short[SysDefs.MAX_REC_CHCNT];

                for (int ofs = 0; ofs < SysDefs.MAX_REC_CHCNT; ofs++)
                {
                    avg[ofs] = 0;
                    int sum = 0;

                    Console.WriteLine(">> Recorder count: "+ spc.resRec[ofs].Count);

                    for (int i = 0; i < spc.resRec[ofs].Count; i++)
                    {
                        sum += spc.resRec[ofs][i];
                    }
                    avg[ofs] = (short)(sum / spc.resRec[ofs].Count);

                    if (spc.resUnifMin == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMin = avg[ofs];
                    }

                    if (spc.resUnifMax == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMax = avg[ofs];
                    }

                    if (avg[ofs] <= spc.resUnifMin) spc.resUnifMin = avg[ofs];
                    if (avg[ofs] >= spc.resUnifMax) spc.resUnifMax = avg[ofs];
                }
                spc.workingSts = WorkingSts.End;

                if (_bChamberStep[ch] == true)
                {
                    _bChamberStep[ch] = false;

                    System.Windows.Forms.Button btn = btnChamber11Step;
                    if      (ch == 0) btn = btnChamber11Step;
                    else if (ch == 1) btn = btnChamber12Step;
                    else if (ch == 2) btn = btnChamber21Step;
                    else if (ch == 3) btn = btnChamber22Step;

                    btn.BackColor = Color.WhiteSmoke;
                    btn.ForeColor = Color.Black;
                }
            }
        }
        
        public void onUpdateCtrlSts(int addr, string strSts)
        {
            int ch = 0;

            if (addr == SysDefs.ADDR_CHAMBER11) ch = 0;
            if (addr == SysDefs.ADDR_CHAMBER12) ch = 1;
            if (addr == SysDefs.ADDR_CHAMBER21) ch = 2;
            if (addr == SysDefs.ADDR_CHAMBER22) ch = 3;

            //--------------------------------------------------------------------------//
            // Check OnLine status.
            //--------------------------------------------------------------------------//
            if (strSts.IndexOf("OK") < 0)
            {
                if (addr == SysDefs.ADDR_CHAMBER11) _chamber[0].bOnLine = false;
                if (addr == SysDefs.ADDR_CHAMBER12) _chamber[1].bOnLine = false;

                if (addr == SysDefs.ADDR_CHAMBER21) _chamber[2].bOnLine = false;
                if (addr == SysDefs.ADDR_CHAMBER22) _chamber[3].bOnLine = false;

                if (addr == SysDefs.ADDR_RECORDER1)
                {
                    _recorder[0].bOnLine = false;
                    _recorder[1].bOnLine = false;
                }
                if (addr == SysDefs.ADDR_RECORDER2)
                {
                    _recorder[2].bOnLine = false;
                    _recorder[3].bOnLine = false;
                }
            }
            else
            {
                if (addr == SysDefs.ADDR_CHAMBER11) _chamber[0].bOnLine = true;
                if (addr == SysDefs.ADDR_CHAMBER12) _chamber[1].bOnLine = true;

                if (addr == SysDefs.ADDR_CHAMBER21) _chamber[2].bOnLine = true;
                if (addr == SysDefs.ADDR_CHAMBER22) _chamber[3].bOnLine = true;

                if (addr == SysDefs.ADDR_RECORDER1)
                {
                    _recorder[0].bOnLine = true;
                    _recorder[1].bOnLine = true;
                }
                if (addr == SysDefs.ADDR_RECORDER2)
                {
                    _recorder[2].bOnLine = true;
                    _recorder[3].bOnLine = true;
                }
            }

            if (strSts.IndexOf("OK") < 0)
            {
                return;
            }

            if ((strSts.IndexOf("RRD") < 0) && (strSts.IndexOf("RSD") < 0))
            {
                return;
            }

            if (strSts.Length < 5)
            {
                return;
            }

            strSts = strSts.Substring(0, strSts.Length - 4);
            string[] tmp = strSts.Split(',');

            //--------------------------------------------------------------------------//
            // Read Chamber#1 Status
            // - TEMI(TPV:1, TSP:2, HPV:5, HSP:6, NOWSTS:10)
            // - TEMP(PV:1, SP:3, NOWSTS:10)
            //--------------------------------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER11 || addr == SysDefs.ADDR_CHAMBER12||
                addr == SysDefs.ADDR_CHAMBER21 || addr == SysDefs.ADDR_CHAMBER22)
            {
                _chamber[ch].tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                _chamber[ch].tsp = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                _chamber[ch].sts = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
            }

            //--------------------------------------------------------------------------//
            // Read Recorder#1 Status (NPV1~NPV9)
            //--------------------------------------------------------------------------//
            else if (addr == SysDefs.ADDR_RECORDER1 || addr == SysDefs.ADDR_RECORDER2)
            {
                if (addr == SysDefs.ADDR_RECORDER1)
                {
                    if (tmp.Length < 11)
                    {
                        _recorder[0].bOnLine = false;
                        _recorder[1].bOnLine = false;
                        return;
                    }

                    _recorder[0].ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _recorder[0].ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _recorder[0].ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _recorder[0].ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));

                    _recorder[1].ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                    _recorder[1].ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    _recorder[1].ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[8], System.Globalization.NumberStyles.HexNumber));
                    _recorder[1].ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[9], System.Globalization.NumberStyles.HexNumber));
                }

                else if (addr == SysDefs.ADDR_RECORDER2)
                {
                    if (tmp.Length < 11)
                    {
                        _recorder[2].bOnLine = false;
                        _recorder[3].bOnLine = false;
                        return;
                    }

                    _recorder[2].ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _recorder[2].ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _recorder[2].ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _recorder[2].ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));

                    _recorder[3].ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                    _recorder[3].ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    _recorder[3].ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[8], System.Globalization.NumberStyles.HexNumber));
                    _recorder[3].ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[9], System.Globalization.NumberStyles.HexNumber));
                }
            }

            //--------------------------------------------------------------------------//
            // Update List controls Chamber1
            //--------------------------------------------------------------------------//
            lblChamber11TPv.Text = "온도:" + SysDefs.DotString(_chamber[0].tpv, 1) + " ℃";

            lblRec11Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder[0].ch[0], 1) + " ℃";
            lblRec11Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder[0].ch[1], 1) + " ℃";
            lblRec11Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder[0].ch[2], 1) + " ℃";
            lblRec11Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder[0].ch[3], 1) + " ℃";

            //--------------------------------------------------------------------------//
            // Update List controls Chamber2
            //--------------------------------------------------------------------------//
            lblChamber12TPv.Text = "온도:" + SysDefs.DotString(_chamber[1].tpv, 1) + " ℃";

            lblRec12Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder[1].ch[0], 1) + " ℃";
            lblRec12Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder[1].ch[1], 1) + " ℃";
            lblRec12Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder[1].ch[2], 1) + " ℃";
            lblRec12Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder[1].ch[3], 1) + " ℃";

            //--------------------------------------------------------------------------//
            // Update List controls Chamber3
            //--------------------------------------------------------------------------//
            lblChamber21TPv.Text = "온도:" + SysDefs.DotString(_chamber[2].tpv, 1) + " ℃";

            lblRec21Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder[2].ch[0], 1) + " ℃";
            lblRec21Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder[2].ch[1], 1) + " ℃";
            lblRec21Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder[2].ch[2], 1) + " ℃";
            lblRec21Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder[2].ch[3], 1) + " ℃";

            //--------------------------------------------------------------------------//
            // Update List controls Chamber4
            //--------------------------------------------------------------------------//
            lblChamber22TPv.Text = "온도:" + SysDefs.DotString(_chamber[3].tpv, 1) + " ℃";
 
            lblRec22Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder[3].ch[0], 1) + " ℃";
            lblRec22Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder[3].ch[1], 1) + " ℃";
            lblRec22Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder[3].ch[2], 1) + " ℃";
            lblRec22Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder[3].ch[3], 1) + " ℃";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Comm.IsRun)
            {
                Comm.CommStop();

                btnConnect.Text = "CONNECT";
                btnConnect.BackColor = Color.LightGray;
                btnConnect.ForeColor = Color.Black;
            }
            else
            {
                if (Comm.CommStart(Cfg.CommCfg, Cfg.EqmtCfg[EqmtIdx].Port))
                {
                    btnConnect.Text = "DISCONNECT";
                    btnConnect.BackColor = Color.Salmon;
                    btnConnect.ForeColor = Color.WhiteSmoke;
                }
            }
        }

        private void WriteTpChamberResExcel(Worksheet ws, TSpecTpChamber spc, int srow)
        {
            Microsoft.Office.Interop.Excel.Range rngChambSp = ws.Rows.Cells[srow, 2];

            Microsoft.Office.Interop.Excel.Range rngChambTMin = ws.Rows.Cells[srow, 8];
            Microsoft.Office.Interop.Excel.Range rngChambTMax = ws.Rows.Cells[srow, 11];
            Microsoft.Office.Interop.Excel.Range rngChambTOvr = ws.Rows.Cells[srow, 14];

            Microsoft.Office.Interop.Excel.Range rngChambHMin = ws.Rows.Cells[srow, 17];
            Microsoft.Office.Interop.Excel.Range rngChambHMax = ws.Rows.Cells[srow, 20];
            Microsoft.Office.Interop.Excel.Range rngChambHOvr = ws.Rows.Cells[srow, 23];

            Microsoft.Office.Interop.Excel.Range rngChambStableTm = ws.Rows.Cells[srow, 26];

            Microsoft.Office.Interop.Excel.Range rngChambRamp = ws.Rows.Cells[srow, 30];

            Microsoft.Office.Interop.Excel.Range rngChambRecMin = ws.Rows.Cells[srow, 34];
            Microsoft.Office.Interop.Excel.Range rngChambRecMax = ws.Rows.Cells[srow, 38];
            Microsoft.Office.Interop.Excel.Range rngChambRes = ws.Rows.Cells[srow, 42];

            string strSP = "-";
            string strResCtrTMin = "-";
            string strResCtrTMax = "-";
            string strResCtrTOvr = "-";

            string strResCtrHMin = "-";
            string strResCtrHMax = "-";
            string strResCtrHOvr = "-";

            string strResSettlingTm = "-";
            string strResCtrRamp = "-";
            string strResUnifMin = "-";
            string strResUnifMax = "-";

            // Control Min/Max
            strSP = SysDefs.DotString(spc.tsp, 1) + "℃";
            
            if (spc.result != WorkingRes.NotDef)
            {
                if (spc.bUseTOver == true) strResCtrTOvr = SysDefs.DotString(spc.resTOver, 1);
            
                // Control Min/Max
                if (spc.bUseTDisp == true)
                {
                    strResCtrTMin = SysDefs.DotString(spc.resCtrTMin, 1);
                    strResCtrTMax = SysDefs.DotString(spc.resCtrTMax, 1);
                }

                // Control Ramp
                if (spc.bUseRamp)
                {
                    strResCtrRamp = SysDefs.DotString(spc.resCtrRamp, 1);
                }

                // Control Uniformity
                if (spc.bUseUnif)
                {
                    strResUnifMin = SysDefs.DotString(spc.resUnifMin, 1);
                    strResUnifMax = SysDefs.DotString(spc.resUnifMax, 1);

                    strResSettlingTm = spc.resStableTm.ToString();
                }
            }

            //------------------------------------------------------------//
            // Write WorkSheet
            //------------------------------------------------------------//
            rngChambSp.Value = strSP;
            rngChambTMin.Value = strResCtrTMin;
            rngChambTMax.Value = strResCtrTMax;
            rngChambTOvr.Value = strResCtrTOvr;

            rngChambHMin.Value = strResCtrHMin;
            rngChambHMax.Value = strResCtrHMax;
            rngChambHOvr.Value = strResCtrHOvr;

            rngChambStableTm.Value = strResSettlingTm;
            rngChambRamp.Value = strResCtrRamp;

            rngChambRecMin.Value = strResUnifMin;
            rngChambRecMax.Value = strResUnifMax;

            if (spc.result == WorkingRes.Success)
            {
                rngChambRes.Value = "GOOD";
            }
            else if (spc.result == WorkingRes.Fail)
            {
                rngChambRes.Value = "NG";
            }
            else
            {
                rngChambRes.Value = "-";
            }
        }

        private void GenReport()
        {
            string templeteFile = "";
            templeteFile = SysDefs.execPath + "\\" + SysDefs.RESULT_FOLDER_PATH + "\\Report_Tp.xlsx";
            
            string tarPath = SysDefs.execPath + SysDefs.RESULT_FOLDER_PATH + "\\" + txtSerialNo.Text + ".xlsx";

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(templeteFile, ReadOnly: false, Editable: true);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets.Item[1] as Microsoft.Office.Interop.Excel.Worksheet;

            if (worksheet == null)
            {
                return;
            }

            for(int ch = 0; ch<4; ch++)
            {
                int sRow = 10;
                int idx = 0;

                if (ch == 0) sRow = 10;
                else if (ch == 1) sRow = 20;
                else if (ch == 2) sRow = 30;
                else if (ch == 3) sRow = 40;

                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    TSpecTpChamber spc = specTpChamber[ch][i];
                    WriteTpChamberResExcel(worksheet, spc, sRow + idx);
                    idx++;
                }
            }

            Microsoft.Office.Interop.Excel.Range rngSerial = worksheet.Rows.Cells[5, 33];
            Microsoft.Office.Interop.Excel.Range rngAmbTemp = worksheet.Rows.Cells[49, 13];
            Microsoft.Office.Interop.Excel.Range rngAmbHumi = worksheet.Rows.Cells[49, 17];
            Microsoft.Office.Interop.Excel.Range rngCoolant = worksheet.Rows.Cells[50, 13];
            Microsoft.Office.Interop.Excel.Range rngApprov = worksheet.Rows.Cells[53, 13];
            Microsoft.Office.Interop.Excel.Range rngDate = worksheet.Rows.Cells[52, 13];

            rngSerial.Value = txtSerialNo.Text;
            rngAmbTemp.Value = txtAmbTemp.Text;
            rngAmbHumi.Value = txtAmbHumi.Text;
            rngCoolant.Value = txtCoolTemp.Text;
            rngApprov.Value = txtApprov.Text;
            rngDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                excel.Application.ActiveWorkbook.SaveAs(tarPath, 
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, 
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close();

                excel.Application.Quit();
                excel.Quit();
            }
            catch
            {
                object misValue = System.Reflection.Missing.Value;
                workbook.Close(false, misValue, misValue);

                excel.Application.Quit();
                excel.Quit();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 결과 내용으로 결과 레포트를 생성 하시겠습니까?",
                               "Export to Excel file?",
                                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                GenReport();
            }
        }

    
        private void InitChamberTestSheetList()
        {
            ListViewNF lsvChamber;
            
            for (int idx = 0; idx < 4; idx++){
                lsvChamber = lsvChmbSpc11;
                if (idx == 1) lsvChamber = lsvChmbSpc12;
                if (idx == 2) lsvChamber = lsvChmbSpc21;
                if (idx == 3) lsvChamber = lsvChmbSpc22;

                lsvChamber.Columns.Clear();

                lsvChamber.Columns.Add("No.");
                lsvChamber.Columns.Add("온도(℃)");
                lsvChamber.Columns.Add("대기(분)");
                lsvChamber.Columns.Add("테스트(분)");
                lsvChamber.Columns.Add("제어 Min");
                lsvChamber.Columns.Add("제어 Max");
                lsvChamber.Columns.Add("온도 Over(℃)");
                lsvChamber.Columns.Add("분포 Over(℃)");
                lsvChamber.Columns.Add("분포안정시간");
                lsvChamber.Columns.Add("제어 Ramp");
                lsvChamber.Columns.Add("분포도 Min");
                lsvChamber.Columns.Add("분포도 Max");
                lsvChamber.Columns.Add("판정결과");

                int w = lsvChamber.Width / lsvChamber.Columns.Count;
                int tw = 0;

                for (int i = 0; i < lsvChamber.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        lsvChamber.Columns[i].Width = 50;
                    }
                    else if (i == 6 || i == 7)
                    {
                        lsvChamber.Columns[i].Width = w + 20;
                    }

                    else
                    {
                        lsvChamber.Columns[i].Width = w;
                    }
                    tw += lsvChamber.Columns[i].Width;
                }
                lsvChamber.Columns[lsvChamber.Columns.Count - 1].Width = w + lsvChamber.Width - tw - 5;
             }

            lsvChmbSpc11.Items.Clear();
            lsvChmbSpc12.Items.Clear();
            lsvChmbSpc21.Items.Clear();
            lsvChmbSpc22.Items.Clear();

            for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
            {
                ListViewItem itm1;
                ListViewItem itm2;
                ListViewItem itm3;
                ListViewItem itm4;

                itm1 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });
                itm2 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });
                itm3 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });
                itm4 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });

                lsvChmbSpc11.Items.Add(itm1);
                lsvChmbSpc12.Items.Add(itm2);
                lsvChmbSpc21.Items.Add(itm3);
                lsvChmbSpc22.Items.Add(itm4);
            }

            UpdateChamberTestSheetList(lsvChmbSpc11);
            UpdateChamberTestSheetList(lsvChmbSpc12);
            UpdateChamberTestSheetList(lsvChmbSpc21);
            UpdateChamberTestSheetList(lsvChmbSpc22);
        }

        public void UpdateTestSheet()
        {
            for(int ch = 0; ch <4; ch++)
            {
                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    specTpChamber[ch][i].tsp = Cfg.TpCfg.TSp[i];
                    specTpChamber[ch][i].waitTm = Cfg.TpCfg.WaitTm[i];
                    specTpChamber[ch][i].testTm = Cfg.TpCfg.TestTm[i];

                    specTpChamber[ch][i].jugTDisp = Cfg.TpCfg.TempDiff[i];
                    specTpChamber[ch][i].jugRamp = Cfg.TpCfg.Ramp[i];
                    specTpChamber[ch][i].jugUnif = Cfg.TpCfg.Uniformity[i];
                    specTpChamber[ch][i].jugStableTm = Cfg.TpCfg.StblTm[i];
                    specTpChamber[ch][i].jugTOver = Cfg.TpCfg.TOver[i];

                    specTpChamber[ch][i].bUseTDisp = Cfg.TpCfg.bUseTDiff[i];
                    specTpChamber[ch][i].bUseRamp = Cfg.TpCfg.bUseRamp[i];
                    specTpChamber[ch][i].bUseUnif = Cfg.TpCfg.bUseUnif[i];
                    specTpChamber[ch][i].bUseTOver = Cfg.TpCfg.bUseTOver[i];
                }
            }

            UpdateChamberTestSheetList(lsvChmbSpc11);
            UpdateChamberTestSheetList(lsvChmbSpc12);
            UpdateChamberTestSheetList(lsvChmbSpc21);
            UpdateChamberTestSheetList(lsvChmbSpc22);
        }

        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (sender == lsvChmbSpc11 || sender == lsvChmbSpc12 || sender == lsvChmbSpc21 || sender == lsvChmbSpc22)
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    e.Graphics.FillRectangle(Brushes.LightSalmon, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                }
            }

            System.Drawing.Rectangle cr = new System.Drawing.Rectangle(
                                          new System.Drawing.Point(e.Bounds.Location.X - 1, e.Bounds.Location.Y - 1),
                                          new System.Drawing.Size(e.Bounds.Width, e.Bounds.Height));
            e.Graphics.DrawRectangle(Pens.Gray, cr);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Far;

            System.Drawing.Font fnt = new System.Drawing.Font("돋음", 9);
            e.Graphics.DrawString(e.Header.Text, fnt, Brushes.Gray, e.Bounds, drawFormat);
        }

        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        { 
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Far;

            ListViewItem.ListViewSubItem itm = e.SubItem;

            int workingIdx = 0;

            if (sender == lsvChmbSpc11) workingIdx = _chamberWorkingIdx[0];
            if (sender == lsvChmbSpc12) workingIdx = _chamberWorkingIdx[1];
            if (sender == lsvChmbSpc21) workingIdx = _chamberWorkingIdx[2];
            if (sender == lsvChmbSpc22) workingIdx = _chamberWorkingIdx[3];

            if (workingIdx == e.ItemIndex)
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds);

                System.Drawing.Font fnt = new System.Drawing.Font("돋음", 12);
                e.Graphics.DrawString(itm.Text, fnt, SystemBrushes.HighlightText, e.Bounds, drawFormat);
            }

            else
            {
                //e.DrawBackground();

                System.Drawing.Font fnt = new System.Drawing.Font("돋음", 9);
                e.Graphics.DrawString(itm.Text, fnt, Brushes.DarkGray, e.Bounds, drawFormat);
            }
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int listIdx = 0;
                if (sender == lsvChmbSpc11) listIdx = 0;
                if (sender == lsvChmbSpc12) listIdx = 1;
                if (sender == lsvChmbSpc21) listIdx = 2;
                if (sender == lsvChmbSpc22) listIdx = 3;

                ref int workingIdx = ref _chamberWorkingIdx[listIdx];
                ListViewNF lstView = sender as ListViewNF;
                
                if (_bWorkChamber[listIdx])
                {
                    return;
                }

                ListViewItem itm = lstView.GetItemAt(e.X, e.Y);
                if (itm != null)
                {
                    workingIdx = itm.Index;
                }
            }
        }

        private void OnReportInfoUpdate(object sender, EventArgs e)
        {
            Cfg.EqmtCfg[EqmtIdx].Serial = txtSerialNo.Text;
            Cfg.EqmtCfg[EqmtIdx].AmbTemp = txtAmbTemp.Text;
            Cfg.EqmtCfg[EqmtIdx].AmbHumi = txtAmbHumi.Text;
            Cfg.EqmtCfg[EqmtIdx].CoolTemp = txtCoolTemp.Text;
            Cfg.EqmtCfg[EqmtIdx].Approv = txtApprov.Text;

            if (UpdateConfig != null)
            {
                UpdateConfig(this, EventArgs.Empty);
            }
        }

        private void btnAllStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("일괄 중지 하시겠습니까?",
                "일괄 정지?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            if (Comm.IsRun)
            {
                //------------------------------------------------------------------//
                // Write Temp Chamber #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHAMBER11, 102, 4);
                
                //------------------------------------------------------------------//
                // Write Temp Chamber #2 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHAMBER12, 102, 4);
                
                //------------------------------------------------------------------//
                // Write Temp Chamber #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHAMBER21, 102, 4);
                
                //------------------------------------------------------------------//
                // Write Temp Chamber #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHAMBER22, 102, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                //------------------------------------------------------------------//
                // Write Recorder #2 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);
            }
        }

        private void btnChamberHold_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;

            int chIdx = 0;

            if      (btn == btnChamber11Hold) chIdx = 0;
            else if (btn == btnChamber12Hold) chIdx = 1;
            else if (btn == btnChamber21Hold) chIdx = 2;
            else if (btn == btnChamber22Hold) chIdx = 3;

            if (_bChamberHold[chIdx] == false)
            {
                _bChamberHold[chIdx] = true;
                btn.BackColor = Color.Salmon;
                btn.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                _bChamberHold[chIdx] = false;
                btn.BackColor = Color.WhiteSmoke;
                btn.ForeColor = Color.Black;

                TSpecTpChamber spc = specTpChamber[chIdx][_chamberWorkingIdx[chIdx]];
                if (spc.workingSts == WorkingSts.Testing)
                {
                    spc.workStartTm = DateTime.Now;
                    spc.resCtrTMin = SysDefs.NOT_DEFVAL;
                    spc.resCtrTMax = SysDefs.NOT_DEFVAL;

                    spc.resUnifMin = SysDefs.NOT_DEFVAL;
                    spc.resUnifMax = SysDefs.NOT_DEFVAL;

                    spc.resRec[SysDefs.CH1].Clear();
                    spc.resRec[SysDefs.CH2].Clear();
                    spc.resRec[SysDefs.CH3].Clear();
                    spc.resRec[SysDefs.CH4].Clear();
                }
            }
        }

        private void btnChamberStep_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            int chIdx = 0;

            if (btn == btnChamber11Step) chIdx = 0;
            else if (btn == btnChamber12Step) chIdx = 1;
            else if (btn == btnChamber21Step) chIdx = 2;
            else if (btn == btnChamber22Step) chIdx = 3;

            if (_bChamberStep[chIdx] == false)
            {
                if (MessageBox.Show("Step 동작을 설정 하시겠습니까?",
                    "Step 설정?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                _bChamberStep[chIdx] = true;
                btn.BackColor = Color.Salmon;
                btn.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                if (MessageBox.Show("Step 동작을 해제 하시겠습니까?",
                    "Step 해제?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                _bChamberStep[chIdx] = false;
                btn.BackColor = Color.WhiteSmoke;
                btn.ForeColor = Color.Black;
            }

            TSpecTpChamber spc = specTpChamber[chIdx][_chamberWorkingIdx[chIdx]];
            if (spc.workingSts == WorkingSts.Testing)
            {
                spc.workStartTm = DateTime.Now;
                spc.resCtrTMin = SysDefs.NOT_DEFVAL;
                spc.resCtrTMax = SysDefs.NOT_DEFVAL;

                spc.resUnifMin = SysDefs.NOT_DEFVAL;
                spc.resUnifMax = SysDefs.NOT_DEFVAL;

                spc.resRec[SysDefs.CH1].Clear();
                spc.resRec[SysDefs.CH2].Clear();
                spc.resRec[SysDefs.CH3].Clear();
                spc.resRec[SysDefs.CH4].Clear();
            }
        }
    }
}
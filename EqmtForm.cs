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
    enum RoomType
    {
        Room1,
        Room2,
    };

    public enum EqmtType
    {
        Temi,
        Temp,
    };

    public partial class EqmtForm : Form
    {
        public DateTime WorkStartTime {  get; private set; }
        public bool bWorkingNow { get; private set; }
        public bool bWorkFinish { get; private set; }

        public SerialComm Comm { get; }
        public event EventHandler UpdateConfig;
        public event EventHandler EqmtClose;

        public int idx = 0;
        public int EqmtIdx { get; }
        private Config Cfg;

        private bool _doFirstRunChamber1 = false;
        private bool _doFirstRunChamber2 = false;
        private bool _doFirstRunChiller1 = false;
        private bool _doFirstRunChiller2 = false;

        private ChamberSts _chamber1 = new ChamberSts();
        private ChillerSts _chiller1 = new ChillerSts();
        private RecorderSts _recorder1 = new RecorderSts();

        private ChamberSts _chamber2 = new ChamberSts();
        private ChillerSts _chiller2 = new ChillerSts();
        private RecorderSts _recorder2 = new RecorderSts();

        private TSpecTmChamber[] specTmChamber1 = new TSpecTmChamber[SysDefs.TEMI_TEST_CNT];
        private TSpecTmChamber[] specTmChamber2 = new TSpecTmChamber[SysDefs.TEMI_TEST_CNT];

        private TSpecTpChamber[] specTpChamber1 = new TSpecTpChamber[SysDefs.TEMP_TEST_CNT];
        private TSpecTpChamber[] specTpChamber2 = new TSpecTpChamber[SysDefs.TEMP_TEST_CNT];

        private TSpecChiller[] specChiller1 = new TSpecChiller[SysDefs.CHILLER_TEST_CNT];
        private TSpecChiller[] specChiller2 = new TSpecChiller[SysDefs.CHILLER_TEST_CNT];

        private int _chamber1WorkingIdx = 0;
        private int _chiller1WorkingIdx = 0;

        private int _chamber2WorkingIdx = 0;
        private int _chiller2WorkingIdx = 0;

        private List<int> _chamber1TestList = new List<int>();
        private List<int> _chiller1TestList = new List<int>();

        private List<int> _chamber2TestList = new List<int>();
        private List<int> _chiller2TestList = new List<int>();

        private System.Windows.Forms.Timer _workChamber1Tmr;
        private System.Windows.Forms.Timer _workChiller1Tmr;

        private System.Windows.Forms.Timer _workChamber2Tmr;
        private System.Windows.Forms.Timer _workChiller2Tmr;

        private bool _bWorkChamber1 = false;
        private bool _bWorkChiller1 = false;

        private bool _bWorkChamber2 = false;
        private bool _bWorkChiller2 = false;

        private bool _bReadyRoom1;
        private bool _bReadyRoom2;

        private bool _bChamber1Hold = false;
        private bool _bChiller1Hold = false;
        private bool _bChamber2Hold = false;
        private bool _bChiller2Hold = false;

        private EqmtType _eqmtType;

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        private void EqmtForm_Load(object sender, EventArgs e)
        {

        }

        public void SetType(EqmtType type)
        {
            this._eqmtType = type;

            if (_eqmtType == EqmtType.Temi)
            {
                this.lblTitle.Text = string.Format("항온항습\n Chamber #{0}", EqmtIdx + 1);
            }

            else
            {
                this.lblTitle.Text = string.Format("항온\n  Chamber #{0}", EqmtIdx + 1);
            }

            InitChamberTestSheetList();
            UpdateTestSheet();
        }

        public EqmtForm(int _idx, Config _cfg) 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            EqmtIdx = _idx;
            Cfg = _cfg;

            _eqmtType = EqmtType.Temi;

            if(_eqmtType == EqmtType.Temi)
            {
                this.lblTitle.Text = string.Format("항온항습\n  Chamber #{0}", EqmtIdx + 1);
            }

            else
            {
                this.lblTitle.Text = string.Format("항온\n  Chamber #{0}", EqmtIdx + 1);
            }
            
            bWorkingNow = false;
            bWorkFinish = false;

            for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++)
            {
                specTmChamber1[i] = new TSpecTmChamber();
                specTmChamber2[i] = new TSpecTmChamber();
            }

            for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
            {
                specTpChamber1[i] = new TSpecTpChamber();
                specTpChamber2[i] = new TSpecTpChamber();
            }

            for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
            {
                specChiller1[i] = new TSpecChiller();
                specChiller2[i] = new TSpecChiller();
            }

            _bReadyRoom1 = false;
            _bReadyRoom2 = false;

            //----------------------------------------------------------------//
            // Result report infomations
            //----------------------------------------------------------------//
            txtSerialNo.Text = _cfg.EqmtCfg[EqmtIdx].Serial;
            txtAmbTemp.Text = _cfg.EqmtCfg[EqmtIdx].AmbTemp;
            txtAmbHumi.Text = _cfg.EqmtCfg[EqmtIdx].AmbHumi;
            txtApprov.Text = _cfg.EqmtCfg[EqmtIdx].Approv;
            txtCoolTemp.Text = _cfg.EqmtCfg[EqmtIdx].CoolTemp;
            
            //----------------------------------------------------------------//
            // Set work #1 timer.
            //----------------------------------------------------------------//
            _workChamber1Tmr = new System.Windows.Forms.Timer();
            _workChamber1Tmr.Tick += new EventHandler(OnWorkChamberTmrEvent);
            _workChamber1Tmr.Interval = 1000;

            _workChiller1Tmr = new System.Windows.Forms.Timer();
            _workChiller1Tmr.Tick += new EventHandler(OnWorkChillerTmrEvent);
            _workChiller1Tmr.Interval = 1000;

            _chamber1WorkingIdx = 0;
            _chiller1WorkingIdx = 0;

            //----------------------------------------------------------------//
            // Set work #2 timer.
            //----------------------------------------------------------------//
            _workChamber2Tmr = new System.Windows.Forms.Timer();
            _workChamber2Tmr.Tick += new EventHandler(OnWorkChamberTmrEvent);
            _workChamber2Tmr.Interval = 1000;

            _workChiller2Tmr = new System.Windows.Forms.Timer();
            _workChiller2Tmr.Tick += new EventHandler(OnWorkChillerTmrEvent);
            _workChiller2Tmr.Interval = 1000;

            _chamber2WorkingIdx = 0;
            _chiller2WorkingIdx = 0;

            //--------------------------------------------------------------------------//
            // Chamber #1, #2 Comm. Status
            //--------------------------------------------------------------------------//
            lblChamb1Sts.Text = "OFFLINE";
            lblChiller1Sts.Text = "OFFLINE";
            lblRecorder1Sts.Text = "OFFLINE";

            lblChamb2Sts.Text = "OFFLINE";
            lblChiller2Sts.Text = "OFFLINE";
            lblRecorder2Sts.Text = "OFFLINE";

            UpdateTestSheet();
            InitChamberTestSheetList();

            Comm = new SerialComm();
            Comm.UpdateCtrlStatus += new UpdateCtrlStatusEventHandler(onUpdateCtrlSts);
            Comm.CommClose += new EventHandler(OnCommClosed);

            //btnConnect_Click(this, EventArgs.Empty);

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
                if(_eqmtType == EqmtType.Temi)
                {
                    //------------------------------------------------------------------//
                    // Write Temi Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 101, 4);

                    //------------------------------------------------------------------//
                    // Write Temi Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 101, 4);
                }
                else
                {
                    //------------------------------------------------------------------//
                    // Write Temp Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 104, 4);

                    //------------------------------------------------------------------//
                    // Write Temp Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 104, 4);
                }
                
                //------------------------------------------------------------------//
                // Write Chiller #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 102, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                //--------------------------------------------------//
                // Subch 11 Stop(1) (유량)
                //--------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 2831, 1);

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
            _chamber1.bOnLine = false;
            _chiller1.bOnLine = false;
            _recorder1.bOnLine = false;

            _chamber2.bOnLine = false;
            _chiller2.bOnLine = false;
            _recorder2.bOnLine = false;
        }

        //--------------------------------------------------------------------------//
        // Current Date/Time
        //--------------------------------------------------------------------------//
        private void OnTimedEvent(Object src, ElapsedEventArgs e)
        {
            UpdateStatus();

            if(_workChamber1Tmr.Enabled || _workChamber2Tmr.Enabled || _workChiller1Tmr.Enabled || _workChiller2Tmr.Enabled)
            {
                if(bWorkingNow == false)
                {
                    WorkStartTime = DateTime.Now;
                    bWorkFinish = false;
                }
                bWorkingNow = true;
            }
            else
            {
                if (bWorkingNow == true)
                {
                    bWorkFinish = true;
                }
                bWorkingNow = false;
            }

            // Room 1
            if (_workChamber1Tmr.Enabled) _bWorkChamber1 = true;
            else   _bWorkChamber1 = false;
            
            if (_workChiller1Tmr.Enabled) _bWorkChiller1 = true;
            else _bWorkChiller1 = false;

            // Room2
            if (_workChamber2Tmr.Enabled) _bWorkChamber2 = true;
            else _bWorkChamber2 = false;

            if (_workChiller2Tmr.Enabled) _bWorkChiller2 = true;
            else _bWorkChiller2 = false;


            if(_bWorkChamber1 == false)
            {
                btnStartChamber1.BackColor = Color.LightGray;
                btnStartChamber1.ForeColor = Color.Black;

                btnStartSelectedChamber1.BackColor = Color.LightGray;
                btnStartSelectedChamber1.ForeColor = Color.Black;
            }

            if (_bWorkChiller1 == false)
            {
                btnStartChiller1.BackColor = Color.LightGray;
                btnStartChiller1.ForeColor = Color.Black;

                btnStartSelectedChiller1.BackColor = Color.LightGray;
                btnStartSelectedChiller1.ForeColor = Color.Black;
            }

            if (_bWorkChamber2 == false)
            {
                btnStartChamber2.BackColor = Color.LightGray;
                btnStartChamber2.ForeColor = Color.Black;

                btnStartSelectedChamber2.BackColor = Color.LightGray;
                btnStartSelectedChamber2.ForeColor = Color.Black;
            }

            if (_bWorkChiller2 == false)
            {
                btnStartChiller2.BackColor = Color.LightGray;
                btnStartChiller2.ForeColor = Color.Black;

                btnStartSelectedChiller2.BackColor = Color.LightGray;
                btnStartSelectedChiller2.ForeColor = Color.Black;
            }

            if (_bWorkChamber1 == false && _bWorkChiller1 == false)
            {
                this.btnStartRoom1.Enabled = true;
            }
            else
            {
                this.btnStartRoom1.Enabled = false;
            }

            if (_bWorkChamber2 == false && _bWorkChiller2 == false)
            {
                this.btnStartRoom2.Enabled = true;
            }
            else
            {
                this.btnStartRoom2.Enabled = false;
            }
        }

        private void UpdateStatus()
        {
            _bReadyRoom1 = true;
            _bReadyRoom2 = true;

            //--------------------------------------------------------------------------//
            // Chamber#1 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber1.bOnLine)
            {
                lblChamb1Sts.Text = "ONLINE";
                lblChamb1Sts.BackColor = Color.Salmon;
                lblChamb1Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb1Sts.Text = "OFFLINE";
                lblChamb1Sts.BackColor = Color.DarkGray;
                lblChamb1Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom1 = false;
            }

            if (_chiller1.bOnLine)
            {
                lblChiller1Sts.Text = "ONLINE";
                lblChiller1Sts.BackColor = Color.Salmon;
                lblChiller1Sts.ForeColor = Color.WhiteSmoke;
            }

            else
            {
                lblChiller1Sts.Text = "OFFLINE";
                lblChiller1Sts.BackColor = Color.DarkGray;
                lblChiller1Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom1 = false;
            }

            if (_recorder1.bOnLine)
            {
                lblRecorder1Sts.Text = "ONLINE";
                lblRecorder1Sts.BackColor = Color.Salmon;
                lblRecorder1Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder1Sts.Text = "OFFLINE";
                lblRecorder1Sts.BackColor = Color.DarkGray;
                lblRecorder1Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom1 = false;
            }

            //--------------------------------------------------------------------------//
            // Chamber#2 Comm. Status
            //--------------------------------------------------------------------------//
            if (_chamber2.bOnLine)
            {
                lblChamb2Sts.Text = "ONLINE";
                lblChamb2Sts.BackColor = Color.Salmon;
                lblChamb2Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblChamb2Sts.Text = "OFFLINE";
                lblChamb2Sts.BackColor = Color.DarkGray;
                lblChamb2Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom2 = false;
            }

            if (_chiller2.bOnLine)
            {
                lblChiller2Sts.Text = "ONLINE";
                lblChiller2Sts.BackColor = Color.Salmon;
                lblChiller2Sts.ForeColor = Color.WhiteSmoke;
            }

            else
            {
                lblChiller2Sts.Text = "OFFLINE";
                lblChiller2Sts.BackColor = Color.DarkGray;
                lblChiller2Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom2 = false;
            }

            if (_recorder2.bOnLine)
            {
                lblRecorder2Sts.Text = "ONLINE";
                lblRecorder2Sts.BackColor = Color.Salmon;
                lblRecorder2Sts.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblRecorder2Sts.Text = "OFFLINE";
                lblRecorder2Sts.BackColor = Color.DarkGray;
                lblRecorder2Sts.ForeColor = Color.WhiteSmoke;

                _bReadyRoom2 = false;
            }

            if (_bReadyRoom1)
            {
                lblStsRoom1.BackColor = Color.Salmon;
                lblStsRoom1.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                if (idx % 2 == 0)
                {
                    lblStsRoom1.BackColor = Color.Red;
                    lblStsRoom1.ForeColor = Color.WhiteSmoke;
                }
                else
                {
                    lblStsRoom1.BackColor = Color.Black;
                    lblStsRoom1.ForeColor = Color.WhiteSmoke;
                }
            }

            if (_bReadyRoom2)
            {
                lblStsRoom2.BackColor = Color.Salmon;
                lblStsRoom2.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                if (idx % 2 == 0)
                {
                    lblStsRoom2.BackColor = Color.Red;
                    lblStsRoom2.ForeColor = Color.WhiteSmoke;
                }
                else
                {
                    lblStsRoom2.BackColor = Color.Black;
                    lblStsRoom2.ForeColor = Color.WhiteSmoke;
                }
            }
            idx++;
        }

        private void UpdateChamberTestSheetList(ListViewNF lsv)
        {
            if (lsv.Items.Count == 0) return;

            // Temi
            if(_eqmtType == EqmtType.Temi)
            {
                for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++)
                {
                    TSpecTmChamber sp1 = new TSpecTmChamber();

                    if (lsv == lsvChmbSpc1) sp1 = specTmChamber1[i];
                    if (lsv == lsvChmbSpc2) sp1 = specTmChamber2[i];

                    string strTsp1 = SysDefs.DotString(sp1.tsp, 1) + "℃";
                    string strHsp1 = SysDefs.DotString(sp1.hsp, 1) + "%";

                    string strWTm1 = (sp1.waitTm == 0) ? "-" : sp1.waitTm.ToString() + "Min";
                    string strTTm1 = (sp1.testTm == 0) ? "-" : sp1.testTm.ToString() + "Min";

                    string strResult1 = "";
                    string strResTCtrMin1 = "-";
                    string strResHCtrMin1 = "-";

                    string strResTCtrMax1 = "-";
                    string strResHCtrMax1 = "-";
                    string strResTOver = "-";
                    string strResHOver = "-";
                    string strResROver = "-";
                    string strResStableTm = "-";
                    string strResRamp1 = "-";

                    string strResUnifMin1 = "-";
                    string strResUnifMax1 = "-";

                    if (sp1.result == WorkingRes.NotDef)
                    {
                        if (sp1.workingSts == WorkingSts.Begin) strResult1 = "Ready..";
                        if (sp1.workingSts == WorkingSts.TouchSpCheck) strResult1 = "Checking PV";
                        if (sp1.workingSts == WorkingSts.Waiting) strResult1 = "Wating..";
                        if (sp1.workingSts == WorkingSts.Testing) strResult1 = "Testing..";
                        if (sp1.workingSts == WorkingSts.Holding) strResult1 = "Holding..";
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
                    strResHCtrMin1 = (sp1.bUseHDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrHMin, 1);

                    strResTCtrMax1 = (sp1.bUseTDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrTMax, 1);
                    strResHCtrMax1 = (sp1.bUseHDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrHMax, 1);
                    strResRamp1 = (sp1.bUseRamp == false) ? "-" : SysDefs.DotString(sp1.resCtrRamp, 1);

                    strResUnifMin1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMin, 1);
                    strResUnifMax1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMax, 1);

                    strResTOver = (sp1.bUseTOver == false) ? "-" : SysDefs.DotString(sp1.resTOver, 1);
                    strResHOver = (sp1.bUseHOver == false) ? "-" : SysDefs.DotString(sp1.resHOver, 1);
                    strResROver = SysDefs.DotString(sp1.resROver, 1);
                    strResStableTm = sp1.resStableTm.ToString();

                    if (sp1.resCtrTMin == SysDefs.NOT_DEFVAL) strResTCtrMin1 = "-";
                    if (sp1.resCtrTMax == SysDefs.NOT_DEFVAL) strResTCtrMax1 = "-";

                    if (sp1.resCtrHMin == SysDefs.NOT_DEFVAL) strResHCtrMin1 = "-";
                    if (sp1.resCtrHMax == SysDefs.NOT_DEFVAL) strResHCtrMax1 = "-";

                    if (sp1.resCtrRamp == SysDefs.NOT_DEFVAL) strResRamp1 = "-";

                    if (sp1.resUnifMin == SysDefs.NOT_DEFVAL) strResUnifMin1 = "-";
                    if (sp1.resUnifMax == SysDefs.NOT_DEFVAL) strResUnifMax1 = "-";

                    if (sp1.resTOver == SysDefs.NOT_DEFVAL) strResTOver = "-";
                    if (sp1.resHOver == SysDefs.NOT_DEFVAL) strResHOver = "-";
                    if (sp1.resROver == SysDefs.NOT_DEFVAL) strResROver = "-";

                    if (sp1.resStableTm == SysDefs.NOT_DEFVAL) strResStableTm = "-";

                    lsv.Items[i].SubItems[0].Text = String.Format("{0}", i + 1);
                    lsv.Items[i].SubItems[1].Text = strTsp1;
                    lsv.Items[i].SubItems[2].Text = strHsp1;
                    lsv.Items[i].SubItems[3].Text = strWTm1;
                    lsv.Items[i].SubItems[4].Text = strTTm1;
                    lsv.Items[i].SubItems[5].Text = strResTCtrMin1 + "/" + strResHCtrMin1;
                    lsv.Items[i].SubItems[6].Text = strResTCtrMax1 + "/" + strResHCtrMax1;

                    lsv.Items[i].SubItems[7].Text = strResTOver;
                    lsv.Items[i].SubItems[8].Text = strResHOver;
                    lsv.Items[i].SubItems[9].Text = strResROver;
                    lsv.Items[i].SubItems[10].Text = strResStableTm;

                    lsv.Items[i].SubItems[11].Text = strResRamp1;
                    lsv.Items[i].SubItems[12].Text = strResUnifMin1;
                    lsv.Items[i].SubItems[13].Text = strResUnifMax1;
                
                    lsv.Items[i].SubItems[14].Text = strResult1;
                }
            }

            // Temp
            else
            {
                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    TSpecTpChamber sp1 = new TSpecTpChamber();

                    if (lsv == lsvChmbSpc1) sp1 = specTpChamber1[i];
                    if (lsv == lsvChmbSpc2) sp1 = specTpChamber2[i];

                    string strTsp1 = SysDefs.DotString(sp1.tsp, 1) + "℃";
                    string strWTm1 = (sp1.waitTm == 0) ? "-" : sp1.waitTm.ToString() + "Min";
                    string strTTm1 = (sp1.testTm == 0) ? "-" : sp1.testTm.ToString() + "Min";

                    string strResult1 = "";
                    string strResTCtrMin1 = "-";
                    string strResTCtrMax1 = "-";
                    string strResTOver = "-";
                    string strResROver = "-";
                    string strResStableTm = "-";
                    string strResRamp1 = "-";

                    string strResUnifMin1 = "-";
                    string strResUnifMax1 = "-";

                    if (sp1.result == WorkingRes.NotDef)
                    {
                        if (sp1.workingSts == WorkingSts.Begin) strResult1 = "Ready..";
                        if (sp1.workingSts == WorkingSts.TouchSpCheck) strResult1 = "Checking PV";
                        if (sp1.workingSts == WorkingSts.Waiting) strResult1 = "Wating..";
                        if (sp1.workingSts == WorkingSts.Testing) strResult1 = "Testing..";
                        if (sp1.workingSts == WorkingSts.Holding) strResult1 = "Holding..";
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
                    strResRamp1 = (sp1.bUseRamp == false) ? "-" : SysDefs.DotString(sp1.resCtrRamp, 1);

                    strResUnifMin1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMin, 1);
                    strResUnifMax1 = (sp1.bUseUnif == false) ? "-" : SysDefs.DotString(sp1.resUnifMax, 1);

                    strResTOver = (sp1.bUseTOver == false) ? "-" : SysDefs.DotString(sp1.resTOver, 1);
                    strResROver = SysDefs.DotString(sp1.resROver, 1);
                    strResStableTm = sp1.resStableTm.ToString();

                    if (sp1.resCtrTMin == SysDefs.NOT_DEFVAL) strResTCtrMin1 = "-";
                    if (sp1.resCtrTMax == SysDefs.NOT_DEFVAL) strResTCtrMax1 = "-";

                    if (sp1.resCtrRamp == SysDefs.NOT_DEFVAL) strResRamp1 = "-";

                    if (sp1.resUnifMin == SysDefs.NOT_DEFVAL) strResUnifMin1 = "-";
                    if (sp1.resUnifMax == SysDefs.NOT_DEFVAL) strResUnifMax1 = "-";

                    if (sp1.resTOver == SysDefs.NOT_DEFVAL) strResTOver = "-";
                    if (sp1.resROver == SysDefs.NOT_DEFVAL) strResROver = "-";

                    if (sp1.resStableTm == SysDefs.NOT_DEFVAL) strResStableTm = "-";

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
        }

        private void UpdateChillerTestSheetList(ListViewNF lsv)
        {
            if (lsv.Items.Count == 0) return;

            for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
            {
                TSpecChiller sp1 = new TSpecChiller();

                if (lsv == lsvChillerSpc1) sp1 = specChiller1[i];
                if (lsv == lsvChillerSpc2) sp1 = specChiller2[i];

                string strTsp1 = SysDefs.DotString(sp1.tsp, 1) + "℃";
                string strSSp1 = SysDefs.DotString(sp1.ssp, 2) + "lpm";

                string strWTm1 = (sp1.waitTm == 0) ? "-" : sp1.waitTm.ToString() + "Min";
                string strTTm1 = (sp1.testTm == 0) ? "-" : sp1.testTm.ToString() + "Min";

                string strJugTDisp1 = (sp1.bUseTDisp == false) ? "-" : ("±") + SysDefs.DotString(sp1.jugTDisp, 1) + "℃";
                string strJugSDisp1 = (sp1.bUseSDisp == false) ? "-" : ("±") + SysDefs.DotString(sp1.jugSDisp, 2) + "lpm";
                string strJugRamp1 = (sp1.bUseRamp == false) ? "-" : SysDefs.DotString(sp1.jugRamp, 1) + "℃/M";

                string strResTCtrMin1 = "-";
                string strResTCtrMax1 = "-";
                string strResSpCtrMin1 = "-";
                string strResSpCtrMax1 = "-";
                string strResRamp1 = "-";
                string strResult1 = "";

                if (sp1.result == WorkingRes.NotDef)
                {
                    if (sp1.workingSts == WorkingSts.Begin) strResult1 = "Ready..";
                    if (sp1.workingSts == WorkingSts.TouchSpCheck) strResult1 = "Checking PV";
                    if (sp1.workingSts == WorkingSts.Waiting) strResult1 = "Wating..";
                    if (sp1.workingSts == WorkingSts.Testing) strResult1 = "Testing..";
                    if (sp1.workingSts == WorkingSts.Holding) strResult1 = "Holding..";
                }

                else
                {
                    if (sp1.result == WorkingRes.Success) strResult1 = "GOOD";
                    if (sp1.result == WorkingRes.Fail) strResult1 = "NG";
                }

                strResTCtrMin1 = (sp1.bUseTDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrTMin, 1);
                strResTCtrMax1 = (sp1.bUseTDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrTMax, 1);
                strResSpCtrMin1 = (sp1.bUseSDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrSMin, 2);
                strResSpCtrMax1 = (sp1.bUseSDisp == false) ? "-" : SysDefs.DotString(sp1.resCtrSMax, 2);
                strResRamp1 = (sp1.bUseRamp == false) ? "-" : SysDefs.DotString(sp1.resCtrRamp, 1);

                if (sp1.resCtrTMin == SysDefs.NOT_DEFVAL) strResTCtrMin1 = "-";
                if (sp1.resCtrTMax == SysDefs.NOT_DEFVAL) strResTCtrMax1 = "-";

                if (sp1.resCtrSMin == SysDefs.NOT_DEFVAL) strResSpCtrMin1 = "-";
                if (sp1.resCtrSMax == SysDefs.NOT_DEFVAL) strResSpCtrMax1 = "-";

                if (sp1.resCtrRamp == SysDefs.NOT_DEFVAL) strResRamp1 = "-";


                lsv.Items[i].SubItems[0].Text = String.Format("{0}", i + 1);
                lsv.Items[i].SubItems[1].Text = strTsp1;
                lsv.Items[i].SubItems[2].Text = strSSp1;
                lsv.Items[i].SubItems[3].Text = strWTm1;
                lsv.Items[i].SubItems[4].Text = strTTm1;
                lsv.Items[i].SubItems[5].Text = strResTCtrMin1 + "/" + strResTCtrMax1;
                lsv.Items[i].SubItems[6].Text = strResSpCtrMin1 + "/" + strResSpCtrMax1;
                lsv.Items[i].SubItems[7].Text = strResRamp1;
                lsv.Items[i].SubItems[8].Text = strJugTDisp1;
                lsv.Items[i].SubItems[9].Text = strJugSDisp1;
                lsv.Items[i].SubItems[10].Text = strJugRamp1;
                lsv.Items[i].SubItems[11].Text = strResult1;
            }
        }

        private void EqmtStartStop(bool bStart, RoomType rt)
        {
            if (bStart)
            {
                if (rt == RoomType.Room1)
                {
                    btnStartRoom1.BackColor = Color.Salmon;
                    btnStartRoom1.ForeColor = Color.WhiteSmoke;

                    _doFirstRunChamber1 = false;
                    _doFirstRunChiller1 = false;

                    _workChamber1Tmr.Start();
                    _workChiller1Tmr.Start();

                    _chamber1WorkingIdx = 0;
                    _chiller1WorkingIdx = 0;

                    _bWorkChamber1 = true;
                    _bWorkChiller1 = true;

                    _chamber1TestList.Clear();
                    _chiller1TestList.Clear();

                    int idx1 = 0;
                    int idx2 = 0;

                    // Temi
                    if (_eqmtType == EqmtType.Temi)
                    {
                        foreach (TSpecTmChamber spc in specTmChamber1)
                        {
                            spc.Reset();
                            _chamber1TestList.Add(idx1);
                            idx1++;
                        }
                    }

                    // Temp
                    else
                    {
                        foreach (TSpecTpChamber spc in specTpChamber1)
                        {
                            spc.Reset();
                            _chamber1TestList.Add(idx1);
                            idx1++;
                        }
                    }

                    foreach (TSpecChiller spc in specChiller1)
                    {
                        spc.Reset();
                        _chiller1TestList.Add(idx2);
                        idx2++;
                    }
                }

                else if (rt == RoomType.Room2)
                {
                    btnStartRoom2.BackColor = Color.Salmon;
                    btnStartRoom2.ForeColor = Color.WhiteSmoke;

                    _doFirstRunChamber2 = false;
                    _doFirstRunChiller2 = false;

                    _workChamber2Tmr.Start();
                    _workChiller2Tmr.Start();

                    _chamber2WorkingIdx = 0;
                    _chiller2WorkingIdx = 0;

                    _bWorkChamber2 = true;
                    _bWorkChiller2 = true;

                    _chamber2TestList.Clear();
                    _chiller2TestList.Clear();

                    int idx1 = 0;
                    int idx2 = 0;

                    // Temi
                    if (_eqmtType == EqmtType.Temi)
                    {
                        foreach (TSpecTmChamber spc in specTmChamber2)
                        {
                            spc.Reset();
                            _chamber2TestList.Add(idx1);
                            idx1++;
                        }
                    }
                    // Temp
                    else
                    {
                        foreach (TSpecTpChamber spc in specTpChamber2)
                        {
                            spc.Reset();
                            _chamber2TestList.Add(idx1);
                            idx1++;
                        }
                    }

                    foreach (TSpecChiller spc in specChiller2)
                    {
                        spc.Reset();
                        _chiller2TestList.Add(idx2);
                        idx2++;
                    }
                }
            }
            else
            {
                if (rt == RoomType.Room1)
                {
                    btnStartRoom1.BackColor = Color.LightGray;
                    btnStartRoom1.ForeColor = Color.Black;

                    _workChamber1Tmr.Stop();
                    _workChiller1Tmr.Stop();

                    // Temi
                    if (_eqmtType == EqmtType.Temi)
                    {
                        //------------------------------------------------------------------//
                        // Write Chamber #1 Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHAMBER1, 101, 4);
                    }
                    else
                    {
                        //------------------------------------------------------------------//
                        // Write Chamber #1 Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHAMBER1, 102, 4);
                    }
                    //------------------------------------------------------------------//
                    // Write Chiller #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHILLER1, 102, 4);

                    //------------------------------------------------------------------//
                    // Write Recorder #1 Record Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                    //--------------------------------------------------//
                    // Subch 11 Stop(1) (유량)
                    //--------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHILLER1, 2831, 1);

                    _bWorkChamber1 = false;
                    _bWorkChiller1 = false;

                    _chamber1TestList.Clear();
                    _chiller1TestList.Clear();
                }

                else if (rt == RoomType.Room2)
                {
                    btnStartRoom2.BackColor = Color.LightGray;
                    btnStartRoom2.ForeColor = Color.Black;

                    _workChamber2Tmr.Stop();
                    _workChiller2Tmr.Stop();

                    // Temi
                    if (_eqmtType == EqmtType.Temi)
                    {
                        //------------------------------------------------------------------//
                        // Write Chamber #1 Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHAMBER2, 101, 4);
                    }
                    else
                    {
                        //------------------------------------------------------------------//
                        // Write Chamber #1 Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHAMBER2, 102, 4);
                    }

                    //------------------------------------------------------------------//
                    // Write Chiller #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHILLER2, 102, 4);

                    //------------------------------------------------------------------//
                    // Write Recorder #1 Record Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                    //--------------------------------------------------//
                    // Subch 11 Stop(1) (유량)
                    //--------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHILLER2, 2831, 1);

                    _bWorkChamber2 = false;
                    _bWorkChiller2 = false;

                    _chamber2TestList.Clear();
                    _chiller2TestList.Clear();
                }
            }

            if (rt == RoomType.Room1)
            {
                UpdateChamberTestSheetList(lsvChmbSpc1);
                UpdateChillerTestSheetList(lsvChillerSpc1);
            }

            else if (rt == RoomType.Room2)
            {
                UpdateChamberTestSheetList(lsvChmbSpc2);
                UpdateChillerTestSheetList(lsvChillerSpc2);
            }
        }

        //-----------------------------------------------------------------//
        // Chamber & Chiller All Test
        //-----------------------------------------------------------------//
        public void StartTest()
        {
            OnStartRoomTest(this.btnStartRoom1, EventArgs.Empty);
            OnStartRoomTest(this.btnStartRoom2, EventArgs.Empty);
        }

        private void btnStartChamber1_Click(object sender, EventArgs e)
        {
            if (_bWorkChamber1 == true)
            {
                if (MessageBox.Show("챔버 일괄 테스트를 중지 하시겠습니까?",
                "테스트 정지?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChamber1.BackColor = Color.LightGray;
                btnStartChamber1.ForeColor = Color.Black;

                _workChamber1Tmr.Stop();

                // Temi
                if (_eqmtType == EqmtType.Temi)
                {
                    //------------------------------------------------------------------//
                    // Write Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 101, 4);
                }
                else
                {
                    //------------------------------------------------------------------//
                    // Write Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 102, 4);
                }

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                _bWorkChamber1 = false;
                _chamber1TestList.Clear();
            }

            else
            {
                if (MessageBox.Show("챔버 일괄 테스트를 실행 하시겠습니까?",
                "테스트 실행?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChamber1.BackColor = Color.Salmon;
                btnStartChamber1.ForeColor = Color.WhiteSmoke;

                _doFirstRunChamber1 = false;

                _workChamber1Tmr.Start();
                _chamber1WorkingIdx = 0;

                _bWorkChamber1 = true;
                _chamber1TestList.Clear();

                // Temi
                if(_eqmtType == EqmtType.Temi)
                {
                    int idx1 = 0;
                    foreach (TSpecTmChamber spc in specTmChamber1)
                    {
                        spc.Reset();
                        _chamber1TestList.Add(idx1);
                        idx1++;
                    }
                }

                // Temp
                else
                {
                    int idx1 = 0;
                    foreach (TSpecTpChamber spc in specTpChamber1)
                    {
                        spc.Reset();
                        _chamber1TestList.Add(idx1);
                        idx1++;
                    }
                }
            }
        }

        private void btnStartChiller1_Click(object sender, EventArgs e)
        {
            if (_bWorkChiller1 == true)
            {
                if (MessageBox.Show("칠러 일괄 테스트를 중지 하시겠습니까?",
                "테스트 정지?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChiller1.BackColor = Color.LightGray;
                btnStartChiller1.ForeColor = Color.Black;

                _workChiller1Tmr.Stop();

                //------------------------------------------------------------------//
                // Write Chiller #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 102, 4);

                //--------------------------------------------------//
                // Subch 11 Stop(1) (유량)
                //--------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 2831, 1);

                _bWorkChiller1 = false;
                _chiller1TestList.Clear();
            }

            else
            {
                if (MessageBox.Show("칠러 일괄 테스트를 수행 하시겠습니까?",
                "테스트 수행?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChiller1.BackColor = Color.Salmon;
                btnStartChiller1.ForeColor = Color.WhiteSmoke;

                _doFirstRunChiller1 = false;

                _workChiller1Tmr.Start();
                _chiller1WorkingIdx = 0;

                _bWorkChiller1 = true;
                _chiller1TestList.Clear();

                int idx2 = 0;
                foreach (TSpecChiller spc in specChiller1)
                {
                    spc.Reset();
                    _chiller1TestList.Add(idx2);
                    idx2++;
                }
            }
        }

        private void btnStartChamber2_Click(object sender, EventArgs e)
        {
            if (_bWorkChamber2 == true)
            {
                if (MessageBox.Show("챔버 일괄 테스트를 중지 하시겠습니까?",
                    "테스트 정지?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChamber2.BackColor = Color.LightGray;
                btnStartChamber2.ForeColor = Color.Black;

                _workChamber2Tmr.Stop();

                if (_eqmtType == EqmtType.Temi)
                {
                    //------------------------------------------------------------------//
                    // Write Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 101, 4);
                }
                else
                {
                    //------------------------------------------------------------------//
                    // Write Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 102, 4);
                }

                //------------------------------------------------------------------//
                // Write Recorder #2 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                _bWorkChamber2 = false;
                _chamber2TestList.Clear();
            }

            else
            {
                if (MessageBox.Show("챔버 일괄 테스트를 실행 하시겠습니까?",
                    "테스트 실행?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChamber2.BackColor = Color.Salmon;
                btnStartChamber2.ForeColor = Color.WhiteSmoke;

                _doFirstRunChamber2 = false;

                _workChamber2Tmr.Start();
                _chamber2WorkingIdx = 0;

                _bWorkChamber2 = true;
                _chamber2TestList.Clear();

                // Temi
                if (_eqmtType == EqmtType.Temi)
                {
                    int idx1 = 0;
                    foreach (TSpecTmChamber spc in specTmChamber2)
                    {
                        spc.Reset();
                        _chamber2TestList.Add(idx1);
                        idx1++;
                    }
                }

                // Temp
                else
                {
                    int idx1 = 0;
                    foreach (TSpecTpChamber spc in specTpChamber2)
                    {
                        spc.Reset();
                        _chamber2TestList.Add(idx1);
                        idx1++;
                    }
                }
            }
        }

        private void btnStartChiller2_Click(object sender, EventArgs e)
        {
            if (_bWorkChiller2 == true)
            {
                if (MessageBox.Show("칠러 일괄 테스트를 중지 하시겠습니까?",
                    "테스트 정지?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChiller2.BackColor = Color.LightGray;
                btnStartChiller2.ForeColor = Color.Black;

                _workChiller2Tmr.Stop();

                //------------------------------------------------------------------//
                // Write Chiller #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER2, 102, 4);

                //--------------------------------------------------//
                // Subch 11 Stop(1) (유량)
                //--------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER2, 2831, 1);

                _bWorkChiller2 = false;
                _chiller2TestList.Clear();
            }

            else
            {
                if (MessageBox.Show("칠러 일괄 테스트를 수행 하시겠습니까?",
                    "테스트 수행?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                btnStartChiller2.BackColor = Color.Salmon;
                btnStartChiller2.ForeColor = Color.WhiteSmoke;

                _doFirstRunChiller2 = false;

                _workChiller2Tmr.Start();
                _chiller2WorkingIdx = 0;

                _bWorkChiller2 = true;
                _chiller2TestList.Clear();

                int idx2 = 0;
                foreach (TSpecChiller spc in specChiller2)
                {
                    spc.Reset();
                    _chiller2TestList.Add(idx2);
                    idx2++;
                }
            }
        }

        //-----------------------------------------------------------------//
        // Chamber & Chiller All Test
        //-----------------------------------------------------------------//
        private void OnStartRoomTest(object sender, EventArgs e)
        {
            bool bWork = false;
            RoomType rt = RoomType.Room1;

            if (sender == btnStartRoom1)
            {
                bWork = _bWorkChamber1;
                rt = RoomType.Room1;
            }

            else if(sender == btnStartRoom2)
            {
                bWork = _bWorkChamber2;
                rt = RoomType.Room2;
            }

            if (bWork)
            {
                if (MessageBox.Show("장비 일괄 테스트를 중지 하시겠습니까?",
                   "테스트 정지?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    EqmtStartStop(false, rt);
                }
            }
            else
            {
                if(rt == RoomType.Room1)
                {
                    if(_chamber1.bOnLine== false || _chiller1.bOnLine==false || _recorder1.bOnLine == false)
                    {
                        return;
                    }
                }

                if (rt == RoomType.Room2)
                {
                    if (_chamber2.bOnLine==false || _chiller2.bOnLine== false || _recorder2.bOnLine == false)
                    {
                        return;
                    }
                }

                if (MessageBox.Show("장비 일괄 테스트 운전을 시작 하시겠습니까?",
                    "테스트 시작?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    EqmtStartStop(true, rt);
                }
            }
        }

        private void btnStartChamberTest_Click(object sender, EventArgs e)
        {
            bool bWork = false;
            int chmAddr = 0;
            int recAddr = 0;

            if (sender == this.btnStartSelectedChamber1)
            {
                bWork = _workChamber1Tmr.Enabled;
                chmAddr = SysDefs.ADDR_CHAMBER1;
                recAddr = SysDefs.ADDR_RECORDER1;
            }

            else if (sender == btnStartSelectedChamber2)
            {
                bWork = _workChamber2Tmr.Enabled;
                chmAddr = SysDefs.ADDR_CHAMBER2;
                recAddr = SysDefs.ADDR_RECORDER2;
            }

            if (bWork)
            {
                if (MessageBox.Show("챔버 테스트를 중지 하시겠습니까?",
                                    "장비 정지?",
                                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                if(chmAddr == SysDefs.ADDR_CHAMBER1)
                {
                    _workChamber1Tmr.Stop();
                    _bWorkChamber1 = false;

                    btnStartSelectedChamber1.BackColor = Color.LightGray;
                    btnStartSelectedChamber1.ForeColor = Color.Black;
                }

                if (chmAddr == SysDefs.ADDR_CHAMBER2)
                {
                    _workChamber2Tmr.Stop();
                    _bWorkChamber2 = false;

                    btnStartSelectedChamber2.BackColor = Color.LightGray;
                    btnStartSelectedChamber2.ForeColor = Color.Black;
                }

                if(_eqmtType == EqmtType.Temi)
                {
                    //------------------------------------------------------------------//
                    // Write Chamber Temi #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(chmAddr, 101, 4);
                }
                else
                {
                    //------------------------------------------------------------------//
                    // Write Chamber Temp #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(chmAddr, 102, 4);
                }
                
                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(recAddr, 100, 0);
            }

            else
            {
                if (MessageBox.Show("챔버 테스트를 시작 하시겠습니까?",
                    "테스트 시작?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                if (chmAddr == SysDefs.ADDR_CHAMBER1)
                {
                    btnStartSelectedChamber1.BackColor = Color.Salmon;
                    btnStartSelectedChamber1.ForeColor = Color.WhiteSmoke;

                    _doFirstRunChamber1 = false;
                    
                    _workChamber1Tmr.Start();
                    _bWorkChamber1 = true;

                    if (_eqmtType == EqmtType.Temi)
                    {
                        specTmChamber1[_chamber1WorkingIdx].Reset();
                    }
                    else
                    {
                        specTpChamber1[_chamber1WorkingIdx].Reset();
                    }

                    _chamber1TestList.Clear();
                    _chamber1TestList.Add(_chamber1WorkingIdx);
                }

                if (chmAddr == SysDefs.ADDR_CHAMBER2)
                {
                    btnStartSelectedChamber2.BackColor = Color.Salmon;
                    btnStartSelectedChamber2.ForeColor = Color.WhiteSmoke;

                    _doFirstRunChamber2 = false;

                    _workChamber2Tmr.Start();
                    _bWorkChamber2 = true;

                    if (_eqmtType == EqmtType.Temi)
                    {
                        specTmChamber2[_chamber2WorkingIdx].Reset();
                    }
                    else
                    {
                        specTpChamber2[_chamber2WorkingIdx].Reset();
                    }

                    _chamber2TestList.Clear();
                    _chamber2TestList.Add(_chamber2WorkingIdx);
                }
            }
        }

        private void btnStartChillerTest_Click(object sender, EventArgs e)
        {

            bool bWork = false;
            int addr = 0;
            
            if (sender == this.btnStartSelectedChiller1)
            {
                bWork = _workChiller1Tmr.Enabled;
                addr = SysDefs.ADDR_CHILLER1;
            }

            else if (sender == btnStartSelectedChiller2)
            {
                bWork = _workChiller2Tmr.Enabled;
                addr = SysDefs.ADDR_CHILLER2;
            }

            if (bWork)
            {
                if (MessageBox.Show("칠러 테스트를 중지 하시겠습니까?",
                                    "장비 정지?",
                                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                if(addr == SysDefs.ADDR_CHILLER1)
                {
                    _bWorkChiller1 = false;
                    _workChiller1Tmr.Stop();

                    btnStartSelectedChiller1.BackColor = Color.LightGray;
                    btnStartSelectedChiller1.ForeColor = Color.Black;
                }

                if (addr == SysDefs.ADDR_CHILLER2)
                {
                    _bWorkChiller2 = false;
                    _workChiller2Tmr.Stop();

                    btnStartSelectedChiller2.BackColor = Color.LightGray;
                    btnStartSelectedChiller2.ForeColor = Color.Black;
                }

                //------------------------------------------------------------------//
                // Write Chiller #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(addr, 102, 4);

                //------------------------------------------------------------------//
                // 유량 Stop
                //------------------------------------------------------------------//
                Comm.Write(addr, 2831, 1);
            }

            else
            {
                if (MessageBox.Show("칠러 테스트 운전을 시작 하시겠습니까?",
                    "테스트 시작?",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                if (addr == SysDefs.ADDR_CHILLER1)
                {
                    _doFirstRunChiller1 = false;

                    _workChiller1Tmr.Start();
                    _bWorkChiller1 = true;

                    specChiller1[_chiller1WorkingIdx].Reset();

                    _chiller1TestList.Clear();
                    _chiller1TestList.Add(_chiller1WorkingIdx);

                    btnStartSelectedChiller1.BackColor = Color.Salmon;
                    btnStartSelectedChiller1.ForeColor = Color.WhiteSmoke;
                }

                if (addr == SysDefs.ADDR_CHILLER2)
                {
                    _doFirstRunChiller2 = false;

                    _workChiller2Tmr.Start();
                    _bWorkChiller2 = true;

                    specChiller2[_chiller2WorkingIdx].Reset();

                    _chiller2TestList.Clear();
                    _chiller2TestList.Add(_chiller2WorkingIdx);

                    btnStartSelectedChiller2.BackColor = Color.Salmon;
                    btnStartSelectedChiller2.ForeColor = Color.WhiteSmoke;
                }
            }
        }

        private void DoTmChamberTest(int addr, TSpecTmChamber spcChamber)
        {
            if (spcChamber.workingSts == WorkingSts.Begin)
            {
                PerformTmChamberTestBegin(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.TouchSpCheck)
            {
                PerformTmChamberTestTouch(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Waiting)
            {
                PerformTmChamberTestWait(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Testing || spcChamber.workingSts == WorkingSts.Holding)
            {
                PerformTmChamberDoTest(addr, spcChamber);
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

                if (spcChamber.bUseHDisp)
                {
                    if (spcChamber.resCtrHMax > (spcChamber.hsp + spcChamber.jugHDisp) ||
                        spcChamber.resCtrHMin < (spcChamber.hsp - spcChamber.jugHDisp))
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
                // Success check : Humi Over
                //------------------------------------------------------------------//
                if (spcChamber.bUseHOver)
                {
                    if (spcChamber.resHOver > (spcChamber.hsp + spcChamber.jugHOver))
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                if(spcChamber.resStableTm > (short)(spcChamber.waitTm)){

                    if(spcChamber.waitTm > 0)
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }
                
                spcChamber.workEndTm = DateTime.Now;
                GenReport();
            }

            if( addr == SysDefs.ADDR_CHAMBER1) UpdateChamberTestSheetList(lsvChmbSpc1);
            if (addr == SysDefs.ADDR_CHAMBER2) UpdateChamberTestSheetList(lsvChmbSpc2);
        }

        private void DoTpChamberTest(int addr, TSpecTpChamber spcChamber)
        {
            if (spcChamber.workingSts == WorkingSts.Begin)
            {
                PerformTpChamberTestBegin(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.TouchSpCheck)
            {
                PerformTpChamberTestTouch(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Waiting)
            {
                PerformTpChamberTestWait(addr, spcChamber);
            }

            else if (spcChamber.workingSts == WorkingSts.Testing || spcChamber.workingSts == WorkingSts.Holding)
            {
                PerformTpChamberDoTest(addr, spcChamber);
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

                if (spcChamber.resStableTm > (short)(spcChamber.waitTm))
                {

                    if (spcChamber.waitTm > 0)
                    {
                        spcChamber.result = WorkingRes.Fail;
                    }
                }

                spcChamber.workEndTm = DateTime.Now;
                GenReport();
            }

            if (addr == SysDefs.ADDR_CHAMBER1) UpdateChamberTestSheetList(lsvChmbSpc1);
            if (addr == SysDefs.ADDR_CHAMBER2) UpdateChamberTestSheetList(lsvChmbSpc2);
        }

        //--------------------------------------------------------------------------//
        // TESTING CHAMBER #1
        //--------------------------------------------------------------------------//
        private void OnWorkChamberTmrEvent(Object src, EventArgs e)
        {
            System.Windows.Forms.Timer _src = (System.Windows.Forms.Timer)src;

            //------------------------------------------------------------------//
            // Do Chamber1 Test & Check Chamber Test Finish.. 
            //------------------------------------------------------------------//
            if (_src == _workChamber1Tmr)
            {
                // Temi
                if (_eqmtType == EqmtType.Temi)
                {
                    TSpecTmChamber spcChamber = specTmChamber1[_chamber1WorkingIdx];

                    if (spcChamber.result == WorkingRes.NotDef)
                    {
                        DoTmChamberTest(SysDefs.ADDR_CHAMBER1, spcChamber);
                    }
                    else
                    {
                        if (_chamber1TestList.Count > 0)
                        {
                            _chamber1WorkingIdx = _chamber1TestList[0];
                            _chamber1TestList.RemoveAt(0);
                        }
                        else
                        {
                            //------------------------------------------------------------------//
                            // All Chamber Test are Finished.
                            // Write Chamber Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_CHAMBER1, 101, 4);

                            //------------------------------------------------------------------//
                            // Write Recorder #1 Record Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                            _workChamber1Tmr.Stop();
                            _bWorkChamber1 = false;
                        }
                    }
                }
                // Temp
                else
                {
                    TSpecTpChamber spcChamber = specTpChamber1[_chamber1WorkingIdx];

                    if (spcChamber.result == WorkingRes.NotDef)
                    {
                        DoTpChamberTest(SysDefs.ADDR_CHAMBER1, spcChamber);
                    }
                    else
                    {
                        if (_chamber1TestList.Count > 0)
                        {
                            _chamber1WorkingIdx = _chamber1TestList[0];
                            _chamber1TestList.RemoveAt(0);
                        }
                        else
                        {
                            //------------------------------------------------------------------//
                            // All Chamber Test are Finished.
                            // Write Chamber Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_CHAMBER1, 102, 4);

                            //------------------------------------------------------------------//
                            // Write Recorder #1 Record Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                            _workChamber1Tmr.Stop();
                            _bWorkChamber1 = false;
                        }
                    }
                }
            }

            //------------------------------------------------------------------//
            // Do Chamber2 Test & Check Chamber Test Finish.. 
            //------------------------------------------------------------------//
            else if (_src == _workChamber2Tmr)
            {
                // Temi
                if (_eqmtType == EqmtType.Temi)
                {
                    TSpecTmChamber spcChamber = specTmChamber2[_chamber2WorkingIdx];
                    if (spcChamber.result == WorkingRes.NotDef)
                    {
                        DoTmChamberTest(SysDefs.ADDR_CHAMBER2, spcChamber);
                    }
                    else
                    {
                        if (_chamber2TestList.Count > 0)
                        {
                            _chamber2WorkingIdx = _chamber2TestList[0];
                            _chamber2TestList.RemoveAt(0);
                        }
                        else
                        {
                            //------------------------------------------------------------------//
                            // All Chamber Test are Finished.
                            // Write Chamber Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_CHAMBER2, 101, 4);

                            //------------------------------------------------------------------//
                            // Write Recorder #1 Record Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                            _workChamber2Tmr.Stop();
                            _bWorkChamber2 = false;
                        }
                    }
                }
                // Temp
                else
                {
                    TSpecTpChamber spcChamber = specTpChamber2[_chamber2WorkingIdx];
                    if (spcChamber.result == WorkingRes.NotDef)
                    {
                        DoTpChamberTest(SysDefs.ADDR_CHAMBER2, spcChamber);
                    }
                    else
                    {
                        if (_chamber2TestList.Count > 0)
                        {
                            _chamber2WorkingIdx = _chamber2TestList[0];
                            _chamber2TestList.RemoveAt(0);
                        }
                        else
                        {
                            //------------------------------------------------------------------//
                            // All Chamber Test are Finished.
                            // Write Chamber Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_CHAMBER2, 102, 4);

                            //------------------------------------------------------------------//
                            // Write Recorder #1 Record Stop
                            //------------------------------------------------------------------//
                            Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                            _workChamber2Tmr.Stop();
                            _bWorkChamber2 = false;
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------//
        // TESTING TEMI CHILLER #1
        //--------------------------------------------------------------------------//
        private void OnWorkChillerTmrEvent(Object src, EventArgs e)
        {
            System.Windows.Forms.Timer _src = (System.Windows.Forms.Timer)src;

            //------------------------------------------------------------------//
            // Do Chiller1 Test & Check Chiller Test Finish.. 
            //------------------------------------------------------------------//
            if (_src == _workChiller1Tmr)
            {
                TSpecChiller spcChiller = specChiller1[_chiller1WorkingIdx];
                if (spcChiller.result == WorkingRes.NotDef)
                {
                    DoChillerTest(SysDefs.ADDR_CHILLER1, spcChiller);
                }
                else
                {
                    if (_chiller1TestList.Count > 0)
                    {
                        _chiller1WorkingIdx = _chiller1TestList[0];
                        _chiller1TestList.RemoveAt(0);
                    }
                    else
                    {
                        //------------------------------------------------------------------//
                        // All Chamber Test are Finished.
                        // Write Chiller Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHILLER1, 102, 4);

                        //--------------------------------------------------//
                        // Subch 11 Stop(1) (유량)
                        //--------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHILLER1, 2831, 1);

                        _workChiller1Tmr.Stop();
                        _bWorkChiller1 = false;
                    }
                }
            }

            else if (_src == _workChiller2Tmr)
            {
                TSpecChiller spcChiller = specChiller2[_chiller2WorkingIdx];
                if (spcChiller.result == WorkingRes.NotDef)
                {
                    DoChillerTest(SysDefs.ADDR_CHILLER2, spcChiller);
                }
                else
                {
                    if (_chiller2TestList.Count > 0)
                    {
                        _chiller2WorkingIdx = _chiller2TestList[0];
                        _chiller2TestList.RemoveAt(0);
                    }
                    else
                    {
                        //------------------------------------------------------------------//
                        // All Chamber Test are Finished.
                        // Write Chiller Stop
                        //------------------------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHILLER2, 102, 4);

                        //--------------------------------------------------//
                        // Subch 11 Stop(1) (유량)
                        //--------------------------------------------------//
                        Comm.Write(SysDefs.ADDR_CHILLER2, 2831, 1);

                        _workChiller2Tmr.Stop();
                        _bWorkChiller2 = false;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------//
        // 1. TEMI Chamber Test Begin
        //--------------------------------------------------------------------------//
        private void PerformTmChamberTestBegin(int addr, TSpecTmChamber spc)
        {
            short tpv = 0;
            short hpv = 0;
            
            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
                hpv = _chamber1.hpv;
            }

            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
                hpv = _chamber2.hpv;
            }

            if (tpv < spc.tsp)
            {
                spc.tempSlop = Slop.Up;
            }
            else
            {
                spc.tempSlop = Slop.Dn;
            }

            if (hpv < spc.hsp)
            {
                spc.humiSlop = Slop.Up;
            }
            else
            {
                spc.humiSlop = Slop.Dn;
            }

            // Write Target Temp, Humi SP
            Comm.Write(addr, 102, spc.tsp);
            Comm.Write(addr, 103, spc.hsp);

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                if(_doFirstRunChamber1 == false)
                {
                    // Write Chamber RUN
                    if ((_chamber1.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 101, 1);
                        _doFirstRunChamber1 = true;
                    }
                }

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Start
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 1);
            }

            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                if (_doFirstRunChamber2 == false)
                {
                    //Write Chamber RUN
                    if ((_chamber2.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 101, 1);
                        _doFirstRunChamber2 = true;
                    }
                }

                //------------------------------------------------------------------//
                // Write Recorder #2 Record Start
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 1);
            }

            spc.startPv = tpv;
            spc.workingSts = WorkingSts.TouchSpCheck;
            spc.workStartTm = DateTime.Now;

            spc.bTouchTemp = false;
            spc.bTouchHumi = false;

            spc.resCtrTMin = SysDefs.NOT_DEFVAL;
            spc.resCtrTMax = SysDefs.NOT_DEFVAL;

            spc.resCtrHMin = SysDefs.NOT_DEFVAL;
            spc.resCtrHMax = SysDefs.NOT_DEFVAL;

            spc.resCtrRamp = SysDefs.NOT_DEFVAL;

            spc.resUnifMin = SysDefs.NOT_DEFVAL;
            spc.resUnifMax = SysDefs.NOT_DEFVAL;

            spc.resTOver = SysDefs.NOT_DEFVAL;
            spc.resHOver = SysDefs.NOT_DEFVAL;
            spc.resROver = SysDefs.NOT_DEFVAL;
            spc.resStableTm = SysDefs.NOT_DEFVAL;
        }

        //--------------------------------------------------------------------------//
        // 2. TEMI Chamber : Touch Target SP Check..
        //--------------------------------------------------------------------------//
        private void PerformTmChamberTestTouch(int addr, TSpecTmChamber spc)
        {
            short tpv = 0;
            short hpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
                hpv = _chamber1.hpv;
            }
            else if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
                hpv = _chamber2.hpv;
            }

            if (spc.hsp == 0) spc.bTouchHumi = true;

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

                if (spc.bTouchTemp == true)
                {
                    TimeSpan span = DateTime.Now.Subtract(spc.workStartTm);
                    spc.resCtrRamp = (short)(Math.Abs(spc.tsp - spc.startPv) / (span.TotalMinutes + 1));
                }
            }
                        
            //--------------------------------------------------------//
            // Humi touch check.
            //--------------------------------------------------------//
            if (spc.bTouchHumi == false)
            {
                if (spc.humiSlop == Slop.Up)
                {
                    if (hpv >= spc.hsp) spc.bTouchHumi = true;
                }
                else
                {
                    if (hpv <= spc.hsp) spc.bTouchHumi = true;
                }
            }

            //--------------------------------------------------------//
            // Temp & Humi touched to SP
            //--------------------------------------------------------//
            if (spc.bTouchTemp == true && spc.bTouchHumi == true)
            {
                spc.workingSts = WorkingSts.Waiting;
                spc.workStartTm = DateTime.Now;
                spc.stableTm = DateTime.Now;

                return;
            }

            // Wait End check.
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= 60)
            {
                spc.workingSts = WorkingSts.End;
            }
        }

        //--------------------------------------------------------------------------//
        // 3. TEMI Chamber : Test Wating..
        //--------------------------------------------------------------------------//
        private void PerformTmChamberTestWait(int addr, TSpecTmChamber spc)
        {
            //--------------------------------------------------------------------------//
            // Get Hunting max value.
            //--------------------------------------------------------------------------//
            short tpv = 0;
            short hpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
                hpv = _chamber1.hpv;
            }
            else if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
                hpv = _chamber2.hpv;
            }

            if (spc.resTOver == SysDefs.NOT_DEFVAL)
            {
                spc.resTOver = tpv;
            }

            if (spc.resHOver == SysDefs.NOT_DEFVAL)
            {
                spc.resHOver = hpv;
            }

            if (spc.tempSlop == Slop.Up)
            {
                if (tpv >= spc.resTOver) spc.resTOver = tpv;

                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    short val = 0;
                    if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                    if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                    if (spc.resROver == SysDefs.NOT_DEFVAL)
                    {
                        spc.resROver = val;
                    }
                    if (val >= spc.resROver) spc.resROver = val;
                }
            }
            else
            {
                if (tpv <= spc.resTOver) spc.resTOver = tpv;

                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    short val = 0;
                    if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                    if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                    if (spc.resROver == SysDefs.NOT_DEFVAL)
                    {
                        spc.resROver = val;
                    }
                    if (val <= spc.resROver) spc.resROver = val;
                }
            }

            if (spc.humiSlop == Slop.Up)
            {
                if (hpv >= spc.resHOver) spc.resHOver = hpv;
            }
            else
            {
                if (hpv <= spc.resHOver) spc.resHOver = hpv;
            }

            // Humi Wait End check.
            if (spc.hsp != 0)
            {
                if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.waitTm)
                {
                    spc.workingSts = WorkingSts.Testing;
                    spc.workStartTm = DateTime.Now;
                    spc.resStableTm = (short)spc.waitTm;
                }
            }

            else
            {
                //--------------------------------------------------------------------------//
                // Get Stable time.
                //--------------------------------------------------------------------------//
                int hlmt = (spc.tsp + spc.jugUnif);
                int llmt = (spc.tsp - spc.jugUnif);

                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    short val = 0;
                    if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                    if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

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
                spc.workingSts = WorkingSts.Testing;
                spc.workStartTm = DateTime.Now;
            }
        }

        //--------------------------------------------------------------------------//
        // 4. TEMI Chamber : Testing...
        //--------------------------------------------------------------------------//
        private void PerformTmChamberDoTest(int addr, TSpecTmChamber spc)
        {
            //------------------------------------------------//
            // Chamber1 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                if (_bChamber1Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;
                    
                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            //------------------------------------------------//
            // Chamber2 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                if (_bChamber2Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;

                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            short tpv = 0;
            short hpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
                hpv = _chamber1.hpv;
            }
            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
                hpv = _chamber2.hpv;
            }

            if ((spc.testTm - DateTime.Now.Subtract(spc.workStartTm).TotalMinutes) <= 2)
            {
                if (spc.resCtrTMax == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrTMax = tpv;
                }

                if (spc.resCtrTMin == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrTMin = tpv;
                }

                if (spc.resCtrHMax == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrHMax = hpv;
                }

                if (spc.resCtrHMin == SysDefs.NOT_DEFVAL)
                {
                    spc.resCtrHMin = hpv;
                }

                if (tpv >= spc.resCtrTMax) spc.resCtrTMax = tpv;
                if (tpv <= spc.resCtrTMin) spc.resCtrTMin = tpv;
                if (hpv >= spc.resCtrHMax) spc.resCtrHMax = hpv;
                if (hpv <= spc.resCtrHMin) spc.resCtrHMin = hpv;
            }
            
            //--------------------------------------------------------------------------//
            // Record pv
            //--------------------------------------------------------------------------//
            for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
            {
                short val = 0;
                if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                spc.AddRecData(ch, val);
            }

            //--------------------------------------------------------------------------//
            // Test End check.
            // Get Uniformity MIN/MAX from record list.
            //--------------------------------------------------------------------------//
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.testTm)
            {
                short[] avg = new short[9];
                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    avg[ch] = 0;
                    short sum = 0;
                    for (int i = 0; i < spc.resRec[ch].Capacity; i++)
                    {
                        sum += spc.resRec[ch][i];
                    }
                    avg[ch] = (short)(sum / spc.resRec[ch].Capacity);

                    if (spc.resUnifMin == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMin = avg[ch];
                    }

                    if (spc.resUnifMax == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMax = avg[ch];
                    }

                    if (avg[ch] <= spc.resUnifMin) spc.resUnifMin = avg[ch];
                    if (avg[ch] >= spc.resUnifMax) spc.resUnifMax = avg[ch];
                }

                spc.workingSts = WorkingSts.End;
            }
        }

        //--------------------------------------------------------------------------//
        // 1. TEMP Chamber Test Begin
        //--------------------------------------------------------------------------//
        private void PerformTpChamberTestBegin(int addr, TSpecTpChamber spc)
        {
            short tpv = 0;
            
            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
            }

            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
            }

            if (tpv < spc.tsp)
            {
                spc.tempSlop = Slop.Up;
            }
            else
            {
                spc.tempSlop = Slop.Dn;
            }

            // Write Target Temp
            Comm.Write(addr, 104, spc.tsp);

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                if (_doFirstRunChamber1 == false)
                {
                    // Write Chamber RUN
                    if ((_chamber1.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 102, 1);
                        _doFirstRunChamber1 = true;
                    }
                }

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Start
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 1);
            }

            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                if (_doFirstRunChamber2 == false)
                {
                    // Write Chamber RUN
                    if ((_chamber2.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 102, 1);
                        _doFirstRunChamber2= true;
                    }
                }

                //------------------------------------------------------------------//
                // Write Recorder #2 Record Start
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 1);
            }

            spc.startPv = tpv;
            spc.workingSts = WorkingSts.TouchSpCheck;
            spc.workStartTm = DateTime.Now;

            spc.bTouchTemp = false;

            spc.resCtrTMin = SysDefs.NOT_DEFVAL;
            spc.resCtrTMax = SysDefs.NOT_DEFVAL;
            spc.resCtrRamp = SysDefs.NOT_DEFVAL;

            spc.resUnifMin = SysDefs.NOT_DEFVAL;
            spc.resUnifMax = SysDefs.NOT_DEFVAL;

            spc.resTOver = SysDefs.NOT_DEFVAL;
            spc.resROver = SysDefs.NOT_DEFVAL;
            spc.resStableTm = SysDefs.NOT_DEFVAL;
        }

        //--------------------------------------------------------------------------//
        // 2. TEMP Chamber : Touch Target SP Check..
        //--------------------------------------------------------------------------//
        private void PerformTpChamberTestTouch(int addr, TSpecTpChamber spc)
        {
            short tpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
            }
            else if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
            }

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

                spc.workingSts = WorkingSts.Waiting;
                spc.workStartTm = DateTime.Now;
                spc.stableTm = DateTime.Now;

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
        private void PerformTpChamberTestWait(int addr, TSpecTpChamber spc)
        {
            //--------------------------------------------------------------------------//
            // Get Hunting max value.
            //--------------------------------------------------------------------------//
            short tpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
            }
            else if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
            }

            if (spc.resTOver == SysDefs.NOT_DEFVAL)
            {
                spc.resTOver = tpv;
            }

            if (spc.tempSlop == Slop.Up)
            {
                if (tpv >= spc.resTOver) spc.resTOver = tpv;

                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    short val = 0;
                    if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                    if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                    if (spc.resROver == SysDefs.NOT_DEFVAL)
                    {
                        spc.resROver = val;
                    }
                    if (val >= spc.resROver) spc.resROver = val;
                }
            }
            else
            {
                if (tpv <= spc.resTOver) spc.resTOver = tpv;

                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    short val = 0;
                    if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                    if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

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
            int hlmt = (spc.tsp + spc.jugUnif);
            int llmt = (spc.tsp - spc.jugUnif);

            for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
            {
                short val = 0;
                if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                if (val > hlmt || val < llmt)
                {
                    if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.waitTm)
                    {
                        spc.resStableTm = (short)(spc.waitTm + 1);
                        //spc.workingSts = WorkingSts.End;
                        spc.workingSts = WorkingSts.Testing;
                        spc.workStartTm = DateTime.Now;
                    }
                    return;
                }
            }

            spc.resStableTm = (short)(DateTime.Now.Subtract(spc.workStartTm).TotalMinutes + 1);
            spc.workingSts = WorkingSts.Testing;
            spc.workStartTm = DateTime.Now;
        }

        //--------------------------------------------------------------------------//
        // 4. TEMP Chamber : Testing...
        //--------------------------------------------------------------------------//
        private void PerformTpChamberDoTest(int addr, TSpecTpChamber spc)
        {
            //------------------------------------------------//
            // Chamber1 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                if (_bChamber1Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;

                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            //------------------------------------------------//
            // Chamber2 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                if (_bChamber2Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;

                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            short tpv = 0;

            if (addr == SysDefs.ADDR_CHAMBER1)
            {
                tpv = _chamber1.tpv;
            }
            if (addr == SysDefs.ADDR_CHAMBER2)
            {
                tpv = _chamber2.tpv;
            }

            if ((spc.testTm - DateTime.Now.Subtract(spc.workStartTm).TotalMinutes) <= 2)
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
            for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
            {
                short val = 0;
                if (addr == SysDefs.ADDR_CHAMBER1) val = _recorder1.ch[ch];
                if (addr == SysDefs.ADDR_CHAMBER2) val = _recorder2.ch[ch];

                spc.AddRecData(ch, val);
            }

            //--------------------------------------------------------------------------//
            // Test End check.
            // Get Uniformity MIN/MAX from record list.
            //--------------------------------------------------------------------------//
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.testTm)
            {
                short[] avg = new short[9];
                for (int ch = 0; ch < SysDefs.MAX_REC_CHCNT; ch++)
                {
                    avg[ch] = 0;
                    short sum = 0;
                    for (int i = 0; i < spc.resRec[ch].Capacity; i++)
                    {
                        sum += spc.resRec[ch][i];
                    }
                    avg[ch] = (short)(sum / spc.resRec[ch].Capacity);

                    if (spc.resUnifMin == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMin = avg[ch];
                    }

                    if (spc.resUnifMax == SysDefs.NOT_DEFVAL)
                    {
                        spc.resUnifMax = avg[ch];
                    }

                    if (avg[ch] <= spc.resUnifMin) spc.resUnifMin = avg[ch];
                    if (avg[ch] >= spc.resUnifMax) spc.resUnifMax = avg[ch];
                }

                spc.workingSts = WorkingSts.End;
            }
        }

        private void DoChillerTest(int addr, TSpecChiller spc)
        {
            if (spc.workingSts == WorkingSts.Begin)
            {
                PerformChillerTestBegin(addr, spc);
            }

            else if (spc.workingSts == WorkingSts.TouchSpCheck)
            {
                PerformChillerTestTouch(addr, spc);
            }

            else if (spc.workingSts == WorkingSts.Waiting)
            {
                PerformChillerTestWait(addr, spc);
            }

            else if (spc.workingSts == WorkingSts.Testing || spc.workingSts == WorkingSts.Holding)
            {
                PerformChillerDoTest(addr, spc);
            }

            //------------------------------------------------------------------//
            // Test End.
            //------------------------------------------------------------------//
            if (spc.workingSts == WorkingSts.End)
            {
                spc.result = WorkingRes.Success;
                //------------------------------------------------------------------//
                // Success check : Disparity
                //------------------------------------------------------------------//
                if (spc.bUseTDisp)
                {
                    if (spc.resCtrTMax > (spc.tsp + spc.jugTDisp) ||
                        spc.resCtrTMin < (spc.tsp - spc.jugTDisp))
                    {
                        spc.result = WorkingRes.Fail;
                    }
                }

                if (spc.bUseSDisp)
                {
                    if (spc.resCtrSMax > (spc.ssp + spc.jugSDisp) ||
                        spc.resCtrSMin < (spc.ssp - spc.jugSDisp))
                    {
                        spc.result = WorkingRes.Fail;
                    }
                }

                //------------------------------------------------------------------//
                // Success check : Ramp 
                //------------------------------------------------------------------//
                if (spc.bUseRamp)
                {
                    if (spc.resCtrRamp < spc.jugRamp)
                    {
                        spc.result = WorkingRes.Fail;
                    }
                }

                spc.workEndTm = DateTime.Now;
                GenReport();
            }
              
            if (addr == SysDefs.ADDR_CHILLER1) UpdateChillerTestSheetList(lsvChillerSpc1);
            if (addr == SysDefs.ADDR_CHILLER2) UpdateChillerTestSheetList(lsvChillerSpc2);
        }

        //--------------------------------------------------------------------------//
        // 1. Chiller Test Begin
        //--------------------------------------------------------------------------//
        private void PerformChillerTestBegin(int addr, TSpecChiller spc)
        {
            short tpv = 0;
            short spv = 0;

            //------------------------------------------------------------------//
            // Write Target Temp SP
            //------------------------------------------------------------------//
            Comm.Write(addr, 104, spc.tsp);

            if (addr == SysDefs.ADDR_CHILLER1)
            {
                tpv = _chiller1.tpv;
                spv = _chiller1.spv;

                if(_doFirstRunChiller1 == false)
                {
                    if ((_chiller1.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 102, 1);
                        _doFirstRunChiller1 = true;
                    }
                }
            }

            if (addr == SysDefs.ADDR_CHILLER2)
            {
                tpv = _chiller2.tpv;
                spv = _chiller2.spv;

                if (_doFirstRunChiller2 == false)
                {
                    if ((_chiller2.sts & 0x0001) > 0)
                    {
                        Comm.Write(addr, 102, 1);
                        _doFirstRunChiller2 = true;
                    }
                }
            }

            if (spc.bUseSDisp)
            {
                //------------------------------------------------------------------//
                // Write Target Oil SP & Run
                //------------------------------------------------------------------//
                Comm.Write(addr, 3251, spc.ssp);
                Comm.Write(addr, 2831, 0);
            }
            
            if (tpv < spc.tsp) spc.tSlop = Slop.Up;
            else               spc.tSlop = Slop.Dn;

            if (spv < spc.ssp) spc.sSlop = Slop.Up;
            else               spc.sSlop = Slop.Dn;

            spc.startPv = tpv;
            spc.workingSts = WorkingSts.TouchSpCheck;
            spc.workStartTm = DateTime.Now;

            spc.bTouchTSP = false;
            spc.bTouchSSP = false;

            spc.resCtrTMax = SysDefs.NOT_DEFVAL;
            spc.resCtrTMin = SysDefs.NOT_DEFVAL;
            spc.resCtrSMax = SysDefs.NOT_DEFVAL;
            spc.resCtrSMin = SysDefs.NOT_DEFVAL;

            spc.resCtrRamp = SysDefs.NOT_DEFVAL;
        }

        //--------------------------------------------------------------------------//
        // 2. Chiller : Touch Target SP Check..
        //--------------------------------------------------------------------------//
        private void PerformChillerTestTouch(int addr, TSpecChiller spc)
        {
            short tpv = 0;
            short spv = 0;

            if (addr == SysDefs.ADDR_CHILLER1)
            {
                tpv = _chiller1.tpv;
                spv = _chiller1.spv;
            }

            if (addr == SysDefs.ADDR_CHILLER2)
            {
                tpv = _chiller2.tpv;
                spv = _chiller2.spv;
            }

            //if (spc.bUseTDisp == false) spc.bTouchTSP = true;
            if (spc.bUseSDisp == false) spc.bTouchSSP = true;
            
            //--------------------------------------------------------//
            // Temp touch check.
            //--------------------------------------------------------//
            if (spc.bTouchTSP == false)
            {
                if (spc.tSlop == Slop.Up)
                {
                    if (tpv >= spc.tsp) spc.bTouchTSP = true;
                }
                else
                {
                    if (tpv <= spc.tsp) spc.bTouchTSP = true;
                }

                if (spc.bTouchTSP == true)
                {
                    TimeSpan span = DateTime.Now.Subtract(spc.workStartTm);
                    spc.resCtrRamp = (short)(Math.Abs(spc.tsp - spc.startPv) / (span.TotalMinutes + 1));
                }
            }

            //--------------------------------------------------------//
            // Oil touch check.
            //--------------------------------------------------------//
            if (spc.bTouchSSP == false)
            {
                if (spc.sSlop == Slop.Up)
                {
                    if (spv >= spc.ssp) spc.bTouchSSP = true;
                }
                else
                {
                    if (spv <= spc.ssp) spc.bTouchSSP = true;
                }
            }

            if (spc.bTouchTSP == true && spc.bTouchSSP == true)
            {
                spc.workingSts = WorkingSts.Waiting;
                spc.workStartTm = DateTime.Now;
                return;
            }

            // Wait End check.
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.waitTimeOutTm)
            {
                spc.workingSts = WorkingSts.End;
            }
        }

        //--------------------------------------------------------------------------//
        // 3. Chiller : Wating..
        //--------------------------------------------------------------------------//
        private void PerformChillerTestWait(int addr, TSpecChiller spc)
        {
            // Wait End check.
             if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.waitTm)
            {
                spc.workingSts = WorkingSts.Testing;
                spc.workStartTm = DateTime.Now;
            }
        }

        //--------------------------------------------------------------------------//
        // 4. Chiller : Testing...
        //--------------------------------------------------------------------------//
        private void PerformChillerDoTest(int addr, TSpecChiller spc)
        {
            //------------------------------------------------//
            // Chiller1 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHILLER1)
            {
                if (_bChiller1Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;

                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            //------------------------------------------------//
            // Chiller2 Holding..
            //------------------------------------------------//
            if (addr == SysDefs.ADDR_CHILLER2)
            {
                if (_bChiller2Hold == true)
                {
                    spc.workingSts = WorkingSts.Holding;
                    spc.workStartTm = DateTime.Now;

                    return;
                }
                else
                {
                    spc.workingSts = WorkingSts.Testing;
                }
            }

            short tpv = 0;
            short spv = 0;

            if (addr == SysDefs.ADDR_CHILLER1)
            {
                tpv = _chiller1.tpv;
                spv = _chiller1.spv;
            }

            if (addr == SysDefs.ADDR_CHILLER2)
            {
                tpv = _chiller2.tpv;
                spv = _chiller2.spv;
            }

            if (spc.resCtrTMax == SysDefs.NOT_DEFVAL)
            {
                spc.resCtrTMax = tpv;
            }

            if (spc.resCtrTMin == SysDefs.NOT_DEFVAL)
            {
                spc.resCtrTMin = tpv;
            }

            if (spc.resCtrSMax == SysDefs.NOT_DEFVAL)
            {
                spc.resCtrSMax = spv;
            }

            if (spc.resCtrSMin == SysDefs.NOT_DEFVAL)
            {
                spc.resCtrSMin = spv;
            }

            if (tpv >= spc.resCtrTMax) spc.resCtrTMax = tpv;
            if (tpv <= spc.resCtrTMin) spc.resCtrTMin = tpv;
            if (spv >= spc.resCtrSMax) spc.resCtrSMax = spv;
            if (spv <= spc.resCtrSMin) spc.resCtrSMin = spv;

            //--------------------------------------------------------------------------//
            // Test End check.
            //--------------------------------------------------------------------------//
            if (DateTime.Now.Subtract(spc.workStartTm).TotalMinutes >= spc.testTm)
            {
                if (spc.bUseSDisp)
                {
                    //------------------------------------------------------------------//
                    // Write Target Oil Stop
                    //------------------------------------------------------------------//
                    Comm.Write(addr, 2831, 1);
                }

                spc.workingSts = WorkingSts.End;
            }
        }

        public void onUpdateCtrlSts(int addr, string strSts)
        {
            //--------------------------------------------------------------------------//
            // Check OnLine status.
            //--------------------------------------------------------------------------//
            if (strSts.IndexOf("OK") < 0)
            {
                if (addr == SysDefs.ADDR_CHAMBER1) _chamber1.bOnLine = false;
                if (addr == SysDefs.ADDR_CHAMBER2) _chamber2.bOnLine = false;

                if (addr == SysDefs.ADDR_CHILLER1) _chiller1.bOnLine = false;
                if (addr == SysDefs.ADDR_CHILLER2) _chiller2.bOnLine = false;

                if (addr == SysDefs.ADDR_RECORDER1) _recorder1.bOnLine = false;
                if (addr == SysDefs.ADDR_RECORDER2) _recorder2.bOnLine = false;
            }
            else
            {
                if (addr == SysDefs.ADDR_CHAMBER1) _chamber1.bOnLine = true;
                if (addr == SysDefs.ADDR_CHAMBER2) _chamber2.bOnLine = true;

                if (addr == SysDefs.ADDR_CHILLER1) _chiller1.bOnLine = true;
                if (addr == SysDefs.ADDR_CHILLER2) _chiller2.bOnLine = true;

                if (addr == SysDefs.ADDR_RECORDER1) _recorder1.bOnLine = true;
                if (addr == SysDefs.ADDR_RECORDER2) _recorder2.bOnLine = true;
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
            if (addr == SysDefs.ADDR_CHAMBER1 || addr == SysDefs.ADDR_CHAMBER2)
            {
                if(_eqmtType == EqmtType.Temi)
                {
                    if (addr == SysDefs.ADDR_CHAMBER1)
                    {
                        if (tmp.Length < 8)
                        {
                            _chamber1.bOnLine = false;
                            return;
                        }

                        _chamber1.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.tsp = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.hpv = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.hsp = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.sts = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    }

                    else if (addr == SysDefs.ADDR_CHAMBER2)
                    {
                        if (tmp.Length < 8)
                        {
                            _chamber2.bOnLine = false;
                            return;
                        }

                        _chamber2.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.tsp = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.hpv = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.hsp = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.sts = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    }
                }
                else
                {
                    if (addr == SysDefs.ADDR_CHAMBER1)
                    {
                        if (tmp.Length < 8)
                        {
                            _chamber1.bOnLine = false;
                            return;
                        }

                        _chamber1.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.tsp = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                        _chamber1.sts = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    }

                    else if (addr == SysDefs.ADDR_CHAMBER2)
                    {
                        if (tmp.Length < 8)
                        {
                            _chamber2.bOnLine = false;
                            return;
                        }

                        _chamber2.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.tsp = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                        _chamber2.sts = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    }
                }
            }

            //--------------------------------------------------------------------------//
            // Read Chiller#1 Status (NPV, NSP, NOW_STS)
            //--------------------------------------------------------------------------//
            else if (addr == SysDefs.ADDR_CHILLER1 || addr == SysDefs.ADDR_CHILLER2)
            {
                if (addr == SysDefs.ADDR_CHILLER1)
                {
                    if (tmp.Length < 6)
                    {
                        _chiller1.bOnLine = false;
                        return;
                    }

                    _chiller1.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _chiller1.tsp = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _chiller1.sts = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _chiller1.spv = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                }

                else if (addr == SysDefs.ADDR_CHILLER2)
                {
                    if (tmp.Length < 6)
                    {
                        _chiller2.bOnLine = false;
                        return;
                    }

                    _chiller2.tpv = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _chiller2.tsp = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _chiller2.sts = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _chiller2.spv = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                }
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
                        _recorder1.bOnLine = false;
                        return;
                    }

                    _recorder1.ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH5] = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH6] = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH7] = Convert.ToInt16(Int16.Parse(tmp[8], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH8] = Convert.ToInt16(Int16.Parse(tmp[9], System.Globalization.NumberStyles.HexNumber));
                    _recorder1.ch[SysDefs.CH9] = Convert.ToInt16(Int16.Parse(tmp[10], System.Globalization.NumberStyles.HexNumber));
                }

                else if (addr == SysDefs.ADDR_RECORDER2)
                {
                    if (tmp.Length < 11)
                    {
                        _recorder2.bOnLine = false;
                        return;
                    }

                    _recorder2.ch[SysDefs.CH1] = Convert.ToInt16(Int16.Parse(tmp[2], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH2] = Convert.ToInt16(Int16.Parse(tmp[3], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH3] = Convert.ToInt16(Int16.Parse(tmp[4], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH4] = Convert.ToInt16(Int16.Parse(tmp[5], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH5] = Convert.ToInt16(Int16.Parse(tmp[6], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH6] = Convert.ToInt16(Int16.Parse(tmp[7], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH7] = Convert.ToInt16(Int16.Parse(tmp[8], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH8] = Convert.ToInt16(Int16.Parse(tmp[9], System.Globalization.NumberStyles.HexNumber));
                    _recorder2.ch[SysDefs.CH9] = Convert.ToInt16(Int16.Parse(tmp[10], System.Globalization.NumberStyles.HexNumber));
                }
            }

            //--------------------------------------------------------------------------//
            // Update List controls Room1
            //--------------------------------------------------------------------------//
            lblChamber1TPv.Text = "온도:" + SysDefs.DotString(_chamber1.tpv, 1) + " ℃";
            lblChamber1HPv.Text = "습도:" + SysDefs.DotString(_chamber1.hpv, 1) + " %";

            lblChiller1TPv.Text = "온도:" + SysDefs.DotString(_chiller1.tpv, 1) + " ℃";
            lblChiller1SPv.Text = "유량:" + SysDefs.DotString(_chiller1.spv, 2) + " lpm";

            lblRec1Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder1.ch[0], 1) + " ℃";
            lblRec1Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder1.ch[1], 1) + " ℃";
            lblRec1Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder1.ch[2], 1) + " ℃";
            lblRec1Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder1.ch[3], 1) + " ℃";
            lblRec1Ch5.Text = "[Ch5]  " + SysDefs.DotString(_recorder1.ch[4], 1) + " ℃";
            lblRec1Ch6.Text = "[Ch6]  " + SysDefs.DotString(_recorder1.ch[5], 1) + " ℃";
            lblRec1Ch7.Text = "[Ch7]  " + SysDefs.DotString(_recorder1.ch[6], 1) + " ℃";
            lblRec1Ch8.Text = "[Ch8]  " + SysDefs.DotString(_recorder1.ch[7], 1) + " ℃";
            lblRec1Ch9.Text = "[Ch9]  " + SysDefs.DotString(_recorder1.ch[8], 1) + " ℃";

            //--------------------------------------------------------------------------//
            // Update List controls Room2
            //--------------------------------------------------------------------------//
            lblChamber2TPv.Text = "온도:" + SysDefs.DotString(_chamber2.tpv, 1) + " ℃";
            lblChamber2HPv.Text = "습도:" + SysDefs.DotString(_chamber2.hpv, 1) + " %";

            lblChiller2TPv.Text = "온도:" + SysDefs.DotString(_chiller2.tpv, 1) + " ℃";
            lblChiller2SPv.Text = "유량:" + SysDefs.DotString(_chiller2.spv, 2) + " lpm";

            lblRec2Ch1.Text = "[Ch1]  " + SysDefs.DotString(_recorder2.ch[0], 1) + " ℃";
            lblRec2Ch2.Text = "[Ch2]  " + SysDefs.DotString(_recorder2.ch[1], 1) + " ℃";
            lblRec2Ch3.Text = "[Ch3]  " + SysDefs.DotString(_recorder2.ch[2], 1) + " ℃";
            lblRec2Ch4.Text = "[Ch4]  " + SysDefs.DotString(_recorder2.ch[3], 1) + " ℃";
            lblRec2Ch5.Text = "[Ch5]  " + SysDefs.DotString(_recorder2.ch[4], 1) + " ℃";
            lblRec2Ch6.Text = "[Ch6]  " + SysDefs.DotString(_recorder2.ch[5], 1) + " ℃";
            lblRec2Ch7.Text = "[Ch7]  " + SysDefs.DotString(_recorder2.ch[6], 1) + " ℃";
            lblRec2Ch8.Text = "[Ch8]  " + SysDefs.DotString(_recorder2.ch[7], 1) + " ℃";
            lblRec2Ch9.Text = "[Ch9]  " + SysDefs.DotString(_recorder2.ch[8], 1) + " ℃";
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

        private void WriteTmChamberResExcel(Worksheet ws, TSpecTmChamber spc, int srow)
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
            if (spc.bUseTDisp == false && spc.bUseHDisp == false)
            {
                strSP = SysDefs.DotString(spc.tsp, 1) + "℃";
            }

            else if (spc.bUseTDisp == true && spc.bUseHDisp == false)
            {
                strSP = SysDefs.DotString(spc.tsp, 1) + "℃";
            }

            else if (spc.bUseTDisp == true && spc.bUseHDisp == true)
            {
                strSP = string.Format("{0}℃/{1}%", SysDefs.DotString(spc.tsp, 1), SysDefs.DotString(spc.hsp, 1));
            }

            if (spc.result != WorkingRes.NotDef)
            {
                if (spc.bUseTOver == true) strResCtrTOvr = SysDefs.DotString(spc.resTOver, 1);
                if (spc.bUseHOver == true) strResCtrHOvr = SysDefs.DotString(spc.resHOver, 1);

                // Control Min/Max
                if (spc.bUseTDisp == true)
                {
                    strResCtrTMin = SysDefs.DotString(spc.resCtrTMin, 1);
                    strResCtrTMax = SysDefs.DotString(spc.resCtrTMax, 1);
                }

                if (spc.bUseHDisp == true)
                {
                    strResCtrHMin = SysDefs.DotString(spc.resCtrHMin, 1);
                    strResCtrHMax = SysDefs.DotString(spc.resCtrHMax, 1);
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

        private void WriteChillerResExcel(Worksheet ws, TSpecChiller spc, int srow)
        {
            Microsoft.Office.Interop.Excel.Range rngSp = ws.Rows.Cells[srow, 2];
            Microsoft.Office.Interop.Excel.Range rngTMin = ws.Rows.Cells[srow, 8];
            Microsoft.Office.Interop.Excel.Range rngTMax = ws.Rows.Cells[srow, 11];

            Microsoft.Office.Interop.Excel.Range rngSMin = ws.Rows.Cells[srow, 17];
            Microsoft.Office.Interop.Excel.Range rngSMax = ws.Rows.Cells[srow, 20];

            Microsoft.Office.Interop.Excel.Range rngRamp = ws.Rows.Cells[srow, 30];
            Microsoft.Office.Interop.Excel.Range rngRes = ws.Rows.Cells[srow, 42];

            string strSP = "-";
            string strResCtrTMin = "-";
            string strResCtrTMax = "-";
            string strResCtrSMin = "-";
            string strResCtrSMax = "-";

            string strResCtrRamp = "-";

            // Control Min/Max
            strSP = string.Format("{0}℃/{1}lpm", spc.tsp * 0.1, spc.ssp * 0.01);//SysDefs.DotString(spc.tsp, 1), SysDefs.DotString(spc.ssp, 2));


            if (spc.result != WorkingRes.NotDef)
            {

                // Control Min/Max
                if (spc.bUseTDisp == true)
                {
                    strResCtrTMin = SysDefs.DotString(spc.resCtrTMin, 1);
                    strResCtrTMax = SysDefs.DotString(spc.resCtrTMax, 1);
                }

                if (spc.bUseSDisp == true)
                {
                    strResCtrSMin = SysDefs.DotString(spc.resCtrSMin, 2);
                    strResCtrSMax = SysDefs.DotString(spc.resCtrSMax, 2);
                }

                // Control Ramp
                if (spc.bUseRamp)
                {
                    strResCtrRamp = SysDefs.DotString(spc.resCtrRamp, 1);
                }
            }

            //------------------------------------------------------------//
            // Write WorkSheet
            //------------------------------------------------------------//
            rngSp.Value = strSP;
            rngTMin.Value = strResCtrTMin;
            rngTMax.Value = strResCtrTMax;
            
            rngSMin.Value = strResCtrSMin;
            rngSMax.Value = strResCtrSMax;

            rngRamp.Value = strResCtrRamp;

            if (spc.result == WorkingRes.Success)
            {
                rngRes.Value = "GOOD";
            }
            else if (spc.result == WorkingRes.Fail)
            {
                rngRes.Value = "NG";
            }
            else
            {
                rngRes.Value = "-";
            }
        }

        private void GenReport()
        {
            string templeteFile = "";

            if(_eqmtType == EqmtType.Temi)
            {
                templeteFile = SysDefs.execPath + "\\" + SysDefs.RESULT_FOLDER_PATH + "\\Report_Tm.xlsx";
            }
            else
            {
                templeteFile = SysDefs.execPath + "\\" + SysDefs.RESULT_FOLDER_PATH + "\\Report_Tp.xlsx";
            }
            
            string tarPath = SysDefs.execPath + SysDefs.RESULT_FOLDER_PATH + "\\" + txtSerialNo.Text + ".xlsx";

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(templeteFile, ReadOnly: false, Editable: true);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets.Item[1] as Microsoft.Office.Interop.Excel.Worksheet;

            if (worksheet == null)
            {
                return;
            }

            int idx = 0;

            // Temi
            if (_eqmtType == EqmtType.Temi)
            {
                for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++)
                {
                    TSpecTmChamber spc1 = specTmChamber1[i];
                    TSpecTmChamber spc2 = specTmChamber2[i];

                    if (spc1.bReport == false)
                    {
                        continue;
                    }
                    WriteTmChamberResExcel(worksheet, spc1, 10 + idx);
                    WriteTmChamberResExcel(worksheet, spc2, 28 + idx);
                    idx++;
                }
            }
            // Temp
            else
            {
                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    TSpecTpChamber spc1 = specTpChamber1[i];
                    TSpecTpChamber spc2 = specTpChamber2[i];

                    WriteTpChamberResExcel(worksheet, spc1, 10 + idx);
                    WriteTpChamberResExcel(worksheet, spc2, 28 + idx);
                    idx++;
                }
            }

            for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
            {
                TSpecChiller spc1 = specChiller1[i];
                TSpecChiller spc2 = specChiller2[i];

                WriteChillerResExcel(worksheet, spc1, 18 + i);
                WriteChillerResExcel(worksheet, spc2, 36 + i);
            }

            Microsoft.Office.Interop.Excel.Range rngSerial = worksheet.Rows.Cells[5, 33];
            Microsoft.Office.Interop.Excel.Range rngAmbTemp = worksheet.Rows.Cells[45, 13];
            Microsoft.Office.Interop.Excel.Range rngAmbHumi = worksheet.Rows.Cells[45, 17];
            Microsoft.Office.Interop.Excel.Range rngCoolant = worksheet.Rows.Cells[46, 13];
            Microsoft.Office.Interop.Excel.Range rngApprov = worksheet.Rows.Cells[49, 13];
            Microsoft.Office.Interop.Excel.Range rngDate = worksheet.Rows.Cells[48, 13];

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

            /*
            if (MessageBox.Show("해당 결과 내용으로 결과 레포트를 생성 하시겠습니까?",
                               "Export to Excel file?",
                                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string templeteFile = SysDefs.execPath + "\\" + SysDefs.RESULT_FOLDER_PATH + "\\Report_Tm.xlsx";
                string tarPath = SysDefs.execPath + SysDefs.RESULT_FOLDER_PATH + "\\" + txtSerialNo.Text + ".xlsx";

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excel.Workbooks.Open(templeteFile, ReadOnly: false, Editable: true);
                Worksheet worksheet = workbook.Worksheets.Item[1] as Worksheet;

                if (worksheet == null)
                {
                    MessageBox.Show("엑셀 워크 시트 열기에 실패 했습니다.");
                    return;
                }

                int idx = 0;

                // Temi
                if (_eqmtType == EqmtType.Temi)
                {
                    for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++)
                    {
                        TSpecTmChamber spc1 = specTmChamber1[i];
                        TSpecTmChamber spc2 = specTmChamber2[i];

                        if (spc1.bReport == false)
                        {
                            continue;
                        }
                        WriteTmChamberResExcel(worksheet, spc1, 10 + idx);
                        WriteTmChamberResExcel(worksheet, spc2, 28 + idx);
                        idx++;
                    }
                }
                // Temp
                else
                {
                    for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                    {
                        TSpecTpChamber spc1 = specTpChamber1[i];
                        TSpecTpChamber spc2 = specTpChamber2[i];

                        WriteTpChamberResExcel(worksheet, spc1, 10 + idx);
                        WriteTpChamberResExcel(worksheet, spc2, 28 + idx);
                        idx++;
                    }
                }
                

                for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
                {
                    TSpecChiller spc1 = specChiller1[i];
                    TSpecChiller spc2 = specChiller2[i];

                    WriteChillerResExcel(worksheet, spc1, 18 + i);
                    WriteChillerResExcel(worksheet, spc2, 36 + i);
                }

                Range rngSerial = worksheet.Rows.Cells[5, 33];
                Range rngAmbTemp = worksheet.Rows.Cells[45, 13];
                Range rngAmbHumi = worksheet.Rows.Cells[45, 17];
                Range rngApprov = worksheet.Rows.Cells[49, 13];
                Range rngDate = worksheet.Rows.Cells[48, 13];

                rngSerial.Value = txtSerialNo.Text;
                rngAmbTemp.Value = txtAmbTemp.Text;
                rngAmbHumi.Value = txtAmbHumi.Text;
                rngApprov.Value = txtApprov.Text;
                rngDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

                try
                {
                    excel.Application.ActiveWorkbook.SaveAs(tarPath);
                    workbook.Close();

                    excel.Application.Quit();
                    excel.Quit();

                    FileInfo fi = new FileInfo(tarPath);
                    if (fi.Exists)
                    {
                        System.Diagnostics.Process.Start(tarPath);
                    }
                }
                catch
                {
                    MessageBox.Show("엑셀 파일 저장에 실패 했습니다. 이미 다른 프로그램에서 사용중이거나 오픈 되어 있습니다.",
                        "엑셀 파일 생성 실패");

                    object misValue = System.Reflection.Missing.Value;

                    workbook.Close(false, misValue, misValue);

                    excel.Application.Quit();
                    excel.Quit();

                    return;
                }
            }
            */
        }

        private void InitChamberTestSheetList()
        {
            ListViewNF lsvChamber;
            ListViewNF lsvChiller;

            //----------------------------------------------------------------------------//
            // Temi
            //----------------------------------------------------------------------------//
            if (_eqmtType == EqmtType.Temi)
            {
                for (int idx = 0; idx < 2; idx++){
                    if (idx == 0){
                        lsvChamber = lsvChmbSpc1;
                        lsvChiller = lsvChillerSpc1;
                    }
                    else{
                        lsvChamber = lsvChmbSpc2;
                        lsvChiller = lsvChillerSpc2;
                    }

                    lsvChamber.Columns.Clear();
                    lsvChiller.Columns.Clear();

                    lsvChamber.Columns.Add("No.");
                    lsvChamber.Columns.Add("온도(℃)");
                    lsvChamber.Columns.Add("습도(%)");
                    lsvChamber.Columns.Add("대기(분)");
                    lsvChamber.Columns.Add("테스트(분)");
                    lsvChamber.Columns.Add("제어 Min");
                    lsvChamber.Columns.Add("제어 Max");
                    lsvChamber.Columns.Add("온도 Over(℃)");
                    lsvChamber.Columns.Add("습도 Over(%)");
                    lsvChamber.Columns.Add("분포 Over(℃)");
                    lsvChamber.Columns.Add("안정화시간");
                    lsvChamber.Columns.Add("제어 Ramp");
                    lsvChamber.Columns.Add("분포도 Min");
                    lsvChamber.Columns.Add("분포도 Max");
                    lsvChamber.Columns.Add("판정결과");

                    int w = lsvChamber.Width / lsvChamber.Columns.Count;
                    int tw = 0;

                    for (int i = 0; i < lsvChamber.Columns.Count; i++){
                        if (i == 0){
                            lsvChamber.Columns[i].Width = 50;
                        }
                        else if (i == 6 || i == 7){
                            lsvChamber.Columns[i].Width = w + 20;
                        }
                        else{
                            lsvChamber.Columns[i].Width = w;
                        }
                        tw += lsvChamber.Columns[i].Width;
                    }
                    lsvChamber.Columns[lsvChamber.Columns.Count - 1].Width = w + lsvChamber.Width - tw - 5;

                    //-----------------------------------------------------------------------------//
                    // Chiller List view
                    //-----------------------------------------------------------------------------//
                    lsvChiller.Columns.Add("No.");
                    lsvChiller.Columns.Add("온도(℃)");
                    lsvChiller.Columns.Add("유량(lpm)");

                    lsvChiller.Columns.Add("대기(분)");
                    lsvChiller.Columns.Add("테스트(분)");
                    lsvChiller.Columns.Add("제어 Min/Max");
                    lsvChiller.Columns.Add("유량 Min/Max");
                    lsvChiller.Columns.Add("제어 Ramp");

                    lsvChiller.Columns.Add("온도편차(℃)");
                    lsvChiller.Columns.Add("유량편차(lpm)");
                    lsvChiller.Columns.Add("Ramp (℃/Min)");
                    lsvChiller.Columns.Add("판정결과");

                    w = lsvChiller.Width / lsvChiller.Columns.Count + 5;
                    tw = 0;

                    for (int i = 0; i < lsvChiller.Columns.Count; i++){
                        if (i == 0){
                            lsvChiller.Columns[i].Width = 50;
                        }

                        else{
                            lsvChiller.Columns[i].Width = w;
                        }
                        tw += lsvChiller.Columns[i].Width;
                    }
                    lsvChiller.Columns[lsvChiller.Columns.Count - 1].Width = w + lsvChiller.Width - tw - 5;
                }

                lsvChmbSpc1.Items.Clear();
                lsvChmbSpc2.Items.Clear();

                for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++){
                    ListViewItem itm1;
                    ListViewItem itm2;

                    itm1 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
                    itm2 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });

                    lsvChmbSpc1.Items.Add(itm1);
                    lsvChmbSpc2.Items.Add(itm2);
                }
            }

            //----------------------------------------------------------------------------//
            // Temp
            //----------------------------------------------------------------------------//
            else{
                for (int idx = 0; idx < 2; idx++){
                    if (idx == 0){
                        lsvChamber = lsvChmbSpc1;
                        lsvChiller = lsvChillerSpc1;
                    }
                    else{
                        lsvChamber = lsvChmbSpc2;
                        lsvChiller = lsvChillerSpc2;
                    }

                    lsvChamber.Columns.Clear();
                    lsvChiller.Columns.Clear();

                    lsvChamber.Columns.Add("No.");
                    lsvChamber.Columns.Add("온도(℃)");
                    lsvChamber.Columns.Add("대기(분)");
                    lsvChamber.Columns.Add("테스트(분)");
                    lsvChamber.Columns.Add("제어 Min");
                    lsvChamber.Columns.Add("제어 Max");
                    lsvChamber.Columns.Add("온도 Over(℃)");
                    lsvChamber.Columns.Add("분포 Over(℃)");
                    lsvChamber.Columns.Add("안정화시간");
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


                    //-----------------------------------------------------------------------------//
                    // Chiller List view
                    //-----------------------------------------------------------------------------//
                    lsvChiller.Columns.Add("No.");
                    lsvChiller.Columns.Add("온도(℃)");
                    lsvChiller.Columns.Add("유량(lpm)");

                    lsvChiller.Columns.Add("대기(분)");
                    lsvChiller.Columns.Add("테스트(분)");
                    lsvChiller.Columns.Add("제어 Min/Max");
                    lsvChiller.Columns.Add("유량 Min/Max");
                    lsvChiller.Columns.Add("제어 Ramp");

                    lsvChiller.Columns.Add("온도편차(℃)");
                    lsvChiller.Columns.Add("유량편차(lpm)");
                    lsvChiller.Columns.Add("Ramp (℃/Min)");
                    lsvChiller.Columns.Add("판정결과");

                    w = lsvChiller.Width / lsvChiller.Columns.Count + 5;
                    tw = 0;

                    for (int i = 0; i < lsvChiller.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            lsvChiller.Columns[i].Width = 50;
                        }

                        else
                        {
                            lsvChiller.Columns[i].Width = w;
                        }
                        tw += lsvChiller.Columns[i].Width;
                    }
                    lsvChiller.Columns[lsvChiller.Columns.Count - 1].Width = w + lsvChiller.Width - tw - 5;
                }

                lsvChmbSpc1.Items.Clear();
                lsvChmbSpc2.Items.Clear();

                for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
                {
                    ListViewItem itm1;
                    ListViewItem itm2;

                    itm1 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });
                    itm2 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "", "", "" });

                    lsvChmbSpc1.Items.Add(itm1);
                    lsvChmbSpc2.Items.Add(itm2);
                }
            }

            //----------------------------------------------------------------------------//
            // Chiller
            //----------------------------------------------------------------------------//
            lsvChillerSpc1.Items.Clear();
            lsvChillerSpc2.Items.Clear();

            for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
            {
                ListViewItem itm1;
                ListViewItem itm2;

                itm1 = new ListViewItem(new String[] {  String.Format("{0}",i+1), "", "", "", "", "", "", "", "","","",""});
                itm2 = new ListViewItem(new String[] { String.Format("{0}", i + 1), "", "", "", "", "", "", "", "", "", "", "" });

                lsvChillerSpc1.Items.Add(itm1);
                lsvChillerSpc2.Items.Add(itm2);
            }

            UpdateChamberTestSheetList(lsvChmbSpc1);
            UpdateChamberTestSheetList(lsvChmbSpc2);

            UpdateChillerTestSheetList(lsvChillerSpc1);
            UpdateChillerTestSheetList(lsvChillerSpc2);
        }

        public void UpdateTestSheet()
        {
            // Temi
            for (int i = 0; i < SysDefs.TEMI_TEST_CNT; i++)
            {
                specTmChamber1[i].tsp = Cfg.TmCfg.TSp[i];
                specTmChamber1[i].hsp = Cfg.TmCfg.HSp[i];
                specTmChamber1[i].waitTm = Cfg.TmCfg.WaitTm[i];
                specTmChamber1[i].testTm = Cfg.TmCfg.TestTm[i];

                specTmChamber1[i].jugTDisp = Cfg.TmCfg.TempDiff[i];
                specTmChamber1[i].jugHDisp = Cfg.TmCfg.HumiDiff[i];
                specTmChamber1[i].jugRamp = Cfg.TmCfg.Ramp[i];
                specTmChamber1[i].jugUnif = Cfg.TmCfg.Uniformity[i];

                specTmChamber1[i].jugTOver = Cfg.TmCfg.TOver[i];
                specTmChamber1[i].jugHOver = Cfg.TmCfg.HOver[i];

                specTmChamber1[i].bUseTDisp = Cfg.TmCfg.bUseTDiff[i];
                specTmChamber1[i].bUseHDisp = Cfg.TmCfg.bUseHDiff[i];
                specTmChamber1[i].bUseRamp  = Cfg.TmCfg.bUseRamp[i];
                specTmChamber1[i].bUseUnif  = Cfg.TmCfg.bUseUnif[i];

                specTmChamber1[i].bUseTOver = Cfg.TmCfg.bUseTOver[i];
                specTmChamber1[i].bUseHOver = Cfg.TmCfg.bUseHOver[i];

                specTmChamber1[i].bReport = Cfg.TmCfg.bReport[i];

                specTmChamber2[i].tsp = Cfg.TmCfg.TSp[i];
                specTmChamber2[i].hsp = Cfg.TmCfg.HSp[i];
                specTmChamber2[i].waitTm = Cfg.TmCfg.WaitTm[i];
                specTmChamber2[i].testTm = Cfg.TmCfg.TestTm[i];

                specTmChamber2[i].jugTDisp = Cfg.TmCfg.TempDiff[i];
                specTmChamber2[i].jugHDisp = Cfg.TmCfg.HumiDiff[i];
                specTmChamber2[i].jugRamp = Cfg.TmCfg.Ramp[i];
                specTmChamber2[i].jugUnif = Cfg.TmCfg.Uniformity[i];

                specTmChamber2[i].jugTOver = Cfg.TmCfg.TOver[i];
                specTmChamber2[i].jugHOver = Cfg.TmCfg.HOver[i];

                specTmChamber2[i].bUseTDisp = Cfg.TmCfg.bUseTDiff[i];
                specTmChamber2[i].bUseHDisp = Cfg.TmCfg.bUseHDiff[i];
                specTmChamber2[i].bUseRamp  = Cfg.TmCfg.bUseRamp[i];
                specTmChamber2[i].bUseUnif  = Cfg.TmCfg.bUseUnif[i];

                specTmChamber2[i].bUseTOver = Cfg.TmCfg.bUseTOver[i];
                specTmChamber2[i].bUseHOver = Cfg.TmCfg.bUseHOver[i];

                specTmChamber2[i].bReport = Cfg.TmCfg.bReport[i];
            }
            
            //Temp
            for (int i = 0; i < SysDefs.TEMP_TEST_CNT; i++)
            {
                specTpChamber1[i].tsp = Cfg.TpCfg.TSp[i];
                specTpChamber1[i].waitTm = Cfg.TpCfg.WaitTm[i];
                specTpChamber1[i].testTm = Cfg.TpCfg.TestTm[i];
                     
                specTpChamber1[i].jugTDisp = Cfg.TpCfg.TempDiff[i];
                specTpChamber1[i].jugRamp = Cfg.TpCfg.Ramp[i];
                specTpChamber1[i].jugUnif = Cfg.TpCfg.Uniformity[i];
                     
                specTpChamber1[i].jugTOver = Cfg.TpCfg.TOver[i];
                     
                specTpChamber1[i].bUseTDisp = Cfg.TpCfg.bUseTDiff[i];
                specTpChamber1[i].bUseRamp = Cfg.TpCfg.bUseRamp[i];
                specTpChamber1[i].bUseUnif = Cfg.TpCfg.bUseUnif[i];
                     
                specTpChamber1[i].bUseTOver = Cfg.TpCfg.bUseTOver[i];
                    
                specTpChamber2[i].tsp = Cfg.TpCfg.TSp[i];
                specTpChamber2[i].waitTm = Cfg.TpCfg.WaitTm[i];
                specTpChamber2[i].testTm = Cfg.TpCfg.TestTm[i];
                     
                specTpChamber2[i].jugTDisp = Cfg.TpCfg.TempDiff[i];
                specTpChamber2[i].jugRamp = Cfg.TpCfg.Ramp[i];
                specTpChamber2[i].jugUnif = Cfg.TpCfg.Uniformity[i];

                specTpChamber2[i].jugTOver = Cfg.TpCfg.TOver[i];
                     
                specTpChamber2[i].bUseTDisp = Cfg.TpCfg.bUseTDiff[i];
                specTpChamber2[i].bUseRamp = Cfg.TpCfg.bUseRamp[i];
                specTpChamber2[i].bUseUnif = Cfg.TpCfg.bUseUnif[i];
                     
                specTpChamber2[i].bUseTOver = Cfg.TpCfg.bUseTOver[i];
            }

            // Temi
            if(_eqmtType == EqmtType.Temi)
            {
                for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
                {
                    specChiller1[i].tsp = Cfg.TmChillerCfg.TSp[i];
                    specChiller1[i].ssp = Cfg.TmChillerCfg.SSp[i];

                    specChiller2[i].tsp = Cfg.TmChillerCfg.TSp[i];
                    specChiller2[i].ssp = Cfg.TmChillerCfg.SSp[i];

                    specChiller1[i].waitTm = Cfg.TmChillerCfg.WaitTm[i];
                    specChiller1[i].testTm = Cfg.TmChillerCfg.TestTm[i];

                    specChiller2[i].waitTm = Cfg.TmChillerCfg.WaitTm[i];
                    specChiller2[i].testTm = Cfg.TmChillerCfg.TestTm[i];

                    specChiller1[i].jugTDisp = Cfg.TmChillerCfg.TDiff[i];
                    specChiller1[i].jugSDisp = Cfg.TmChillerCfg.SDiff[i];
                    specChiller1[i].jugRamp = Cfg.TmChillerCfg.Ramp[i];

                    specChiller2[i].jugTDisp = Cfg.TmChillerCfg.TDiff[i];
                    specChiller2[i].jugSDisp = Cfg.TmChillerCfg.SDiff[i];
                    specChiller2[i].jugRamp = Cfg.TmChillerCfg.Ramp[i];

                    specChiller1[i].bUseTDisp = Cfg.TmChillerCfg.bUseTDiff[i];
                    specChiller1[i].bUseSDisp = Cfg.TmChillerCfg.bUseSDiff[i];
                    specChiller1[i].bUseRamp = Cfg.TmChillerCfg.bUseRamp[i];

                    specChiller2[i].bUseTDisp = Cfg.TmChillerCfg.bUseTDiff[i];
                    specChiller2[i].bUseSDisp = Cfg.TmChillerCfg.bUseSDiff[i];
                    specChiller2[i].bUseRamp = Cfg.TmChillerCfg.bUseRamp[i];
                }
            }
            // Temp
            else
            {
                for (int i = 0; i < SysDefs.CHILLER_TEST_CNT; i++)
                {
                    specChiller1[i].tsp = Cfg.TpChillerCfg.TSp[i];
                    specChiller1[i].ssp = Cfg.TpChillerCfg.SSp[i];

                    specChiller2[i].tsp = Cfg.TpChillerCfg.TSp[i];
                    specChiller2[i].ssp = Cfg.TpChillerCfg.SSp[i];

                    specChiller1[i].waitTm = Cfg.TpChillerCfg.WaitTm[i];
                    specChiller1[i].testTm = Cfg.TpChillerCfg.TestTm[i];

                    specChiller2[i].waitTm = Cfg.TpChillerCfg.WaitTm[i];
                    specChiller2[i].testTm = Cfg.TpChillerCfg.TestTm[i];

                    specChiller1[i].jugTDisp = Cfg.TpChillerCfg.TDiff[i];
                    specChiller1[i].jugSDisp = Cfg.TpChillerCfg.SDiff[i];
                    specChiller1[i].jugRamp = Cfg.TpChillerCfg.Ramp[i];

                    specChiller2[i].jugTDisp = Cfg.TpChillerCfg.TDiff[i];
                    specChiller2[i].jugSDisp = Cfg.TpChillerCfg.SDiff[i];
                    specChiller2[i].jugRamp = Cfg.TpChillerCfg.Ramp[i];

                    specChiller1[i].bUseTDisp = Cfg.TpChillerCfg.bUseTDiff[i];
                    specChiller1[i].bUseSDisp = Cfg.TpChillerCfg.bUseSDiff[i];
                    specChiller1[i].bUseRamp = Cfg.TpChillerCfg.bUseRamp[i];

                    specChiller2[i].bUseTDisp = Cfg.TpChillerCfg.bUseTDiff[i];
                    specChiller2[i].bUseSDisp = Cfg.TpChillerCfg.bUseSDiff[i];
                    specChiller2[i].bUseRamp = Cfg.TpChillerCfg.bUseRamp[i];
                }
            }


            UpdateChamberTestSheetList(lsvChmbSpc1);
            UpdateChamberTestSheetList(lsvChmbSpc2);

            UpdateChillerTestSheetList(lsvChillerSpc1);
            UpdateChillerTestSheetList(lsvChillerSpc2);
        }


        private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (sender == lsvChmbSpc1 || sender == lsvChmbSpc2)
            {
                if(_eqmtType == EqmtType.Temi)
                {
                    if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12 || e.ColumnIndex == 13)
                    {
                        e.Graphics.FillRectangle(Brushes.LightSalmon, e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                    }
                }
                else
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
            }
            else if (sender == lsvChillerSpc1 || sender == lsvChillerSpc2)
            {
                if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
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

            if (sender == lsvChmbSpc1) workingIdx = _chamber1WorkingIdx;
            if (sender == lsvChmbSpc2) workingIdx = _chamber2WorkingIdx;
            if (sender == lsvChillerSpc1) workingIdx = _chiller1WorkingIdx;
            if (sender == lsvChillerSpc2) workingIdx = _chiller2WorkingIdx;

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
            bool bWork = false;
            ref int workingIdx = ref _chamber1WorkingIdx;
            ListViewNF lstView = new ListViewNF();

            if (sender == lsvChmbSpc1)
            {
                bWork = _bWorkChamber1;
                workingIdx = ref _chamber1WorkingIdx;
                lstView = lsvChmbSpc1;
            }

            else if (sender == lsvChmbSpc2)
            {
                bWork = _bWorkChamber2;
                workingIdx = ref _chamber2WorkingIdx;
                lstView = lsvChmbSpc2;
            }

            else if (sender == lsvChillerSpc1)
            {
                bWork = _bWorkChiller1;
                workingIdx = ref _chiller1WorkingIdx;
                lstView = lsvChillerSpc1;
            }

            else if (sender == lsvChillerSpc2)
            {
                bWork = _bWorkChiller2;
                workingIdx = ref _chiller2WorkingIdx;
                lstView = lsvChillerSpc2;
            }

            if (bWork)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
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

        private void btnResetChamber_Click(object sender, EventArgs e)
        {
            int idx = _chamber1WorkingIdx;

            if (_eqmtType == EqmtType.Temi)
            {
                specTmChamber1[idx].Reset();
            }
            else
            {
                specTpChamber1[idx].Reset();
            }
            UpdateChamberTestSheetList(lsvChmbSpc1);
            UpdateChamberTestSheetList(lsvChmbSpc2);

            UpdateChillerTestSheetList(lsvChillerSpc1);
            UpdateChillerTestSheetList(lsvChillerSpc2);
        }

        private void btnResetChiller_Click(object sender, EventArgs e)
        {
            int idx = _chiller1WorkingIdx;

            specChiller1[idx].Reset();

            UpdateChamberTestSheetList(lsvChmbSpc1);
            UpdateChamberTestSheetList(lsvChmbSpc2);

            UpdateChillerTestSheetList(lsvChillerSpc1);
            UpdateChillerTestSheetList(lsvChillerSpc2);
        }

        private void btnAllStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("일괄 중지 하시겠습니까?",
                "일괄 정지?",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            //----------------------------------------------------//
            // Reset Room 1 Buttons.
            //----------------------------------------------------//
            btnStartRoom1.BackColor = Color.LightGray;
            btnStartRoom1.ForeColor = Color.Black;
            btnStartRoom1.Enabled = true;

            btnStartChamber1.BackColor = Color.LightGray;
            btnStartChamber1.ForeColor = Color.Black;
            btnStartChamber1.Enabled = true;

            btnStartChiller1.BackColor = Color.LightGray;
            btnStartChiller1.ForeColor = Color.Black;
            btnStartChiller1.Enabled = true;

            btnStartSelectedChamber1.BackColor = Color.LightGray;
            btnStartSelectedChamber1.ForeColor = Color.Black;
            btnStartSelectedChamber1.Enabled = true;

            btnStartSelectedChiller1.BackColor = Color.LightGray;
            btnStartSelectedChiller1.ForeColor = Color.Black;
            btnStartSelectedChamber1.Enabled = true;

            //----------------------------------------------------//
            // Reset Room 2 Buttons.
            //----------------------------------------------------//
            btnStartRoom2.BackColor = Color.LightGray;
            btnStartRoom2.ForeColor = Color.Black;
            btnStartRoom2.Enabled = true;

            btnStartChamber2.BackColor = Color.LightGray;
            btnStartChamber2.ForeColor = Color.Black;
            btnStartChamber2.Enabled = true;

            btnStartChiller2.BackColor = Color.LightGray;
            btnStartChiller2.ForeColor = Color.Black;
            btnStartChiller2.Enabled = true;

            btnStartSelectedChamber2.BackColor = Color.LightGray;
            btnStartSelectedChamber2.ForeColor = Color.Black;
            btnStartSelectedChamber2.Enabled = true;

            btnStartSelectedChiller2.BackColor = Color.LightGray;
            btnStartSelectedChiller2.ForeColor = Color.Black;
            btnStartSelectedChamber2.Enabled = true;

            if (Comm.IsRun)
            {
                if (_eqmtType == EqmtType.Temi)
                {
                    //------------------------------------------------------------------//
                    // Write Temi Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 101, 4);

                    //------------------------------------------------------------------//
                    // Write Temi Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 101, 4);
                }
                else
                {
                    //------------------------------------------------------------------//
                    // Write Temp Chamber #1 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER1, 102, 4);

                    //------------------------------------------------------------------//
                    // Write Temp Chamber #2 Stop
                    //------------------------------------------------------------------//
                    Comm.Write(SysDefs.ADDR_CHAMBER2, 102, 4);
                }

                //------------------------------------------------------------------//
                // Write Chiller #1 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 102, 4);

                //------------------------------------------------------------------//
                // Write Chiller #2 Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER2, 102, 4);

                //------------------------------------------------------------------//
                // Write Recorder #1 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER1, 100, 0);

                //------------------------------------------------------------------//
                // Write Recorder #2 Record Stop
                //------------------------------------------------------------------//
                Comm.Write(SysDefs.ADDR_RECORDER2, 100, 0);

                //--------------------------------------------------//
                // Subch 11 Stop(1) (유량)
                //--------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER1, 2831, 1);

                //--------------------------------------------------//
                // Subch 11 Stop(1) (유량)
                //--------------------------------------------------//
                Comm.Write(SysDefs.ADDR_CHILLER2, 2831, 1);
            }
        }

        private void btnChamber1Hold_Click(object sender, EventArgs e)
        {
            if(_bChamber1Hold == false)
            {
                _bChamber1Hold = true;
                btnChamber1Hold.BackColor = Color.Salmon;
                btnChamber1Hold.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                _bChamber1Hold = false;
                btnChamber1Hold.BackColor = Color.WhiteSmoke;
                btnChamber1Hold.ForeColor = Color.Black;
            }
        }

        private void btnChiller1Hold_Click(object sender, EventArgs e)
        {
            if (_bChiller1Hold == false)
            {
                _bChiller1Hold = true;
                btnChiller1Hold.BackColor = Color.Salmon;
                btnChiller1Hold.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                _bChiller1Hold = false;
                btnChiller1Hold.BackColor = Color.WhiteSmoke;
                btnChiller1Hold.ForeColor = Color.Black;
            }
        }

        private void btnChamber2Hold_Click(object sender, EventArgs e)
        {
            if (_bChamber2Hold == false)
            {
                _bChamber2Hold = true;
                btnChamber2Hold.BackColor = Color.Salmon;
                btnChamber2Hold.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                _bChamber2Hold = false;
                btnChamber2Hold.BackColor = Color.WhiteSmoke;
                btnChamber2Hold.ForeColor = Color.Black;
            }
        }

        private void btnChiller2Hold_Click(object sender, EventArgs e)
        {
            if (_bChiller2Hold == false)
            {
                _bChiller2Hold = true;
                btnChiller2Hold.BackColor = Color.Salmon;
                btnChiller2Hold.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                _bChiller2Hold = false;
                btnChiller2Hold.BackColor = Color.WhiteSmoke;
                btnChiller2Hold.ForeColor = Color.Black;
            }
        }
    }
}

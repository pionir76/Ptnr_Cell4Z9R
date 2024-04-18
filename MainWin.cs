using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Serialization;

using static Ptnr.TSpec;
using static Ptnr.TSpecTmChamber;
using static Ptnr.TSpecTpChamber;

namespace Ptnr
{    
    public partial class MainWin : Form
    {
        private Config _cfg;
        private EqmtForm[] eqmt;
        private UCEqmt[] ucEqmt;

        public MainWin()
        {
            this.Size = new Size(1900, 1080);
            this.WindowState = FormWindowState.Maximized;

            //----------------------------------------------------------------//
            // Config Load
            //----------------------------------------------------------------//
            _cfg = new Config();
            LoadConfigFile();

            //----------------------------------------------------------------//
            // Eqmt Form Create
            //----------------------------------------------------------------//
            eqmt = new EqmtForm[SysDefs.MAX_CHAMBER_CNT];
            ucEqmt = new UCEqmt[SysDefs.MAX_CHAMBER_CNT];

            int sw = this.Size.Width / 4;

            for (int i = 0; i < eqmt.Length; i++)
            {
                eqmt[i] = new EqmtForm(i, _cfg);
                eqmt[i].TopLevel = false;

                eqmt[i].UpdateConfig += SaveConfigFile;
                eqmt[i].EqmtClose += OnChamberClose;

                ucEqmt[i] = new UCEqmt();
                ucEqmt[i].Eqmt = eqmt[i];

                ucEqmt[i].ShowUpEvent += new EventHandler(OnChamberShowUp);

                this.Controls.Add(ucEqmt[i]);
            }

            InitializeComponent();

            //----------------------------------------------------------------//
            // Set up clock.
            //----------------------------------------------------------------//
            System.Timers.Timer clock = new System.Timers.Timer(1000);
            clock.SynchronizingObject = this;

            clock.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            clock.AutoReset = true;
            clock.Enabled = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료 하시겠습니까?",
                               "프로그램 종료",
                                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                for (int i = 0; i < eqmt.Length; i++)
                {
                    eqmt[i].Dispose();
                }
            }
        }

        //--------------------------------------------------------------------------//
        // Current Date/Time
        //--------------------------------------------------------------------------//
        private void OnTimedEvent(Object src, ElapsedEventArgs e)
        {
            DateTime localDate = DateTime.Now;
            this.lblClock.Text = localDate.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void LoadConfigFile()
        {
            string pathName = SysDefs.execPath + SysDefs.CONFIG_FILE;
            XmlSerializer xs = new XmlSerializer(typeof(Config));

            using (var fs = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs))
            {
                _cfg = (Config)xs.Deserialize(sr);
            }
        }

        private void SaveConfigFile(object sender, EventArgs e)
        {
            string pathName = SysDefs.execPath + SysDefs.CONFIG_FILE;

            XmlSerializer xs = new XmlSerializer(typeof(Config));
            using (var fs = new FileStream(pathName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (TextWriter tw = new StreamWriter(fs))
            {
                xs.Serialize(tw, _cfg);
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            SetupForm dlg = new SetupForm(_cfg);
            
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                SaveConfigFile(this, EventArgs.Empty);

                for (int i = 0; i < eqmt.Length; i++)
                {
                    eqmt[i].UpdateTestSheet();
                }
            }
        }

        private void OnChamberShowUp(object sender, EventArgs e)
        {
            EqmtForm ef = sender as EqmtForm;

            if(ef != null)
            {
                this.Controls.Add(ef);
                ef.Dock = DockStyle.Fill;
                ef.Show();

                ef.BringToFront();
            }

            for (int i = 0; i < eqmt.Length; i++)
            {
                ucEqmt[i].Hide();
            }
        }

        private void OnChamberClose(object sender, EventArgs e)
        {
            for (int i = 0; i < eqmt.Length; i++)
            {
                ucEqmt[i].Show();
            }
        }
        
        private void MainWin_Load(object sender, EventArgs e)
        {

        }

        private void MainWin_SizeChanged(object sender, EventArgs e)
        {
            int gap = 100;
            int sw = (this.Size.Width-gap*2) / 4;

            for (int i = 0; i < 4; i++)
            {
                ucEqmt[i].Location = new System.Drawing.Point(gap + (i * sw), 149);
            }

            for (int i = 0; i < 4; i++)
            {
                ucEqmt[4+i].Location = new System.Drawing.Point(gap + (i * sw), 549);
            }
        }
    }
}

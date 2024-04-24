using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Ptnr
{
    public partial class UCEqmt : UserControl
    {
        public event EventHandler ShowUpEvent;

        public EqmtForm Eqmt { get; set; }

        public UCEqmt()
        {
            InitializeComponent();

            this.cmbType.Items.Add("항온항습기");
            this.cmbType.Items.Add("항온기");
            this.cmbType.SelectedIndex = 0;

            //----------------------------------------------------------------//
            // Set up clock.
            //----------------------------------------------------------------//
            System.Timers.Timer tmr = new System.Timers.Timer(1000);
            tmr.SynchronizingObject = this;

            tmr.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            tmr.AutoReset = true;
            tmr.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (Eqmt != null) {
                this.lblTitle.Text = string.Format("Chamber #{0}",Eqmt.EqmtIdx+1);

                if (Eqmt.bWorkingNow)
                {
                    this.lblSts.Text = "TESTING..";
                    this.lblSts.BackColor = Color.Red;
                    this.lblSts.ForeColor = Color.WhiteSmoke;

                    System.TimeSpan spn = (DateTime.Now - Eqmt.WorkStartTime);
                    this.lblWTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", spn.Hours, spn.Minutes, spn.Seconds);

                    this.btnStart.Text = "STOP";
                }

                else
                {
                    this.lblSts.Text = "IDLE";
                    this.lblSts.BackColor = Color.Salmon;
                    this.lblSts.ForeColor = Color.WhiteSmoke;

                    this.lblWTime.Text = "00:00:00";

                    this.btnStart.Text = "START";
                }
            }
        }

        // not use...
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Eqmt != null)
            {
                //Eqmt.StartTest();
            }
        }

        private void btnShowUp_Click(object sender, EventArgs e)
        {
            if (ShowUpEvent != null)
            {
                this.ShowUpEvent(this.Eqmt, e);
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Eqmt == null)
            {
                return;
            }
            
            if (this.cmbType.SelectedIndex == 0)
            {
                Eqmt.SetType(EqmtType.Temi);
            }
            else
            {
                Eqmt.SetType(EqmtType.Temp);
            }
        }
    }
}

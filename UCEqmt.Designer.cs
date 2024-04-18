namespace Ptnr
{
    partial class UCEqmt
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSts = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnShowUp = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSts);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 64);
            this.panel1.TabIndex = 0;
            // 
            // lblSts
            // 
            this.lblSts.BackColor = System.Drawing.Color.DimGray;
            this.lblSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSts.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSts.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblSts.Location = new System.Drawing.Point(191, 0);
            this.lblSts.Name = "lblSts";
            this.lblSts.Size = new System.Drawing.Size(114, 64);
            this.lblSts.TabIndex = 3;
            this.lblSts.Text = "TESTING..";
            this.lblSts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.IndianRed;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(191, 64);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Chamber #1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWTime
            // 
            this.lblWTime.BackColor = System.Drawing.Color.DimGray;
            this.lblWTime.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWTime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblWTime.Location = new System.Drawing.Point(27, 110);
            this.lblWTime.Name = "lblWTime";
            this.lblWTime.Size = new System.Drawing.Size(182, 40);
            this.lblWTime.TabIndex = 2;
            this.lblWTime.Text = "00:00:00";
            this.lblWTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(29, 256);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(245, 56);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnShowUp
            // 
            this.btnShowUp.Font = new System.Drawing.Font("돋움체", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowUp.Location = new System.Drawing.Point(216, 110);
            this.btnShowUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowUp.Name = "btnShowUp";
            this.btnShowUp.Size = new System.Drawing.Size(57, 40);
            this.btnShowUp.TabIndex = 3;
            this.btnShowUp.Text = "+";
            this.btnShowUp.UseVisualStyleBackColor = true;
            this.btnShowUp.Click += new System.EventHandler(this.btnShowUp_Click);
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("돋움체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(31, 182);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(243, 28);
            this.cmbType.TabIndex = 4;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // UCEqmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.btnShowUp);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblWTime);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(307, 396);
            this.Name = "UCEqmt";
            this.Size = new System.Drawing.Size(305, 341);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSts;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWTime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnShowUp;
        private System.Windows.Forms.ComboBox cmbType;
    }
}

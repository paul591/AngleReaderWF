namespace AngleReaderWF
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.edtPPR = new DevExpress.XtraEditors.SpinEdit();
            this.edtFilterDepth = new DevExpress.XtraEditors.SpinEdit();
            this.edtLoopPeriod = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.checkFollowTail = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.edtZeroValue = new DevExpress.XtraEditors.SpinEdit();
            this.edtCOMport = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.edtTestMode = new DevExpress.XtraEditors.CheckEdit();
            this.edtLog = new DevExpress.XtraEditors.ListBoxControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.edtPPR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtFilterDepth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLoopPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkFollowTail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtZeroValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCOMport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTestMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLog)).BeginInit();
            this.SuspendLayout();
            // 
            // edtPPR
            // 
            this.edtPPR.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtPPR.Location = new System.Drawing.Point(194, 71);
            this.edtPPR.Name = "edtPPR";
            this.edtPPR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtPPR.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtPPR.Size = new System.Drawing.Size(164, 20);
            this.edtPPR.TabIndex = 0;
            // 
            // edtFilterDepth
            // 
            this.edtFilterDepth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtFilterDepth.Location = new System.Drawing.Point(194, 111);
            this.edtFilterDepth.Name = "edtFilterDepth";
            this.edtFilterDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtFilterDepth.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtFilterDepth.Size = new System.Drawing.Size(164, 20);
            this.edtFilterDepth.TabIndex = 1;
            // 
            // edtLoopPeriod
            // 
            this.edtLoopPeriod.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtLoopPeriod.Location = new System.Drawing.Point(194, 151);
            this.edtLoopPeriod.Name = "edtLoopPeriod";
            this.edtLoopPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtLoopPeriod.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtLoopPeriod.Size = new System.Drawing.Size(164, 20);
            this.edtLoopPeriod.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 114);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "RPM Filter Depth";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(145, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Encoder Pulses Per Revolution";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 154);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(122, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Data transmit period (ms)";
            // 
            // checkFollowTail
            // 
            this.checkFollowTail.EditValue = true;
            this.checkFollowTail.Location = new System.Drawing.Point(282, 426);
            this.checkFollowTail.Name = "checkFollowTail";
            this.checkFollowTail.Properties.Caption = "Follow Tail";
            this.checkFollowTail.Size = new System.Drawing.Size(75, 19);
            this.checkFollowTail.TabIndex = 7;
            this.checkFollowTail.CheckedChanged += new System.EventHandler(this.checkFollowTail_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(122, 467);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(203, 467);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(284, 467);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 195);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(142, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Reset (zero) to what degree?";
            // 
            // edtZeroValue
            // 
            this.edtZeroValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtZeroValue.Location = new System.Drawing.Point(194, 192);
            this.edtZeroValue.Name = "edtZeroValue";
            this.edtZeroValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtZeroValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtZeroValue.Properties.MaxValue = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.edtZeroValue.Size = new System.Drawing.Size(164, 20);
            this.edtZeroValue.TabIndex = 11;
            // 
            // edtCOMport
            // 
            this.edtCOMport.Location = new System.Drawing.Point(194, 30);
            this.edtCOMport.Name = "edtCOMport";
            this.edtCOMport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtCOMport.Size = new System.Drawing.Size(164, 20);
            this.edtCOMport.TabIndex = 13;
            this.edtCOMport.Click += new System.EventHandler(this.edtCOMport_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(13, 33);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(83, 13);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "Serial (COM) Port";
            // 
            // edtTestMode
            // 
            this.edtTestMode.Location = new System.Drawing.Point(13, 232);
            this.edtTestMode.Name = "edtTestMode";
            this.edtTestMode.Properties.Caption = "Enable Test Mode (Cancelled when dialog closes)";
            this.edtTestMode.Size = new System.Drawing.Size(344, 19);
            this.edtTestMode.TabIndex = 15;
            this.edtTestMode.CheckedChanged += new System.EventHandler(this.edtTestMode_CheckedChanged);
            // 
            // edtLog
            // 
            this.edtLog.DataSource = typeof(AngleReaderWF.LogEntry);
            this.edtLog.DisplayMember = "LogText";
            this.edtLog.Location = new System.Drawing.Point(12, 273);
            this.edtLog.Name = "edtLog";
            this.edtLog.Size = new System.Drawing.Size(345, 147);
            this.edtLog.TabIndex = 3;
            this.edtLog.ValueMember = "LogText";
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClear.Location = new System.Drawing.Point(13, 426);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear Log";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 506);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.edtTestMode);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.edtCOMport);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.edtZeroValue);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.checkFollowTail);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.edtLog);
            this.Controls.Add(this.edtLoopPeriod);
            this.Controls.Add(this.edtFilterDepth);
            this.Controls.Add(this.edtPPR);
            this.Name = "SettingsDialog";
            this.Text = "Angle Reader Preferences & Log";
            ((System.ComponentModel.ISupportInitialize)(this.edtPPR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtFilterDepth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLoopPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkFollowTail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtZeroValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtCOMport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTestMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit edtPPR;
        private DevExpress.XtraEditors.SpinEdit edtFilterDepth;
        private DevExpress.XtraEditors.SpinEdit edtLoopPeriod;
        private DevExpress.XtraEditors.ListBoxControl edtLog;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit checkFollowTail;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit edtZeroValue;
        private DevExpress.XtraEditors.ComboBoxEdit edtCOMport;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit edtTestMode;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}
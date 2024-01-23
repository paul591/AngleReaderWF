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
            this.edtLog = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.edtPPR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtFilterDepth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLoopPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLog)).BeginInit();
            this.SuspendLayout();
            // 
            // edtPPR
            // 
            this.edtPPR.Location = new System.Drawing.Point(194, 26);
            this.edtPPR.Name = "edtPPR";
            this.edtPPR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtPPR.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtPPR.Size = new System.Drawing.Size(164, 20);
            this.edtPPR.TabIndex = 0;
            // 
            // edtFilterDepth
            // 
            this.edtFilterDepth.Location = new System.Drawing.Point(194, 66);
            this.edtFilterDepth.Name = "edtFilterDepth";
            this.edtFilterDepth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtFilterDepth.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtFilterDepth.Size = new System.Drawing.Size(164, 20);
            this.edtFilterDepth.TabIndex = 1;
            // 
            // edtLoopPeriod
            // 
            this.edtLoopPeriod.Location = new System.Drawing.Point(194, 106);
            this.edtLoopPeriod.Name = "edtLoopPeriod";
            this.edtLoopPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edtLoopPeriod.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edtLoopPeriod.Size = new System.Drawing.Size(164, 20);
            this.edtLoopPeriod.TabIndex = 2;
            // 
            // edtLog
            // 
            this.edtLog.Location = new System.Drawing.Point(13, 158);
            this.edtLog.Name = "edtLog";
            this.edtLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.edtLog.Size = new System.Drawing.Size(345, 172);
            this.edtLog.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "RPM Filter Depth";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(145, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Encoder Pulses Per Revolution";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 109);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(122, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Data transmit period (ms)";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 342);
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
    }
}
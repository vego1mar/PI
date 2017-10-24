namespace PI
{
    partial class MeansSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiGrid_PowRank_Num = new System.Windows.Forms.NumericUpDown();
            this.ui_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.ui_Ok_Btn = new System.Windows.Forms.Button();
            this.ui_Grid_TbLay = new System.Windows.Forms.TableLayoutPanel();
            this.uiGrid_Finish_ComBx = new System.Windows.Forms.ComboBox();
            this.uiGrid_Finish_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_Toler_Num = new System.Windows.Forms.NumericUpDown();
            this.uiGrid_Toler_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_Comp_ComBx = new System.Windows.Forms.ComboBox();
            this.uiGrid_Comp_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_CstTol_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_CstDiff_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_Power_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_DiffMode_ComBx = new System.Windows.Forms.ComboBox();
            this.uiGrid_DiffMode_TxtBx = new System.Windows.Forms.TextBox();
            this.uiGrid_PowRank_TxtBx = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_PowRank_Num)).BeginInit();
            this.ui_TbLay.SuspendLayout();
            this.ui_Grid_TbLay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_Toler_Num)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGrid_PowRank_Num
            // 
            this.uiGrid_PowRank_Num.DecimalPlaces = 2;
            this.uiGrid_PowRank_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_PowRank_Num.Location = new System.Drawing.Point(202, 28);
            this.uiGrid_PowRank_Num.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.uiGrid_PowRank_Num.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.uiGrid_PowRank_Num.Name = "uiGrid_PowRank_Num";
            this.uiGrid_PowRank_Num.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_PowRank_Num.TabIndex = 1;
            this.uiGrid_PowRank_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiGrid_PowRank_Num.ThousandsSeparator = true;
            // 
            // ui_TbLay
            // 
            this.ui_TbLay.ColumnCount = 1;
            this.ui_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.Controls.Add(this.ui_Ok_Btn, 0, 1);
            this.ui_TbLay.Controls.Add(this.ui_Grid_TbLay, 0, 0);
            this.ui_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_TbLay.Location = new System.Drawing.Point(0, 0);
            this.ui_TbLay.Name = "ui_TbLay";
            this.ui_TbLay.RowCount = 2;
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ui_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.ui_TbLay.Size = new System.Drawing.Size(404, 381);
            this.ui_TbLay.TabIndex = 1;
            // 
            // ui_Ok_Btn
            // 
            this.ui_Ok_Btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ui_Ok_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_Ok_Btn.Location = new System.Drawing.Point(3, 349);
            this.ui_Ok_Btn.Name = "ui_Ok_Btn";
            this.ui_Ok_Btn.Size = new System.Drawing.Size(398, 29);
            this.ui_Ok_Btn.TabIndex = 1;
            this.ui_Ok_Btn.Text = "OK";
            this.ui_Ok_Btn.UseVisualStyleBackColor = true;
            this.ui_Ok_Btn.Click += new System.EventHandler(this.Ui_Ok_Click);
            // 
            // ui_Grid_TbLay
            // 
            this.ui_Grid_TbLay.AutoScroll = true;
            this.ui_Grid_TbLay.BackColor = System.Drawing.Color.Transparent;
            this.ui_Grid_TbLay.ColumnCount = 2;
            this.ui_Grid_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_Grid_TbLay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Finish_ComBx, 1, 9);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Finish_TxtBx, 0, 9);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Toler_Num, 1, 8);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Toler_TxtBx, 0, 8);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Comp_ComBx, 1, 7);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Comp_TxtBx, 0, 7);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_CstTol_TxtBx, 0, 6);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_CstDiff_TxtBx, 0, 3);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_Power_TxtBx, 0, 0);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_DiffMode_ComBx, 1, 4);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_DiffMode_TxtBx, 0, 4);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_PowRank_TxtBx, 0, 1);
            this.ui_Grid_TbLay.Controls.Add(this.uiGrid_PowRank_Num, 1, 1);
            this.ui_Grid_TbLay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ui_Grid_TbLay.Location = new System.Drawing.Point(3, 3);
            this.ui_Grid_TbLay.Name = "ui_Grid_TbLay";
            this.ui_Grid_TbLay.RowCount = 12;
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ui_Grid_TbLay.Size = new System.Drawing.Size(398, 340);
            this.ui_Grid_TbLay.TabIndex = 2;
            // 
            // uiGrid_Finish_ComBx
            // 
            this.uiGrid_Finish_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Finish_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiGrid_Finish_ComBx.FormattingEnabled = true;
            this.uiGrid_Finish_ComBx.Location = new System.Drawing.Point(202, 228);
            this.uiGrid_Finish_ComBx.Name = "uiGrid_Finish_ComBx";
            this.uiGrid_Finish_ComBx.Size = new System.Drawing.Size(193, 21);
            this.uiGrid_Finish_ComBx.TabIndex = 36;
            // 
            // uiGrid_Finish_TxtBx
            // 
            this.uiGrid_Finish_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_Finish_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_Finish_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Finish_TxtBx.Location = new System.Drawing.Point(3, 228);
            this.uiGrid_Finish_TxtBx.Name = "uiGrid_Finish_TxtBx";
            this.uiGrid_Finish_TxtBx.ReadOnly = true;
            this.uiGrid_Finish_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_Finish_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_Finish_TxtBx.TabIndex = 35;
            this.uiGrid_Finish_TxtBx.Text = "Finisher function:";
            // 
            // uiGrid_Toler_Num
            // 
            this.uiGrid_Toler_Num.DecimalPlaces = 2;
            this.uiGrid_Toler_Num.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Toler_Num.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.uiGrid_Toler_Num.Location = new System.Drawing.Point(202, 203);
            this.uiGrid_Toler_Num.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.uiGrid_Toler_Num.Name = "uiGrid_Toler_Num";
            this.uiGrid_Toler_Num.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_Toler_Num.TabIndex = 34;
            this.uiGrid_Toler_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uiGrid_Toler_Num.ThousandsSeparator = true;
            this.uiGrid_Toler_Num.Value = new decimal(new int[] {
            105,
            0,
            0,
            131072});
            // 
            // uiGrid_Toler_TxtBx
            // 
            this.uiGrid_Toler_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_Toler_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_Toler_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Toler_TxtBx.Location = new System.Drawing.Point(3, 203);
            this.uiGrid_Toler_TxtBx.Name = "uiGrid_Toler_TxtBx";
            this.uiGrid_Toler_TxtBx.ReadOnly = true;
            this.uiGrid_Toler_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_Toler_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_Toler_TxtBx.TabIndex = 33;
            this.uiGrid_Toler_TxtBx.Text = "Comparer tolerance:";
            // 
            // uiGrid_Comp_ComBx
            // 
            this.uiGrid_Comp_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Comp_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiGrid_Comp_ComBx.FormattingEnabled = true;
            this.uiGrid_Comp_ComBx.Location = new System.Drawing.Point(202, 178);
            this.uiGrid_Comp_ComBx.Name = "uiGrid_Comp_ComBx";
            this.uiGrid_Comp_ComBx.Size = new System.Drawing.Size(193, 21);
            this.uiGrid_Comp_ComBx.TabIndex = 32;
            // 
            // uiGrid_Comp_TxtBx
            // 
            this.uiGrid_Comp_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_Comp_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_Comp_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Comp_TxtBx.Location = new System.Drawing.Point(3, 178);
            this.uiGrid_Comp_TxtBx.Name = "uiGrid_Comp_TxtBx";
            this.uiGrid_Comp_TxtBx.ReadOnly = true;
            this.uiGrid_Comp_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_Comp_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_Comp_TxtBx.TabIndex = 31;
            this.uiGrid_Comp_TxtBx.Text = "Comparer of max/min blocks:";
            // 
            // uiGrid_CstTol_TxtBx
            // 
            this.uiGrid_CstTol_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.ui_Grid_TbLay.SetColumnSpan(this.uiGrid_CstTol_TxtBx, 2);
            this.uiGrid_CstTol_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_CstTol_TxtBx.Enabled = false;
            this.uiGrid_CstTol_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiGrid_CstTol_TxtBx.Location = new System.Drawing.Point(3, 153);
            this.uiGrid_CstTol_TxtBx.Name = "uiGrid_CstTol_TxtBx";
            this.uiGrid_CstTol_TxtBx.ReadOnly = true;
            this.uiGrid_CstTol_TxtBx.Size = new System.Drawing.Size(392, 20);
            this.uiGrid_CstTol_TxtBx.TabIndex = 30;
            this.uiGrid_CstTol_TxtBx.Text = "Custom-Tolerance";
            // 
            // uiGrid_CstDiff_TxtBx
            // 
            this.uiGrid_CstDiff_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.ui_Grid_TbLay.SetColumnSpan(this.uiGrid_CstDiff_TxtBx, 2);
            this.uiGrid_CstDiff_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_CstDiff_TxtBx.Enabled = false;
            this.uiGrid_CstDiff_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiGrid_CstDiff_TxtBx.Location = new System.Drawing.Point(3, 78);
            this.uiGrid_CstDiff_TxtBx.Name = "uiGrid_CstDiff_TxtBx";
            this.uiGrid_CstDiff_TxtBx.ReadOnly = true;
            this.uiGrid_CstDiff_TxtBx.Size = new System.Drawing.Size(392, 20);
            this.uiGrid_CstDiff_TxtBx.TabIndex = 29;
            this.uiGrid_CstDiff_TxtBx.Text = "Custom-Differential";
            // 
            // uiGrid_Power_TxtBx
            // 
            this.uiGrid_Power_TxtBx.BackColor = System.Drawing.SystemColors.Control;
            this.ui_Grid_TbLay.SetColumnSpan(this.uiGrid_Power_TxtBx, 2);
            this.uiGrid_Power_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_Power_TxtBx.Enabled = false;
            this.uiGrid_Power_TxtBx.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uiGrid_Power_TxtBx.Location = new System.Drawing.Point(3, 3);
            this.uiGrid_Power_TxtBx.Name = "uiGrid_Power_TxtBx";
            this.uiGrid_Power_TxtBx.ReadOnly = true;
            this.uiGrid_Power_TxtBx.Size = new System.Drawing.Size(392, 20);
            this.uiGrid_Power_TxtBx.TabIndex = 28;
            this.uiGrid_Power_TxtBx.Text = "Power";
            // 
            // uiGrid_DiffMode_ComBx
            // 
            this.uiGrid_DiffMode_ComBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_DiffMode_ComBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiGrid_DiffMode_ComBx.FormattingEnabled = true;
            this.uiGrid_DiffMode_ComBx.Location = new System.Drawing.Point(202, 103);
            this.uiGrid_DiffMode_ComBx.Name = "uiGrid_DiffMode_ComBx";
            this.uiGrid_DiffMode_ComBx.Size = new System.Drawing.Size(193, 21);
            this.uiGrid_DiffMode_ComBx.TabIndex = 27;
            // 
            // uiGrid_DiffMode_TxtBx
            // 
            this.uiGrid_DiffMode_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_DiffMode_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_DiffMode_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_DiffMode_TxtBx.Location = new System.Drawing.Point(3, 103);
            this.uiGrid_DiffMode_TxtBx.Name = "uiGrid_DiffMode_TxtBx";
            this.uiGrid_DiffMode_TxtBx.ReadOnly = true;
            this.uiGrid_DiffMode_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_DiffMode_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_DiffMode_TxtBx.TabIndex = 26;
            this.uiGrid_DiffMode_TxtBx.Text = "Workmode:";
            // 
            // uiGrid_PowRank_TxtBx
            // 
            this.uiGrid_PowRank_TxtBx.BackColor = System.Drawing.SystemColors.HighlightText;
            this.uiGrid_PowRank_TxtBx.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiGrid_PowRank_TxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGrid_PowRank_TxtBx.Location = new System.Drawing.Point(3, 28);
            this.uiGrid_PowRank_TxtBx.Name = "uiGrid_PowRank_TxtBx";
            this.uiGrid_PowRank_TxtBx.ReadOnly = true;
            this.uiGrid_PowRank_TxtBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiGrid_PowRank_TxtBx.Size = new System.Drawing.Size(193, 20);
            this.uiGrid_PowRank_TxtBx.TabIndex = 25;
            this.uiGrid_PowRank_TxtBx.Text = "Rank k:";
            // 
            // MeansSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 381);
            this.Controls.Add(this.ui_TbLay);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 420);
            this.Name = "MeansSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Means Settings";
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_PowRank_Num)).EndInit();
            this.ui_TbLay.ResumeLayout(false);
            this.ui_Grid_TbLay.ResumeLayout(false);
            this.ui_Grid_TbLay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGrid_Toler_Num)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel ui_TbLay;
        private System.Windows.Forms.Button ui_Ok_Btn;
        private System.Windows.Forms.NumericUpDown uiGrid_PowRank_Num;
        private System.Windows.Forms.TableLayoutPanel ui_Grid_TbLay;
        private System.Windows.Forms.TextBox uiGrid_PowRank_TxtBx;
        private System.Windows.Forms.TextBox uiGrid_DiffMode_TxtBx;
        private System.Windows.Forms.ComboBox uiGrid_DiffMode_ComBx;
        private System.Windows.Forms.TextBox uiGrid_Power_TxtBx;
        private System.Windows.Forms.TextBox uiGrid_CstTol_TxtBx;
        private System.Windows.Forms.TextBox uiGrid_CstDiff_TxtBx;
        private System.Windows.Forms.ComboBox uiGrid_Comp_ComBx;
        private System.Windows.Forms.TextBox uiGrid_Comp_TxtBx;
        private System.Windows.Forms.NumericUpDown uiGrid_Toler_Num;
        private System.Windows.Forms.TextBox uiGrid_Toler_TxtBx;
        private System.Windows.Forms.ComboBox uiGrid_Finish_ComBx;
        private System.Windows.Forms.TextBox uiGrid_Finish_TxtBx;
    }
}